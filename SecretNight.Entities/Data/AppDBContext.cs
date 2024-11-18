using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SecretNight.Entities.Models;

namespace SecretNight.Entities.Data;

public partial class AppDBContext : DbContext
{
    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=EMILIANOA\\DBSEGUNDOITES;Database=Programacion2DB;User Id=sa;Password=456emi456;TrustServerCertificate=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Esta es la tabla producto y sus campos
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProductos);

            entity.ToTable("Producto");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Talle)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
