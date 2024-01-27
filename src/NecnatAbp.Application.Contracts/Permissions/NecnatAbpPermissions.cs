using Volo.Abp.Reflection;

namespace NecnatAbp.Permissions;

public class NecnatAbpPermissions
{
    public const string GroupName = "NecnatAbp";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(NecnatAbpPermissions));
    }
}
