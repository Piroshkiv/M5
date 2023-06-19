namespace Catalog.Host.Data;

public class PaginatedData<T>
{
    public long TotalCount { get; init; }

    public IEnumerable<T> Data { get; init; } = null!;
}