using ByteBuoy.Domain.Entities;
using ByteBuoy.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ByteBuoy.Infrastructure.Data
{
    public class ByteBuoyDbContext: DbContext
    {
        public ByteBuoyDbContext(DbContextOptions<ByteBuoyDbContext> options)
       : base(options)
        {
        }

        public DbSet<JobRun> JobRuns { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<Incident> Incidents { get; set; }
		public DbSet<JobRunCheckpoint> JobRunCheckpoints { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Metric>()
				.Property(e => e.Status)
				.HasConversion(
					v => v.ToString(),
					v => (MetricStatus)Enum.Parse(typeof(MetricStatus), v));
		}
	}
}