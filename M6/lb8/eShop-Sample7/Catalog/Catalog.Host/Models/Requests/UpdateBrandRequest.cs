using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests;

public class UpdateBrandRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Brand { get; set; } = null!;
}