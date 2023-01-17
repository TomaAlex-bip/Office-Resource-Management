#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using Microsoft.EntityFrameworkCore;
using Project3Api.Core.Entities;
using Project3Api.Data.EntityConfiguration;

namespace Project3Api.Data
{
    public class ProjectDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Desk> Desks { get; set; }

        public DbSet<Log> Logs { get; set; }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
            modelBuilder.ApplyConfiguration<Desk>(new DeskConfiguration());
            modelBuilder.ApplyConfiguration<DeskAllocation>(new DeskAllocationConfiguration());
            modelBuilder.ApplyConfiguration<Resource>(new ResourceConfiguration());
            modelBuilder.ApplyConfiguration<ResourceAllocation>(new ResourceAllocationConfiguration());
            modelBuilder.ApplyConfiguration<Log>(new LogConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}