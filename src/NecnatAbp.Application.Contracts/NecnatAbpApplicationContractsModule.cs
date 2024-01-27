using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace NecnatAbp;

[DependsOn(
    typeof(NecnatAbpDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class NecnatAbpApplicationContractsModule : AbpModule
{

}
