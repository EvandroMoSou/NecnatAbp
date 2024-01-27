using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace NecnatAbp.AppServices
{
    public interface IGetAndSearchAppService<TEntityDto, TKey, TSearchInput>
    {
        Task<TEntityDto> GetAsync(TKey id);
        Task<PagedResultDto<TEntityDto>> SearchAsync(TSearchInput input);
    }
}
