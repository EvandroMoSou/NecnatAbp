using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace NecnatAbp.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class NecnatAbpBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "NecnatAbp";
}
