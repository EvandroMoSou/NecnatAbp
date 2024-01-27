using Volo.Abp.Modularity;

namespace NecnatAbp;

[DependsOn(
    typeof(NecnatAbpApplicationModule),
    typeof(NecnatAbpDomainTestModule)
    )]
public class NecnatAbpApplicationTestModule : AbpModule
{

}
