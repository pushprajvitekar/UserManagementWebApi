namespace UserManagement.Application.SortingPaging
{
    public record SortingPagingDto(string? SortBy = null, bool SortAsc = false, int PageNumber = 1, int PageSize = 10);

}
