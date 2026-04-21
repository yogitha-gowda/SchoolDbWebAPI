using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolDBCodeFirstApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBCoreWebAPI.Models
{
    public class SchoolDBContext : DbContext
    {
        // public string _ConStr;

        public SchoolDBContext()
        {
        }
        /* public SchoolDBContext(string ConStr)
         {
             _ConStr = ConStr;
         }
        */
        public SchoolDBContext(DbContextOptions<SchoolDBContext> options) : base(options)
        {

        }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /* if (!optionsBuilder.IsConfigured)
                 //optionsBuilder.UseSqlServer("server=EC2AMAZ-EHR6SVV; Database=SchoolDb; Integrated Security=True; Trusted_Connection=True ;TrustServerCertificate=True;");
                 optionsBuilder.UseSqlServer(_ConStr);*/
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>(entity => {
                entity.HasKey(e => e.GradeId).HasName("PK__Grade");
                entity.ToTable("grades", "School");
            });


            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Studentid).HasName("PK__Student");

                entity.ToTable("students", "School");
                entity.HasOne(s => s.grade).WithMany(g => g.Students)
                .HasForeignKey(s => s.GradeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__grades__students");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UsertId).HasName("PK__User");
                entity.ToTable("users", "School");

                entity.HasData(
                    new User
                    {
                        UsertId = 1,
                        FullName = "Pranaya Rout",
                        Email ="pranaya.rout@teksystems.com",
                        PassWordHash = PasswordHasher.HashPassword("Pranaya@123"),
                        Role = "Adminstrator,Manager"
                    },
                    new User
                    {
                        UsertId = 2,
                        FullName = "John Doe",
                        Email = "john.doe@teksystems.com",
                        PassWordHash = PasswordHasher.HashPassword("JohnDoe@123"),
                        Role = "Administrator"
                    },
                    new User
                    {
                        UsertId = 3,
                        FullName = "Jane Smith",
                        Email = "jane.smith@teksystems.com",
                        PassWordHash = PasswordHasher.HashPassword("Jane@123"),
                        Role = "Manager"
                    });
            });
         }
    }
}

