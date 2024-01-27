using Volo.Abp.Modularity;

namespace NecnatAbp;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class NecnatAbpDomainTestBase<TStartupModule> : NecnatAbpTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
