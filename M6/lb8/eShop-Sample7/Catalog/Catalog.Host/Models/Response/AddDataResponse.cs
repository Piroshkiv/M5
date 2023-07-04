using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Response;

public class AddDataResponse<T>
{
    [Required]
    public T Id { get; set; } = default(T) !;
}