using NecnatAbp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace NecnatAbp.Permissions;

public class NecnatAbpPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(NecnatAbpPermissions.GroupName, L("Permission:NecnatAbp"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<NecnatAbpResource>(name);
    }
}
