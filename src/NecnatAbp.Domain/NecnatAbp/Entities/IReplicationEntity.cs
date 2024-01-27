namespace NecnatAbp.Entities
{
    public interface IReplicationEntity<TKey>
    {
        TKey ReplicationId { get; }
    }
}
