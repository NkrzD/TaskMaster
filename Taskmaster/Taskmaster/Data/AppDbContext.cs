//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Taskmaster.Data
//{
//    internal class AppDbContext
//    {
//    }
//}

using Microsoft.EntityFrameworkCore;
using TaskMaster.Models;

namespace TaskMaster.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tache> Taches { get; set; } = default!;
        public DbSet<Utilisateur> Utilisateurs { get; set; } = default!;
        public DbSet<Commentaire> Commentaires { get; set; } = default!;
        public DbSet<SousTache> SousTaches { get; set; } = default!;
        public DbSet<Etiquette> Etiquettes { get; set; } = default!;
        public DbSet<TacheEtiquette> TacheEtiquettes { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "Server=localhost;Port=3306;Database=taskmanagerdb;User=root;Password=;",
                    new MySqlServerVersion(new Version(8, 0, 30))
                );
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relation Many-to-Many entre Tache et Etiquette via TacheEtiquette
            modelBuilder.Entity<TacheEtiquette>()
                .HasKey(te => new { te.TacheId, te.EtiquetteId });

            modelBuilder.Entity<TacheEtiquette>()
                .HasOne(te => te.Tache)
                .WithMany(t => t.TacheEtiquettes)
                .HasForeignKey(te => te.TacheId);

            modelBuilder.Entity<TacheEtiquette>()
                .HasOne(te => te.Etiquette)
                .WithMany(e => e.TacheEtiquettes)
                .HasForeignKey(te => te.EtiquetteId);

            // Relations entre Utilisateur et Tache
            modelBuilder.Entity<Tache>()
                .HasOne(t => t.Auteur)
                .WithMany(u => u.TachesCreees)
                .HasForeignKey(t => t.AuteurId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tache>()
                .HasOne(t => t.Realisateur)
                .WithMany(u => u.TachesAssignees)
                .HasForeignKey(t => t.RealisateurId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation Commentaire -> Tache
            modelBuilder.Entity<Commentaire>()
                .HasOne(c => c.Tache)
                .WithMany(t => t.Commentaires)
                .HasForeignKey(c => c.TacheId);

            // Relation Commentaire -> Utilisateur
            modelBuilder.Entity<Commentaire>()
                .HasOne(c => c.Auteur)
                .WithMany(u => u.Commentaires)
                .HasForeignKey(c => c.AuteurId);

            // Relation SousTache -> Tache
            modelBuilder.Entity<SousTache>()
                .HasOne(st => st.Tache)
                .WithMany(t => t.SousTaches)
                .HasForeignKey(st => st.TacheId);
        }
    }
}

