using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace NecnatAbp;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class NecnatAbpInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<NecnatAbpInstallerModule>();
        });
    }
}
