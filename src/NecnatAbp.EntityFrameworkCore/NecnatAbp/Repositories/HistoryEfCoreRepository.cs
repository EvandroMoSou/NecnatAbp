using Microsoft.EntityFrameworkCore;
using NecnatAbp.Entities;
using NecnatAbp.Enums;
using NecnatAbp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NecnatAbp.Repositories
{
    public class HistoryEfCoreRepository<TDbContext, TEntity, THistoryEntity, TKey, THistoryRepository> : EfCoreRepository<TDbContext, TEntity, TKey>
        where TDbContext : IEfCoreDbContext
        where TEntity : class, IEntity<TKey>
        where THistoryEntity : class, IHistoryEntity<TKey>
        where THistoryRepository : IRepository<THistoryEntity>
    {
        THistoryRepository _historyRepository;

        public HistoryEfCoreRepository(
            IDbContextProvider<TDbContext> dbContextProvider,
            THistoryRepository historyRepository) : base(dbContextProvider)
        {
            _historyRepository = historyRepository;
        }

        public override async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            var result = await base.InsertAsync(entity, false, cancellationToken);

            var clone = JsonUtil.CloneTo<TEntity, THistoryEntity>(entity);
            clone.HistoryId = result.Id;
            clone.SqlCommandType = SqlCommandType.Insert;
            await _historyRepository.InsertAsync(clone, false, cancellationToken);

            if (autoSave)
            {
                await dbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }

            return result;
        }

        public override Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();

            //var dbContext = await GetDbContextAsync();

            //await base.InsertManyAsync(entities, false, cancellationToken);

            //var cloneEntities = new List<THistoryEntity>();
            //foreach (var entity in entities)
            //{
            //    var clone = JsonUtil.CloneTo<TEntity, THistoryEntity>(entity);
            //    clone.HistoryId = entity.Id;
            //    clone.SqlCommandType = SqlCommandType.Insert;

            //    cloneEntities.Add(clone);
            //}
            //await _historyRepository.InsertManyAsync(cloneEntities, false, cancellationToken);

            //if (autoSave)
            //{
            //    await dbContext.SaveChangesAsync(cancellationToken);
            //}
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            var result = await base.UpdateAsync(entity, false, cancellationToken);

            var clone = JsonUtil.CloneTo<TEntity, THistoryEntity>(entity);
            clone.HistoryId = result.Id;
            clone.SqlCommandType = SqlCommandType.Update;
            await _historyRepository.InsertAsync(clone, false, cancellationToken);

            if (autoSave)
            {
                await dbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }

            return result;
        }

        public override Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();

            //var dbContext = await GetDbContextAsync();

            //await base.UpdateManyAsync(entities, false, cancellationToken);

            //var cloneEntities = new List<THistoryEntity>();
            //foreach (var entity in entities)
            //{
            //    var clone = JsonUtil.CloneTo<TEntity, THistoryEntity>(entity);
            //    clone.HistoryId = entity.Id;
            //    clone.SqlCommandType = SqlCommandType.Update;

            //    cloneEntities.Add(clone);
            //}
            //await _historyRepository.InsertManyAsync(cloneEntities, false, cancellationToken);

            //if (autoSave)
            //{
            //    await dbContext.SaveChangesAsync(cancellationToken);
            //}
        }

        public override async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            var clone = JsonUtil.CloneTo<TEntity, THistoryEntity>(entity);
            clone.HistoryId = entity.Id;
            clone.SqlCommandType = SqlCommandType.Delete;
            await _historyRepository.InsertAsync(clone, false, cancellationToken);

            await base.DeleteAsync(entity, false, cancellationToken);

            if (autoSave)
            {
                await dbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
        }

        public override async Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            var cloneEntities = new List<THistoryEntity>();
            foreach (var entity in entities)
            {
                var clone = JsonUtil.CloneTo<TEntity, THistoryEntity>(entity);
                clone.HistoryId = entity.Id;
                clone.SqlCommandType = SqlCommandType.Delete;

                cloneEntities.Add(clone);
            }
            await _historyRepository.InsertManyAsync(cloneEntities, false, cancellationToken);

            await base.DeleteManyAsync(entities, false, cancellationToken);

            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public override async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();
            var dbSet = dbContext.Set<TEntity>();

            var entities = await dbSet
                .Where(predicate)
                .ToListAsync(GetCancellationToken(cancellationToken));

            var cloneEntities = new List<THistoryEntity>();
            foreach (var entity in entities)
            {
                var clone = JsonUtil.CloneTo<TEntity, THistoryEntity>(entity);
                clone.HistoryId = entity.Id;
                clone.SqlCommandType = SqlCommandType.Delete;

                cloneEntities.Add(clone);
            }
            await _historyRepository.InsertManyAsync(cloneEntities, false, cancellationToken);

            await DeleteManyAsync(entities, false, cancellationToken);

            if (autoSave)
            {
                await dbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
        }

        public override Task DeleteDirectAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();
        }
    }
}
