using Microsoft.EntityFrameworkCore;
using VideoMonitoring.Domain.Entities;
using VideoMonitoring.Infra.Data.Mappings;

namespace VideoMonitoring.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Server> Servers { get; set; }
        public DbSet<Video> Videos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=[YOUR_SERVER]; initial catalog=VideoMonitoring; user id=[YOUR_DB_USER]; password=[YOUR_PASSWORD];");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Server>(new ServerMap().Configure);
            modelBuilder.Entity<Video>(new VideoMap().Configure);
        }
    }
}
