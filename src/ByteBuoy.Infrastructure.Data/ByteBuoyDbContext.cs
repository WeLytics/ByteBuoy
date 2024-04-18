using ByteBuoy.Domain.Entities;
using ByteBuoy.Domain.Entities.Identity;
using ByteBuoy.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ByteBuoy.Infrastructure.Data
{
	public class ByteBuoyDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
	{
		public ByteBuoyDbContext(DbContextOptions<ByteBuoyDbContext> options)
	   : base(options)
		{
		}

		public DbSet<Job> Jobs { get; set; }
		public DbSet<JobHistory> JobHistory { get; set; }
		public DbSet<Page> Pages { get; set; }
		public DbSet<Metric> Metrics { get; set; }
		public DbSet<MetricGroup> MetricGroups { get; set; }
		public DbSet<Incident> Incidents { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			string schema = "Identity";

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users", schema: schema);
            });
            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "Roles", schema: schema);
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>(entity =>
            {
                entity.ToTable(name: "UserRoles", schema: schema);
            });
            modelBuilder.Entity<IdentityUserClaim<Guid>>(entity =>
            {
                entity.ToTable(name: "UserClaims", schema: schema);
            });
            modelBuilder.Entity<IdentityUserLogin<Guid>>(entity =>
            {
                entity.ToTable(name: "UserLogins", schema: schema);
            });
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity =>
            {
                entity.ToTable(name: "RoleClaims", schema: schema);
            });
            modelBuilder.Entity<IdentityUserToken<Guid>>(entity =>
            {
                entity.ToTable(name: "UserTokens", schema: schema);
            });

            modelBuilder
				.Entity<Metric>()
				.Property(e => e.Status)
				.HasConversion(
					v => v.ToString(),
					v => (MetricStatus)Enum.Parse(typeof(MetricStatus), v));

			modelBuilder
				.Entity<Job>()
				.Property(e => e.Status)
				.HasConversion(
					v => v.ToString(),
					v => (JobStatus)Enum.Parse(typeof(JobStatus), v));

			modelBuilder
				.Entity<JobHistory>()
				.Property(e => e.Status)
				.HasConversion(
					v => v.ToString(),
					v => (Domain.Enums.TaskStatus)Enum.Parse(typeof(Domain.Enums.TaskStatus), v));

			modelBuilder
				.Entity<MetricGroup>()
				.Property(e => e.MetricInterval)
				.HasConversion(
					v => v.ToString(),
					v => (MetricInterval)Enum.Parse(typeof(MetricInterval), v));

		}
	}
}
