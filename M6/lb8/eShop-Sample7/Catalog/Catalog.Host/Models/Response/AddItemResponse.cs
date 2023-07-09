using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Response;

public class AddItemResponse<T>
{
    [Required]
    public T Id { get; set; } = default(T) !;
}