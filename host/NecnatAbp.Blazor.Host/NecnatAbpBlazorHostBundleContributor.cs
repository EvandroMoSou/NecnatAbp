using Volo.Abp.Bundling;

namespace NecnatAbp.Blazor.Host;

public class NecnatAbpBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
