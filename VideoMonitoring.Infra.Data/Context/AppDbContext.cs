using Microsoft.EntityFrameworkCore;
using VideoMonitoring.Domain.Entities;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost,1433; initial catalog=VideoMonitoring; user id=sa; password=sa12345@BD; Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
