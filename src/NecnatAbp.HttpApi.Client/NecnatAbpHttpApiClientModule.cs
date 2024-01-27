using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace NecnatAbp;

[DependsOn(
    typeof(NecnatAbpApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class NecnatAbpHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(NecnatAbpApplicationContractsModule).Assembly,
            NecnatAbpRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<NecnatAbpHttpApiClientModule>();
        });

    }
}
