using NecnatAbp.Enums;
using Volo.Abp.Domain.Entities;

namespace NecnatAbp.Entities
{
    public interface IHistoryEntity<TKey> : IEntity<TKey>
    {
        TKey HistoryId { get; set; }
        SqlCommandType SqlCommandType { get; set; }
    }
}
