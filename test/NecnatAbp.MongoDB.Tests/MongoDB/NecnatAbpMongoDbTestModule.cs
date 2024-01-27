using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace NecnatAbp.MongoDB;

[DependsOn(
    typeof(NecnatAbpApplicationTestModule),
    typeof(NecnatAbpMongoDbModule)
)]
public class NecnatAbpMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
