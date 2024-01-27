using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace NecnatAbp;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(NecnatAbpHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class NecnatAbpConsoleApiClientModule : AbpModule
{

}
