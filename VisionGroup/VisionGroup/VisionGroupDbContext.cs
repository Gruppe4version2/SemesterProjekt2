﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VisionGroup
{
    public partial class VisionGroupDbContext : DbContext
    {
        public VisionGroupDbContext()
        {
        }

        public VisionGroupDbContext(DbContextOptions<VisionGroupDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Costumer> Costumers { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=gruppe4.database.windows.net;Initial Catalog=db;User ID=VisionGroup;Password=Password123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Costumer>(entity =>
            {
                entity.ToTable("Costumer");

                entity.Property(e => e.CostumerId)
                    .HasColumnName("Costumer_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CvrNr).HasColumnName("CVR_nr");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhonNr).HasColumnName("Phon_nr");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("Project_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProjectLeader)
                    .HasColumnName("Project_leader")
                    .HasMaxLength(50);

                entity.HasOne(d => d.ProjectNavigation)
                    .WithOne(p => p.Project)
                    .HasForeignKey<Project>(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CostumerID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
