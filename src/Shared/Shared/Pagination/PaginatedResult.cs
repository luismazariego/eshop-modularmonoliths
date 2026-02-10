namespace Shared.Pagination;

public class PaginatedResult<TEntity>
    (
    int pageIndex,
    int pageSize,
    long count,
    IEnumerable<TEntity> Data) where TEntity : class
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public long Count { get; } = count;
    public IEnumerable<TEntity> Data { get; } = Data;
}
