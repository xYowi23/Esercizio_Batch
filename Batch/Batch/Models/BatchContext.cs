using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Batch.Models;

public partial class BatchContext : DbContext
{
    public BatchContext()
    {
    }

    public BatchContext(DbContextOptions<BatchContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CodiceFiscale> CodiceFiscales { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("SERVER=ACADEMY2024-24\\SQLEXPRESS;Database=Batch;User Id=academy;Password=academy!;MultipleActiveResultSets=true;Encrypt=false;TrustServerCertificate=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CodiceFiscale>(entity =>
        {
            entity.HasKey(e => e.IdCodFis).HasName("PK__CodiceFi__A41250C81E41424C");

            entity.ToTable("CodiceFiscale");

            entity.HasIndex(e => e.CodiceFis, "UQ__CodiceFi__8DDC5C976BD043FA").IsUnique();

            entity.Property(e => e.IdCodFis).HasColumnName("idCodFis");
            entity.Property(e => e.CodiceFis)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("codiceFis");
            entity.Property(e => e.DataEmis).HasColumnName("dataEmis");
            entity.Property(e => e.DataScad).HasColumnName("dataScad");
            entity.Property(e => e.PersonaRiff).HasColumnName("personaRiff");

            entity.HasOne(d => d.PersonaRiffNavigation).WithMany(p => p.CodiceFiscales)
                .HasForeignKey(d => d.PersonaRiff)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CodiceFis__perso__3B75D760");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Persona__A478814129D85E59");

            entity.ToTable("Persona");

            entity.HasIndex(e => e.Email, "UQ__Persona__AB6E61642AC3FEE8").IsUnique();

            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
            entity.Property(e => e.Cognome)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("cognome");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
