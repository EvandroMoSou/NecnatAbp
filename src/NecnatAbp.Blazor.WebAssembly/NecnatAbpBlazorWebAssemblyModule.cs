using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace NecnatAbp.Blazor.WebAssembly;

[DependsOn(
    typeof(NecnatAbpBlazorModule),
    typeof(NecnatAbpHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class NecnatAbpBlazorWebAssemblyModule : AbpModule
{

}
