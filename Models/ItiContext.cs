using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Day_7.Models;

public partial class ItiContext : DbContext
{
    //public ItiContext()
    //{
    //}

    //public ItiContext(DbContextOptions<ItiContext> options)
    //    : base(options)
    //{
    //}

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<FullNameCourse> FullNameCourses { get; set; }

    public virtual DbSet<InsNameDepName> InsNameDepNames { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<MangerNameTopic> MangerNameTopics { get; set; }

    public virtual DbSet<V1> V1s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-PBR0UV5\\SQLSERVER2022;Database=ITI;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AS");

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptId);

            entity.ToTable("Department");

            entity.Property(e => e.DeptId)
                .ValueGeneratedNever()
                .HasColumnName("Dept_Id");
            entity.Property(e => e.DeptDesc)
                .HasMaxLength(100)
                .HasColumnName("Dept_Desc");
            entity.Property(e => e.DeptLocation)
                .HasMaxLength(50)
                .HasColumnName("Dept_Location");
            entity.Property(e => e.DeptManager).HasColumnName("Dept_Manager");
            entity.Property(e => e.DeptName)
                .HasMaxLength(50)
                .HasColumnName("Dept_Name");
            entity.Property(e => e.Hiredate).HasColumnType("date");

            entity.HasOne(d => d.DeptManagerNavigation).WithMany(p => p.Departments)
                .HasForeignKey(d => d.DeptManager)
                .HasConstraintName("FK_Department_Instructor");
        });

        modelBuilder.Entity<FullNameCourse>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("FullNameCOURSE");

            entity.Property(e => e.CrsName)
                .HasMaxLength(50)
                .HasColumnName("Crs_Name");
            entity.Property(e => e.FullName).HasMaxLength(61);
        });

        modelBuilder.Entity<InsNameDepName>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("INS_Name_Dep_Name");

            entity.Property(e => e.DeptName)
                .HasMaxLength(50)
                .HasColumnName("Dept_Name");
            entity.Property(e => e.InsName)
                .HasMaxLength(50)
                .HasColumnName("Ins_Name");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.InsId);

            entity.ToTable("Instructor");

            entity.Property(e => e.InsId)
                .ValueGeneratedNever()
                .HasColumnName("Ins_Id");
            entity.Property(e => e.DeptId).HasColumnName("Dept_Id");
            entity.Property(e => e.InsDegree)
                .HasMaxLength(50)
                .HasColumnName("Ins_Degree");
            entity.Property(e => e.InsName)
                .HasMaxLength(50)
                .HasColumnName("Ins_Name");
        });

        modelBuilder.Entity<MangerNameTopic>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("manger_name_topic");

            entity.Property(e => e.IsNa)
                .HasMaxLength(50)
                .HasColumnName("is_Na");
            entity.Property(e => e.ToNa)
                .HasMaxLength(50)
                .HasColumnName("To_Na");
        });

        modelBuilder.Entity<V1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V1");

            entity.Property(e => e.DeptId).HasColumnName("Dept_Id");
            entity.Property(e => e.StAddress)
                .HasMaxLength(100)
                .HasColumnName("St_Address");
            entity.Property(e => e.StAge).HasColumnName("St_Age");
            entity.Property(e => e.StFname)
                .HasMaxLength(50)
                .HasColumnName("St_Fname");
            entity.Property(e => e.StId).HasColumnName("St_Id");
            entity.Property(e => e.StLname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("St_Lname");
            entity.Property(e => e.StSuper).HasColumnName("St_super");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
