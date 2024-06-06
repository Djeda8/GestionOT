using DOU.GestionOT.BL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DOU.GestionOT.DAL
{
    public class GestionOTContext : DbContext
    {
        public GestionOTContext(DbContextOptions<GestionOTContext> options)
            : base(options)
        {
        }

        public DbSet<Ot> Ot { get; set; } = default!;

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GestionOTContext>
        {
            public GestionOTContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../DOU.GestionOT.WEBI/appsettings.json").Build();
                var builder = new DbContextOptionsBuilder<GestionOTContext>();
                var connectionString = configuration.GetConnectionString("DatabaseConnection");
                builder.UseSqlServer(connectionString);
                return new GestionOTContext(builder.Options);
            }
        }
    }
}
