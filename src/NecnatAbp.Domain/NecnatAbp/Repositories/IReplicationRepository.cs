using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Repositories
{
    public interface IReplicationRepository<TBaseEntity, TReplicationEntity, TReplicationKey> : IRepository
            where TBaseEntity : class
            where TReplicationEntity : class
    {
        Task<IEnumerable<TReplicationEntity>> GetListAsync(IDbConnection? con = null, IDbTransaction? tran = null);
        Task<TReplicationEntity?> GetReplicationAsync(TBaseEntity eNovoSiga, IDbConnection? con = null, IDbTransaction? tran = null);
        Task<TReplicationEntity?> InsertAsync(TReplicationEntity eReplication, bool returnObject = false, IDbConnection? con = null, IDbTransaction? tran = null);
        Task<TReplicationEntity?> UpdateAsync(TReplicationEntity eReplication, bool returnObject = false, IDbConnection? con = null, IDbTransaction? tran = null);
        Task<int> DeleteAsync(TReplicationEntity eReplication, IDbConnection? con = null, IDbTransaction? tran = null);
        Task<int> DeleteAsync(TBaseEntity eBase, IDbConnection? con = null, IDbTransaction? tran = null);
        Task<int> DeleteAsync(TReplicationKey replicationId, IDbConnection? con = null, IDbTransaction? tran = null);
    }
}
