using Volo.Abp.Modularity;

namespace NecnatAbp;

[DependsOn(
    typeof(NecnatAbpDomainModule),
    typeof(NecnatAbpTestBaseModule)
)]
public class NecnatAbpDomainTestModule : AbpModule
{

}
