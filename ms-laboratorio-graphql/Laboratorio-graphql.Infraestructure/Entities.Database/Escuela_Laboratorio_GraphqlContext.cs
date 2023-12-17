using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Laboratorio_graphql.infraestructure.Entities.Database
{
    public partial class Escuela_Laboratorio_GraphqlContext : DbContext
    {
        public Escuela_Laboratorio_GraphqlContext()
        {
        }

        public Escuela_Laboratorio_GraphqlContext(DbContextOptions<Escuela_Laboratorio_GraphqlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumnos { get; set; }
        public virtual DbSet<Maestro> Maestros { get; set; }
        public virtual DbSet<MateriaAlumno> MateriaAlumnos { get; set; }
        public virtual DbSet<Materium> Materia { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Spanish_Colombia.1252");

            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.ToTable("alumno", "Escuela");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('\"Escuela\".alumno_id_seq'::regclass)");

                entity.Property(e => e.DocumentoIdentidad).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Maestro>(entity =>
            {
                entity.ToTable("maestro", "Escuela");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('\"Escuela\".maestro_id_seq'::regclass)");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<MateriaAlumno>(entity =>
            {
                entity.ToTable("materia_alumno", "Escuela");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('\"Escuela\".materia_alumno_id_seq'::regclass)");

                entity.Property(e => e.AlumnoId).HasColumnName("alumno_id");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.MateriaId).HasColumnName("materia_id");

                entity.HasOne(d => d.Alumno)
                    .WithMany(p => p.MateriaAlumnos)
                    .HasForeignKey(d => d.AlumnoId)
                    .HasConstraintName("materia_alumno_alumno_id_fkey");

                entity.HasOne(d => d.Materia)
                    .WithMany(p => p.MateriaAlumnos)
                    .HasForeignKey(d => d.MateriaId)
                    .HasConstraintName("materia_alumno_materia_id_fkey");
            });

            modelBuilder.Entity<Materium>(entity =>
            {
                entity.ToTable("materia", "Escuela");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('\"Escuela\".materia_id_seq'::regclass)");

                entity.Property(e => e.MaestroId).HasColumnName("maestro_id");

                entity.Property(e => e.Nombre).HasColumnName("nombre");
            });

            modelBuilder.HasSequence("alumno_id_seq", "Escuela");

            modelBuilder.HasSequence("maestro_id_seq", "Escuela");

            modelBuilder.HasSequence("materia_alumno_id_seq", "Escuela");

            modelBuilder.HasSequence("materia_id_seq", "Escuela");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
