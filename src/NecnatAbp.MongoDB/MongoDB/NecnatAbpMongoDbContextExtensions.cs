using Volo.Abp;
using Volo.Abp.MongoDB;

namespace NecnatAbp.MongoDB;

public static class NecnatAbpMongoDbContextExtensions
{
    public static void ConfigureNecnatAbp(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
