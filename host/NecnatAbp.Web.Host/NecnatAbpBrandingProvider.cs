using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace NecnatAbp;

[Dependency(ReplaceServices = true)]
public class NecnatAbpBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "NecnatAbp";
}
