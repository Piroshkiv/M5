using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests;

public class UpdateProductRequest
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string PictureFileName { get; set; } = null!;

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int CatalogTypeId { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int CatalogBrandId { get; set; }

    public int AvailableStock { get; set; }
}