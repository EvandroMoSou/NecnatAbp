using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NecnatAbp.AppServices
{
    public interface ICrudsAppService<TEntityDto, in TKey, in TGetListInput, in TCreateInput, TSearchInput> : ICrudAppService<TEntityDto, TKey, TGetListInput, TCreateInput>
    {
        Task<PagedResultDto<TEntityDto>> SearchAsync(TSearchInput input);
    }
}
