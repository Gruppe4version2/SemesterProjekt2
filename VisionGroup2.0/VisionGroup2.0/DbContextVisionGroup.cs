using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VisionGroup2._0
{
    public partial class DbContextVisionGroup : DbContext
    {
        public DbContextVisionGroup()
        {
        }

        public DbContextVisionGroup(DbContextOptions<DbContextVisionGroup> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=gruppe4.database.windows.net;Initial Catalog=db;Persist Security Info=True;User ID=VisionGroup;Password=Password123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("Project_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CostumerId).HasColumnName("Costumer_Id");

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
