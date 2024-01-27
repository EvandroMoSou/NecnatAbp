using NecnatAbp.Samples;
using Xunit;

namespace NecnatAbp.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<NecnatAbpMongoDbTestModule>
{

}
