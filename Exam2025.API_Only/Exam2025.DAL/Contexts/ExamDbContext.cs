using Exam2025.DAL.BaseEntity;
using Exam2025.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2025.DAL.Contexts
{
    public class ExamDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        //public ExamDbContext()
        //{
        //    ChangeTracker.LazyLoadingEnabled = false;
        //}
        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer("server=.; database= DemoDb; integrated security= true;");

        public DbSet<AnswerTBL> AnswerTBLs { get; set; }
        public DbSet<ExamTBL> ExamTBLs { get; set; }
        public DbSet<QuestionTBL> QuestionTBLs { get; set; }
        public DbSet<UserExamTBL> UserExamTBLs { get; set; }
        public DbSet<UserExamDetailTBL> UserExamDetailTBLs { get; set; }
        public DbSet<AppErrorTBL> AppErrorTBLs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("BDataSchema");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().ToTable("Users", "ASecurity");
            modelBuilder.Entity<AppRole>().ToTable("Roles", "ASecurity");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "ASecurity");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "ASecurity");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "ASecurity");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "ASecurity");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "ASecurity");
        }
    }
}
