using NecnatAbp.Localization;
using Volo.Abp.AspNetCore.Components;

namespace NecnatAbp.Blazor.Server.Host;

public abstract class NecnatAbpComponentBase : AbpComponentBase
{
    protected NecnatAbpComponentBase()
    {
        LocalizationResource = typeof(NecnatAbpResource);
    }
}
