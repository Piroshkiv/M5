using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Dtos;

public class CatalogItemDto
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public decimal Price { get; set; }

    public string PictureUrl { get; set; } = null!;

    public CatalogTypeDto CatalogType { get; set; } = null!;

    public CatalogBrandDto CatalogBrand { get; set; } = null!;

    public int AvailableStock { get; set; }
}
