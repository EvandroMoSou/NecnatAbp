using NecnatAbp.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace NecnatAbp.Pages;

public abstract class NecnatAbpPageModel : AbpPageModel
{
    protected NecnatAbpPageModel()
    {
        LocalizationResourceType = typeof(NecnatAbpResource);
    }
}
