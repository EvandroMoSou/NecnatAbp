using NecnatAbp.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Entities
{
    public class HistoryFullAuditedAggregateRoot<TKey> : FullAuditedAggregateRoot<TKey>, IHistoryEntity<TKey>
        where TKey : struct
    {
        public virtual TKey HistoryId { get; set; }
        public virtual SqlCommandType SqlCommandType { get; set; }
    }
}
