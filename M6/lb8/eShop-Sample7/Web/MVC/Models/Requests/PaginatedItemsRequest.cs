namespace MVC.Dtos;

public class PaginatedItemsRequest<T>
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int PageIndex { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int PageSize { get; set; }
    
    public Dictionary<T, int>? Filters { get; set; }
}