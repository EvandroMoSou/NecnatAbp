using Volo.Abp.Application.Dtos;

namespace NecnatAbp.Dtos
{
    public class OptionalPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        public bool IsPaged { get; set; } = true;
    }
}
