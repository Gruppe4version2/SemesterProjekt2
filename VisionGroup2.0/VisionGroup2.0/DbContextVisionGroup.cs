using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace VisionGroup2._0
{
    using VisionGroup2._0.DomainClasses;

    public partial class DbContextVisionGroup : DbContext
    {
        public DbContextVisionGroup()
        {
        }

        public DbContextVisionGroup(DbContextOptions<DbContextVisionGroup> options)
            : base(options)
        {
        }

        public virtual DbSet<Costumer> Costumers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectsForEmployee> ProjectsForEmployees { get; set; }

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
            modelBuilder.Entity<Costumer>(entity =>
            {
                entity.ToTable("Costumer");

                entity.Property(e => e.CostumerId).HasColumnName("Costumer_Id");

                entity.Property(e => e.CvrNr).HasColumnName("CVR_nr");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNr).HasColumnName("Phone_Nr");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_Id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNr).HasColumnName("Phone_Nr");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.ProjectId).HasColumnName("Project_Id");

                entity.Property(e => e.CostumerId).HasColumnName("Costumer_Id");

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Costumer)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CostumerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CostumerID");
            });

            modelBuilder.Entity<ProjectsForEmployee>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.EmployeeId });

                entity.Property(e => e.ProjectId).HasColumnName("Project_Id");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_Id");

                entity.Property(e => e.IsLeader).HasColumnName("Is_Leader");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ProjectsForEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeId");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectsForEmployees)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
