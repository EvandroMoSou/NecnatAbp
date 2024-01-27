using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace NecnatAbp;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(NecnatAbpDomainSharedModule)
)]
public class NecnatAbpDomainModule : AbpModule
{

}
