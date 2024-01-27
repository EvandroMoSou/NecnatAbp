using Localization.Resources.AbpUi;
using NecnatAbp.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace NecnatAbp;

[DependsOn(
    typeof(NecnatAbpApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class NecnatAbpHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(NecnatAbpHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<NecnatAbpResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
