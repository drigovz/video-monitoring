using VideoMonitoring.Infra.Data.Context;

namespace VideoMonitoring.Infra.Data.Seeding
{
    public static class DatabaseGenerator
    {
        public static void Seed()
        {
            using (var context = new AppDbContext())
            {
                if (!context.Database.EnsureCreated())
                    context.Database.EnsureCreated();
            }
        }
    }
}
