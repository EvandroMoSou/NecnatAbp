using NecnatAbp.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;


namespace NecnatAbp.AppServices
{
    public abstract class CrudsAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TSearchInput>
        : CrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
        where TSearchInput : OptionalPagedAndSortedResultRequestDto, TGetListInput
    {
        protected CrudsAppService(IRepository<TEntity, TKey> repository)
            : base(repository)
        {
        }

        protected virtual async Task<IQueryable<TEntity>> CreateFilteredQuerySearchAsync(TSearchInput input)
        {
            return await ReadOnlyRepository.GetQueryableAsync();
        }

        public virtual async Task<PagedResultDto<TEntityDto>> SearchAsync(TSearchInput input)
        {
            await CheckGetListPolicyAsync();

            var query = await CreateFilteredQuerySearchAsync(input);
            var totalCount = await AsyncExecuter.CountAsync(query);

            var entities = new List<TEntity>();
            var entityDtos = new List<TEntityDto>();

            if (totalCount > 0)
            {
                query = ApplySorting(query, input);
                if (input.IsPaged)
                    query = ApplyPaging(query, input);

                entities = await AsyncExecuter.ToListAsync(query);
                entityDtos = await MapToGetListOutputDtosAsync(entities);
            }

            return new PagedResultDto<TEntityDto>(
                totalCount,
                entityDtos
            );
        }
    }
}
