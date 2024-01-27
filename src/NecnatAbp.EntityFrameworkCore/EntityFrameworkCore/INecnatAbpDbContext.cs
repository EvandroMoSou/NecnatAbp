using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace NecnatAbp.EntityFrameworkCore;

[ConnectionStringName(NecnatAbpDbProperties.ConnectionStringName)]
public interface INecnatAbpDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
