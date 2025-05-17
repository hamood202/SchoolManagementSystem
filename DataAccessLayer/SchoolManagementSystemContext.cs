using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Domain;
using DataAccessLayer.UserModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccessLayer;

public partial class SchoolManagementSystemContext : IdentityDbContext<ApplicationUser>
{
    public SchoolManagementSystemContext()
    {
    }

    public SchoolManagementSystemContext(DbContextOptions<SchoolManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbAttendance> TbAttendances { get; set; }

    public virtual DbSet<TbCity> TbCities { get; set; }

    public virtual DbSet<TbClass> TbClasses { get; set; }

    public virtual DbSet<TbStudent> TbStudents { get; set; }

    public virtual DbSet<TbSubject> TbSubjects { get; set; }

    public virtual DbSet<TbTeacher> TbTeachers { get; set; }

    public virtual DbSet<TbTeacherSubject> TbTeacherSubjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=SchoolManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;");
        }
    }
      
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TbAttendance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TbAttend__3214EC07292606CE");

            entity.ToTable("TbAttendance");

            entity.HasIndex(e => new { e.StudentId, e.Date }, "UQ_TbAttendance_StudentId_Date").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrentState).HasDefaultValue(1);
            entity.Property(e => e.IsPresent).HasDefaultValue(true);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Student).WithMany(p => p.TbAttendances).HasForeignKey(d => d.StudentId);
        });

        modelBuilder.Entity<TbCity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TbCities__3214EC0709C22D47");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CityAname)
                .HasMaxLength(50)
                .HasColumnName("CityAName");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TbClasse__3214EC074C2D77BE");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrentState).HasDefaultValue(1);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbStudent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TbStuden__3214EC07CD44C50D");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrentState).HasDefaultValue(1);
            entity.Property(e => e.Ename)
                .HasMaxLength(255)
                .HasColumnName("EName");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Note).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbSubject>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TbTeacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TbTeache__3214EC07CAABCCF7");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrentState).HasDefaultValue(1);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Note).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Qualification).HasMaxLength(100);
            entity.Property(e => e.Specialization).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.TbTeacher)
                .HasForeignKey<TbTeacher>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbTeachers_TbCities_CitiesId");
        });

        modelBuilder.Entity<TbTeacherSubject>(entity =>
        {
            entity.HasKey(e => new { e.TeacherId, e.SubjectId }).HasName("PK__TbTeache__7733E35E904D4FB2");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrentState).HasDefaultValue(1);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TbTeacherSubjects)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbTeacherSubjects_Teacher_TeacherId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
