using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NecnatAbp.EntityFrameworkCore;

public class NecnatAbpHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<NecnatAbpHttpApiHostMigrationsDbContext>
{
    public NecnatAbpHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<NecnatAbpHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("NecnatAbp"));

        return new NecnatAbpHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
