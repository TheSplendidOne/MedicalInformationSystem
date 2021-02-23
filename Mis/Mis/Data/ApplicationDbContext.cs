using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Mis.Models;

namespace Mis.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DoctorInfo> DoctorInfo { get; set; }

        public DbSet<Hospital> Hospital { get; set; }

        public DbSet<MeetingCell> MeetingCell { get; set; }

        public DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            builder.Entity<DoctorInfo>()
                .HasOne(info => info.User)
                .WithOne(user => user.DoctorInfo)
                .HasForeignKey<DoctorInfo>(info => info.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<MeetingCell>()
                .HasOne(cell => cell.Doctor)
                .WithMany(doctor => doctor.MeetingCells)
                .HasForeignKey(cell => cell.DoctorId);

            builder.Entity<MeetingCell>()
                .HasOne(cell => cell.Patient)
                .WithMany(patient => patient.MeetingCells)
                .HasForeignKey(cell => cell.PatientId);

            builder.Entity<Comment>()
                .HasOne(comment => comment.Author)
                .WithMany(author => author.AuthorsComments)
                .HasForeignKey(comment => comment.AuthorId);

            builder.Entity<Comment>()
                .HasOne(comment => comment.Owner)
                .WithMany(author => author.ProfileComments)
                .HasForeignKey(comment => comment.OwnerId);
        }
    }
}
