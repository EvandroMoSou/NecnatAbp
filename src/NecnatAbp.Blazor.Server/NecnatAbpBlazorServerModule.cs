using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace NecnatAbp.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(NecnatAbpBlazorModule)
    )]
public class NecnatAbpBlazorServerModule : AbpModule
{

}
