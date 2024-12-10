﻿using CCVProyecto2v2.Models;
using CCVProyecto2v2.Utilidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CCVProyecto2v2.DataAccess
{
    public class DbbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DbSet<Profesor> Profesor { get; set; }
        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet<Clase> Clase { get; set; }
        public DbSet<ClaseEstudiante> ClaseEstudiantes { get; set; }
        public DbbContext()
        {

        }

        public DbbContext(DbContextOptions<DbbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDB = $"Filename={ConexionDB.DevolverRuta("CCVProyecto2.db")}";
            optionsBuilder.UseSqlite(conexionDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(c => c.Grado).HasMaxLength(50);
            });
            modelBuilder.Entity<ClaseEstudiante>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(c => c.Materia).HasMaxLength(100);
            });
            modelBuilder.Entity<Clase>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(c => c.Nombre).HasMaxLength(100);
            });

            
            modelBuilder.Entity<Clase>(entity =>
            {
                entity.HasOne(c => c.Profesor)
                    .WithMany(c => c.Clases)
                    .HasForeignKey(c => c.ProfesorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<ClaseEstudiante>()
                .HasOne(c => c.Clase)
                .WithMany(c => c.ClasesEstudiantes)
                .HasForeignKey(c => c.ClaseId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ClaseEstudiante>()
                .HasOne(c => c.Estudiante)
                .WithMany(c => c.ClasesEstudiantes)
                .HasForeignKey(c => c.EstudianteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}