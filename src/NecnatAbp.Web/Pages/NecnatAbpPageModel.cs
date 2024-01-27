using NecnatAbp.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace NecnatAbp.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class NecnatAbpPageModel : AbpPageModel
{
    protected NecnatAbpPageModel()
    {
        LocalizationResourceType = typeof(NecnatAbpResource);
        ObjectMapperContext = typeof(NecnatAbpWebModule);
    }
}
