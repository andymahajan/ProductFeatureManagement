using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProductFeatureManagementWebApi.Models;

public partial class ProductFeatureMgmtDbContext : DbContext
{
    public ProductFeatureMgmtDbContext()
    {
    }

    public ProductFeatureMgmtDbContext(DbContextOptions<ProductFeatureMgmtDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Complexity> Complexities { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=Database");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Complexity>(entity =>
        {
            entity.HasKey(e => e.ComplexityId).HasName("PK__Complexi__A9541F3A1C50DEFC");

            entity.ToTable("Complexity");

            entity.Property(e => e.ComplexityName).HasMaxLength(20);
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.HasKey(e => e.FeaturesId).HasName("PK__Features__5265EF8CE0A17670");

            entity.Property(e => e.ActualCompletionDate).HasColumnType("date");
            entity.Property(e => e.TargetCompletionDate).HasColumnType("date");
            entity.Property(e => e.Title).HasMaxLength(1000);

            entity.HasOne(d => d.Complexity).WithMany(p => p.Features)
                .HasForeignKey(d => d.ComplexityId)
                .HasConstraintName("FK_Features_Complexity");

            entity.HasOne(d => d.Status).WithMany(p => p.Features)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_Features_Status");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE2063C9EB2380");

            entity.ToTable("Status");

            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
