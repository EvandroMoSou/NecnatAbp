namespace NecnatAbp.Dtos
{
    public class AutocompleteResultRequestDto : OptionalPagedAndSortedResultRequestDto
    {
        public string? GenericSearch { get; set; }
    }
}
