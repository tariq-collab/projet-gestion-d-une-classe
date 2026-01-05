using Appli_gestion_cla.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configuration d'Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddRoles<IdentityRole>() // Pour activer les rôles
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Code d'initialisation des rôles, admin et enseignants
using (var scope = app.Services.CreateScope())
{
    try
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // Création des rôles
        string[] roles = { "Admin", "Prof", "Eleve" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                Console.WriteLine($"✅ Rôle '{role}' créé avec succès.");
            }
        }

        // 1. Création de l'utilisateur ADMIN
        string adminEmail = "admin@admin.com";
        string adminPassword = "Admin@123";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var createAdminResult = await userManager.CreateAsync(adminUser, adminPassword);

            if (createAdminResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                Console.WriteLine($"✅ Utilisateur ADMIN '{adminEmail}' créé avec succès.");
            }
            else
            {
                Console.WriteLine("❌ Erreurs lors de la création de l'admin:");
                foreach (var error in createAdminResult.Errors)
                {
                    Console.WriteLine($"- {error.Description}");
                }
            }
        }
        else
        {
            Console.WriteLine($"ℹ️ Utilisateur ADMIN '{adminEmail}' existe déjà.");
        }

        // 2. Création d'une LISTE d'enseignants
        var enseignants = new[]
        {
            new {
                Email = "martin.sophie@ecole.com",
                Password = "Martin@123",
                Prenom = "Sophie",
                Nom = "Martin",
                Matiere = "Mathématiques"
            },
            new {
                Email = "bernard.pierre@ecole.com",
                Password = "Bernard@123",
                Prenom = "Pierre",
                Nom = "Bernard",
                Matiere = "Physique"
            },
            new {
                Email = "dubois.marie@ecole.com",
                Password = "Dubois@123",
                Prenom = "Marie",
                Nom = "Dubois",
                Matiere = "Français"
            },
            new {
                Email = "lefevre.jacques@ecole.com",
                Password = "Lefevre@123",
                Prenom = "Jacques",
                Nom = "Lefevre",
                Matiere = "Histoire"
            },
            new {
                Email = "roux.claire@ecole.com",
                Password = "Roux@123",
                Prenom = "Claire",
                Nom = "Roux",
                Matiere = "Anglais"
            }
        };

        Console.WriteLine("\n=== CRÉATION DES ENSEIGNANTS ===");

        foreach (var prof in enseignants)
        {
            var profUser = await userManager.FindByEmailAsync(prof.Email);

            if (profUser == null)
            {
                profUser = new IdentityUser
                {
                    UserName = prof.Email,
                    Email = prof.Email,
                    EmailConfirmed = true
                };

                var createProfResult = await userManager.CreateAsync(profUser, prof.Password);

                if (createProfResult.Succeeded)
                {
                    // Ajout du rôle Prof
                    await userManager.AddToRoleAsync(profUser, "Prof");

                    // Ajout des claims pour stocker des informations supplémentaires
                    await userManager.AddClaimAsync(profUser, new System.Security.Claims.Claim("Prenom", prof.Prenom));
                    await userManager.AddClaimAsync(profUser, new System.Security.Claims.Claim("Nom", prof.Nom));
                    await userManager.AddClaimAsync(profUser, new System.Security.Claims.Claim("Matiere", prof.Matiere));
                    await userManager.AddClaimAsync(profUser, new System.Security.Claims.Claim("FullName", $"{prof.Prenom} {prof.Nom}"));

                    Console.WriteLine($"✅ Enseignant créé: {prof.Prenom} {prof.Nom}");
                    Console.WriteLine($"   Email: {prof.Email} | Matière: {prof.Matiere}");
                }
                else
                {
                    Console.WriteLine($"❌ Erreur pour {prof.Email}:");
                    foreach (var error in createProfResult.Errors)
                    {
                        Console.WriteLine($"  - {error.Description}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"ℹ️ Enseignant existe déjà: {prof.Email}");
            }
        }

        Console.WriteLine("\n=== RÉCAPITULATIF DES COMPTES ===");
        Console.WriteLine("ADMIN:");
        Console.WriteLine($"  - admin@admin.com / Admin@123");
        Console.WriteLine("\nENSEIGNANTS:");
        foreach (var prof in enseignants)
        {
            Console.WriteLine($"  - {prof.Email} / {prof.Password}");
            Console.WriteLine($"    → {prof.Prenom} {prof.Nom} - {prof.Matiere}");
        }
        Console.WriteLine("\n⚠️ Note: Changez ces mots de passe après le premier login!");

    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️ Erreur lors de l'initialisation: {ex.Message}");
        Console.WriteLine($"Détails: {ex.InnerException?.Message}");
    }
}

app.Run();