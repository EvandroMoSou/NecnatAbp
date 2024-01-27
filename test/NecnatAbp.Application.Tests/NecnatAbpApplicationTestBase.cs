using Volo.Abp.Modularity;

namespace NecnatAbp;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class NecnatAbpApplicationTestBase<TStartupModule> : NecnatAbpTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
