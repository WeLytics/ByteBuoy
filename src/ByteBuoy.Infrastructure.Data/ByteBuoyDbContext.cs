using ByteBuoy.Domain.Entities;
using ByteBuoy.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ByteBuoy.Infrastructure.Data
{
	public class ByteBuoyDbContext : DbContext
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
