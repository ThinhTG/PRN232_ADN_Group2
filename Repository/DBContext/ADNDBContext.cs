using ADN_Group2.BusinessObject.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using System;

namespace Repository.DBContext
{
	public class ADNDbContext : IdentityDbContext<
		ApplicationUser,
		ApplicationRole,
		Guid,
		ApplicationUserClaims,
		ApplicationUserRole,
		ApplicationUserLogins,
		ApplicationRoleClaims,
		ApplicationUserTokens>
	{
		public ADNDbContext(DbContextOptions<ADNDbContext> options)
			: base(options)
		{
			//this.Database.Migrate();
        }
		// user
		public virtual DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
		public virtual DbSet<ApplicationRole> ApplicationRoles => Set<ApplicationRole>();
		public virtual DbSet<ApplicationUserClaims> ApplicationUserClaims => Set<ApplicationUserClaims>();
		public virtual DbSet<ApplicationUserRole> ApplicationUserRoles => Set<ApplicationUserRole>();
		public virtual DbSet<ApplicationUserLogins> ApplicationUserLogins => Set<ApplicationUserLogins>();
		public virtual DbSet<ApplicationRoleClaims> ApplicationRoleClaims => Set<ApplicationRoleClaims>();
		public virtual DbSet<ApplicationUserTokens> ApplicationUserTokens => Set<ApplicationUserTokens>();
		public DbSet<Payment> Payments => Set<Payment>();
		public DbSet<Appointment> Appointments => Set<Appointment>();
		public DbSet<TestResult> TestResults => Set<TestResult>();
		public DbSet<Service> Services => Set<Service>();
		public DbSet<Blog> Blogs => Set<Blog>();
		public DbSet<Feedback> Feedbacks => Set<Feedback>();
		public DbSet<Kit> Kits => Set<Kit>();
		public DbSet<Sample> Samples => Set<Sample>();
		public DbSet<TestPerson> TestPersons => Set<TestPerson>();
		public DbSet<Address> Addresses => Set<Address>();



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ApplicationUserRole>()
					.HasOne(ur => ur.User)
					.WithMany(u => u.UserRoles)
					.HasForeignKey(ur => ur.UserId)
					.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ApplicationUserRole>(b =>
			{
				b.HasKey(ur => new { ur.UserId, ur.RoleId });

				b.HasOne(ur => ur.User)
					.WithMany(u => u.UserRoles)
					.HasForeignKey(ur => ur.UserId)
					.OnDelete(DeleteBehavior.Cascade);

				b.HasOne(ur => ur.Role)
					.WithMany()
					.HasForeignKey(ur => ur.RoleId)
					.OnDelete(DeleteBehavior.NoAction);
			});
            modelBuilder.Entity<Payment>()
			   .Property(p => p.Amount)
			   .HasPrecision(18, 2); // 18 chữ số tổng, 2 chữ số sau dấu phẩy

            modelBuilder.Entity<Service>()
                .Property(s => s.Price)
                .HasPrecision(18, 2);
            // Appointment
            modelBuilder.Entity<Appointment>(entity =>
			{
				entity.HasKey(a => a.AppointmentId);
				entity.HasOne(a => a.User)
					  .WithMany(u => u.Appointments)
					  .HasForeignKey(a => a.UserId)
					  .OnDelete(DeleteBehavior.Cascade);
				entity.HasOne(a => a.Service)
					  .WithMany(s => s.Appointments)
					  .HasForeignKey(a => a.ServiceId)
					  .OnDelete(DeleteBehavior.Restrict);
			});

			// Payment
			modelBuilder.Entity<Payment>(entity =>
			{
				entity.HasKey(p => p.PaymentId);
				entity.HasOne(p => p.Appointment)
					  .WithMany(a => a.Payments)
					  .HasForeignKey(p => p.AppointmentId)
					  .OnDelete(DeleteBehavior.Cascade);
			});

			// TestResult
			modelBuilder.Entity<TestResult>(entity =>
			{
				entity.HasKey(tr => tr.ResultId);
				entity.HasOne(tr => tr.Appointment)
					  .WithMany(a => a.TestResults)
					  .HasForeignKey(tr => tr.AppointmentId)
					  .OnDelete(DeleteBehavior.Cascade);
			});

			// Service
			modelBuilder.Entity<Service>(entity =>
			{
				entity.HasKey(s => s.ServiceId);
				entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
				entity.Property(s => s.Description).HasMaxLength(500);
				entity.Property(s => s.Type).HasMaxLength(50);
			});

			// Blog
			modelBuilder.Entity<Blog>(entity =>
			{
				entity.HasKey(b => b.BlogId);
				entity.HasOne(b => b.User)
					  .WithMany(u => u.Blogs)
					  .HasForeignKey(b => b.UserId)
					  .OnDelete(DeleteBehavior.Cascade);
			});

			// Feedback
			modelBuilder.Entity<Feedback>(entity =>
			{
				entity.HasKey(f => f.FeedbackId);
				entity.HasOne(f => f.User)
					  .WithMany(u => u.Feedbacks)
					  .HasForeignKey(f => f.UserId)
					  .OnDelete(DeleteBehavior.Cascade);
				entity.HasOne(f => f.Service)
					  .WithMany(s => s.Feedbacks)
					  .HasForeignKey(f => f.ServiceId)
					  .OnDelete(DeleteBehavior.Cascade);
				entity.HasOne(f => f.Appointment)
					  .WithMany(a => a.Feedbacks)
					  .HasForeignKey(f => f.AppointmentId)
					  .OnDelete(DeleteBehavior.Cascade);
			});

			// Kit
			modelBuilder.Entity<Kit>(entity =>
			{
				entity.HasKey(k => k.KitId);
				entity.HasOne(k => k.Appointment)
					  .WithMany(a => a.Kits)
					  .HasForeignKey(k => k.AppointmentId)
					  .OnDelete(DeleteBehavior.Cascade);
			});

			// Sample
			modelBuilder.Entity<Sample>(entity =>
			{
				entity.HasKey(s => s.SampleId);
				entity.HasOne(s => s.Kit)
					  .WithMany(k => k.Samples)
					  .HasForeignKey(s => s.KitId)
					  .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(s => s.Person)
					.WithOne()
					.HasForeignKey<Sample>(s => s.PersonId)
					.OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(s => s.PersonId)
                    .IsUnique();
            });

			// TestPerson
			modelBuilder.Entity<TestPerson>(entity =>
			{
				entity.HasKey(tp => tp.PersonId);
				entity.HasOne(tp => tp.Appointment)
					  .WithMany()
					  .HasForeignKey(tp => tp.AppointmentId)
					  .OnDelete(DeleteBehavior.Cascade);
			});

			// Address
			modelBuilder.Entity<Address>(entity =>
			{
				entity.HasKey(a => a.AddressId);
				entity.HasOne(a => a.User)
					  .WithMany(u => u.Addresses)
					  .HasForeignKey(a => a.UserId)
					  .OnDelete(DeleteBehavior.Cascade);
			});
		}
	}
}
