using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace NecnatAbp.EntityFrameworkCore;

[ConnectionStringName(NecnatAbpDbProperties.ConnectionStringName)]
public class NecnatAbpDbContext : AbpDbContext<NecnatAbpDbContext>, INecnatAbpDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public NecnatAbpDbContext(DbContextOptions<NecnatAbpDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureNecnatAbp();
    }
}
