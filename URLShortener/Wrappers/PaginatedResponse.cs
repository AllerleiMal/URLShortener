namespace URLShortener.Wrappers;

public class PaginatedResponse<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public long TotalRecords { get; set; }
    public List<T> Data { get; set; } = new();
}