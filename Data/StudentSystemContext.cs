using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data
{
    public partial class StudentSystemContext: DbContext
    {

        public StudentSystemContext()
        {

        }
        public StudentSystemContext(DbContextOptions<StudentSystemContext> options)
            :base(options)
        {

        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Homework> HomeworkSubmissions { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server =.\SQLEXPRESS; Database = StudentSystem; Integrated Security = True;");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.HasOne(h => h.Student).WithMany(s => s.HomeworkSubmissions);

                entity.Property(p => p.Content).IsUnicode(false);

                entity.ToTable("HomeworkSubmissions");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.Property(p => p.Name).IsUnicode().HasMaxLength(50);

                entity.Property(p => p.Url).IsUnicode(false);

            });


            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(p => p.Name).IsUnicode().HasMaxLength(80).IsRequired(true);

                entity.Property(p => p.Description).IsUnicode(false).IsRequired(false);

            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(p => p.Name).IsUnicode().HasMaxLength(100);

                entity.Property(p => p.PhoneNumber).IsUnicode(false).IsRequired(false).HasMaxLength(10).IsFixedLength(true);

                entity.Property(p => p.Birthday).IsRequired(false);


            });


        }

        // TO DO: FINISH CONTEXT
    }
}
