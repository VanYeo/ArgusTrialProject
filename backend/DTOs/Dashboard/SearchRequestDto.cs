namespace backend.DTOs.Dashboard
{
    public class SearchRequestDto
    {
        public string? Keyword { get; set; }
        public List<string>? SelectedFields { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<string>? SelectedFilters { get; set; } = [];
    }
}
