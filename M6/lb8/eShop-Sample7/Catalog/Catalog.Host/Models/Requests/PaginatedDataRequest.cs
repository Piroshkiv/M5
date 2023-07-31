using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests;

public class PaginatedDataRequest<T>
    where T : notnull
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int PageIndex { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int PageSize { get; set; }

    public Dictionary<T, int>? Filters { get; set; }
}