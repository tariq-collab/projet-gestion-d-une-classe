using Appli_gestion_cla.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // Ajoutez cet using

namespace Appli_gestion_cla.Data
{
    // Modifiez la ligne suivante pour inclure IdentityRole
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Classe> Classes { get; set; }
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Enseignant> Enseignants { get; set; }
        public DbSet<Matiere> Matieres { get; set; }
        public DbSet<Affectation> Affectations { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Absence> Absences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Appel à la méthode de base POUR Identity
            base.OnModelCreating(modelBuilder);

            // Vos configurations personnalisées
            modelBuilder.Entity<Affectation>()
                .HasIndex(a => new { a.EnseignantId, a.MatiereId, a.ClasseId })
                .IsUnique();

            modelBuilder.Entity<Note>()
                .HasOne(n => n.Etudiant)
                .WithMany(e => e.Notes)
                .HasForeignKey(n => n.EtudiantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.Affectation)
                .WithMany(a => a.Notes)
                .HasForeignKey(n => n.AffectationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enseignant>()
                .HasOne(e => e.Matiere)
                .WithMany(m => m.Enseignants)
                .HasForeignKey(n => n.MatiereId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Absence>(entity =>
            {
                // Relation avec Etudiant
                entity.HasOne(a => a.Etudiant)
                      .WithMany(e => e.Absences) // ← CORRIGEZ ICI : ajoutez la navigation
                      .HasForeignKey(a => a.EtudiantId)
                      .OnDelete(DeleteBehavior.ClientSetNull); // Modifié de Restrict

                // Relation avec Classe (manquante dans votre config)
                entity.HasOne(a => a.Classe)
                      .WithMany(c => c.Absences) // ← Navigation inverse dans Classe
                      .HasForeignKey(a => a.ClasseId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    // Modifiez la ligne suivante pour inclure IdentityRole
public DbSet<Appli_gestion_cla.Models.Admin> Admin { get; set; } = default!;
    // Modifiez la ligne suivante pour inclure IdentityRole
public DbSet<Appli_gestion_cla.Models.Prof> Prof { get; set; } = default!;
    // Modifiez la ligne suivante pour inclure IdentityRole

    }
}