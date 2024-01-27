using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NecnatAbp.EntityFrameworkCore;

public class NecnatAbpHttpApiHostMigrationsDbContext : AbpDbContext<NecnatAbpHttpApiHostMigrationsDbContext>
{
    public NecnatAbpHttpApiHostMigrationsDbContext(DbContextOptions<NecnatAbpHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureNecnatAbp();
    }
}
