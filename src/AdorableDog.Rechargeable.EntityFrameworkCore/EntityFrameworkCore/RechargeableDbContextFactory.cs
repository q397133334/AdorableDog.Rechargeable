using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AdorableDog.Rechargeable.EntityFrameworkCore
{
    public class RechargeableDbContextFactory : IDesignTimeDbContextFactory<RechargeableDbContext>
    {
        public RechargeableDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<RechargeableDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new RechargeableDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../AdorableDog.Rechargeable.Web/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
