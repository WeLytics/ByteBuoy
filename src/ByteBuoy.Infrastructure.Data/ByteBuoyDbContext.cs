using ByteBuoy.Domain.Entities;
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
        public DbSet<JobRunCheckpoint> Checkpoints { get; set; }
    }
}
