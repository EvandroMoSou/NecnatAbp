using NecnatAbp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace NecnatAbp;

public abstract class NecnatAbpController : AbpControllerBase
{
    protected NecnatAbpController()
    {
        LocalizationResource = typeof(NecnatAbpResource);
    }
}
