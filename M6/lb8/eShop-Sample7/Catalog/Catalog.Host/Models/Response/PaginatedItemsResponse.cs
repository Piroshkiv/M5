using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Response;

public class PaginatedItemsResponse<T>
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int PageIndex { get; init; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int PageSize { get; init; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public long Count { get; init; }

    public IEnumerable<T> Data { get; init; } = null!;
}
