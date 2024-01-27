using NecnatAbp.Localization;
using Volo.Abp.Application.Services;

namespace NecnatAbp;

public abstract class NecnatAbpAppService : ApplicationService
{
    protected NecnatAbpAppService()
    {
        LocalizationResource = typeof(NecnatAbpResource);
        ObjectMapperContext = typeof(NecnatAbpApplicationModule);
    }
}
