using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests;

public class CreateBrandRequest
{
    [Required]
    public string Brand { get; set; } = null !;
}