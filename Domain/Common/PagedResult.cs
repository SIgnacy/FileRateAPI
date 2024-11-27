namespace Domain.Common;
public sealed class PagedResult<T>
{
    public List<T>? Items { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public int TotalCount { get; set; }
    public bool HasNextPage => Page * Size < TotalCount;
    public bool HasPreviousPage => Page > 1;
}
