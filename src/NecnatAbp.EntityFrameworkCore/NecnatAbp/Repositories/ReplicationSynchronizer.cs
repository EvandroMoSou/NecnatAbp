using NecnatAbp.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Repositories
{
    public abstract class ReplicationSynchronizer<TBaseEntity, TBaseKey, TReplicationEntity, TReplicationKey, TBaseRepository, TReplicationRepository>
        where TBaseEntity : class, IEntity<TBaseKey>, new()
        where TReplicationEntity : class, new()
        where TBaseRepository : IRepository<TBaseEntity, TBaseKey>
        where TReplicationRepository : IReplicationRepository<TBaseEntity, TReplicationEntity, TReplicationKey>
    {
        protected TBaseRepository _baseRepository;
        protected TReplicationRepository _replicationRepository;

        public ReplicationSynchronizer(
            TBaseRepository baseRepository,
            TReplicationRepository replicationRepository)
        {
            _baseRepository = baseRepository;
            _replicationRepository = replicationRepository;
        }

        protected async Task<List<TBaseEntity>> SynchronizeToBaseAsync(List<TReplicationEntity> lReplication, List<TBaseEntity> lBase)
        {
            var l = new List<TBaseEntity>();

            foreach (var iReplication in lReplication)
            {
                var eBase = await ReplicateToBaseAsync(iReplication, lBase);
                lBase = RemoveFromList(lBase, eBase);

                l.Add(eBase);
            }

            foreach (var iBase in lBase)
                await _baseRepository.DeleteAsync(iBase, true);

            return l;
        }

        protected virtual async Task<List<TReplicationEntity>> SynchronizeAsync(List<TBaseEntity> lBase, List<TReplicationEntity> lReplication)
        {
            var l = new List<TReplicationEntity>();

            foreach (var iBase in lBase)
            {
                var eReplication = await ReplicateAsync(iBase, lReplication);
                lReplication = RemoveFromList(lReplication, eReplication);

                l.Add(eReplication);
            }

            foreach (var iReplication in lReplication)
                await _replicationRepository.DeleteAsync(iReplication);

            return l;
        }

        protected virtual async Task<TBaseEntity> ReplicateToBaseAsync(TReplicationEntity eReplication, List<TBaseEntity> lBase)
        {
            var eBase = Identify(eReplication, lBase);
            return await ReplicateToBaseAsync(eReplication, eBase);
        }

        protected virtual async Task<TBaseEntity> ReplicateToBaseAsync(TReplicationEntity eReplication, TBaseEntity? eBase)
        {
            SqlCommandType? sqlCommandType = null;
            if (eBase == null)
            {
                eBase = new TBaseEntity();
                sqlCommandType = SqlCommandType.Insert;
            }

            if (sqlCommandType == null && CheckModification(eReplication, eBase))
                sqlCommandType = SqlCommandType.Update;

            eBase = Map(eReplication, eBase);

            if (sqlCommandType == SqlCommandType.Insert)
                eBase = await _baseRepository.InsertAsync(eBase, true);
            else if (sqlCommandType == SqlCommandType.Update)
                eBase = await _baseRepository.UpdateAsync(eBase, true);

            return eBase;
        }

        protected virtual async Task<TReplicationEntity> ReplicateAsync(TBaseEntity eBase, List<TReplicationEntity> lReplication)
        {
            var eReplication = Identify(eBase, lReplication);
            return (await ReplicateAsync(eBase, eReplication, true))!;
        }

        protected virtual async Task<TReplicationEntity?> ReplicateAsync(TBaseEntity eBase, TReplicationEntity? eReplication, bool returnObject = false)
        {
            SqlCommandType? sqlCommandType = null;
            if (eReplication == null)
            {
                eReplication = new TReplicationEntity();
                sqlCommandType = SqlCommandType.Insert;
            }

            if (sqlCommandType == null && CheckModification(eReplication, eBase))
                sqlCommandType = SqlCommandType.Update;

            eReplication = Map(eBase, eReplication);

            if (sqlCommandType == SqlCommandType.Insert)
                eReplication = await _replicationRepository.InsertAsync(eReplication, returnObject);
            else if (sqlCommandType == SqlCommandType.Update)
                eReplication = await _replicationRepository.UpdateAsync(eReplication, returnObject);

            return eReplication;
        }

        protected abstract TBaseEntity Map(TReplicationEntity eReplication);

        protected abstract TBaseEntity Map(TReplicationEntity eReplication, TBaseEntity eBase);

        protected abstract TReplicationEntity Map(TBaseEntity eBase);

        protected abstract TReplicationEntity Map(TBaseEntity eBase, TReplicationEntity eReplication);

        protected abstract TReplicationEntity? Identify(TBaseEntity eBase, List<TReplicationEntity> lReplication);

        protected abstract TBaseEntity? Identify(TReplicationEntity eReplication, List<TBaseEntity> lBase);

        protected abstract bool CheckModification(TReplicationEntity eReplication, TBaseEntity eBase);

        protected abstract List<TBaseEntity> RemoveFromList(List<TBaseEntity> lBase, TBaseEntity eBase);

        protected abstract List<TReplicationEntity> RemoveFromList(List<TReplicationEntity> lReplication, TReplicationEntity eReplication);

        public async virtual Task<List<TBaseEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            var lBase = await _baseRepository.GetListAsync(includeDetails, cancellationToken);
            var lReplication = await _replicationRepository.GetListAsync();

            return await SynchronizeToBaseAsync(lReplication.ToList(), lBase);
        }

        public async virtual Task<TBaseEntity> InsertAsync(TBaseEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var eReplication = await _replicationRepository.GetReplicationAsync(entity);
            await ReplicateAsync(entity, eReplication);

            return await _baseRepository.InsertAsync(entity, true, cancellationToken);
        }

        public async virtual Task<TBaseEntity> UpdateAsync(TBaseEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var eReplication = await _replicationRepository.GetReplicationAsync(entity);
            await ReplicateAsync(entity, eReplication);

            return await _baseRepository.UpdateAsync(entity, true, cancellationToken);
        }

        public async virtual Task DeleteAsync(TBaseKey id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var e = await _baseRepository.GetAsync(id);
            await DeleteAsync(e, autoSave, cancellationToken);
        }

        public async virtual Task DeleteAsync(TBaseEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await _replicationRepository.DeleteAsync(entity);
            await _baseRepository.DeleteAsync(entity, autoSave, cancellationToken);
        }
    }
}
