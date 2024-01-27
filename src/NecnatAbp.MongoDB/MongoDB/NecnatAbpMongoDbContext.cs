using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace NecnatAbp.MongoDB;

[ConnectionStringName(NecnatAbpDbProperties.ConnectionStringName)]
public class NecnatAbpMongoDbContext : AbpMongoDbContext, INecnatAbpMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureNecnatAbp();
    }
}
