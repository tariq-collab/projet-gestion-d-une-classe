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

        public DbSet<Classe> Classe { get; set; }
        public DbSet<Etudiants> Etudiants { get; set; }
        public DbSet<Enseignant> Enseignants { get; set; }
        public DbSet<Matiere> Matiere { get; set; }
        public DbSet<Affectation> Affectations { get; set; }
        public DbSet<Note> Notes { get; set; }

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
        }
    }
}