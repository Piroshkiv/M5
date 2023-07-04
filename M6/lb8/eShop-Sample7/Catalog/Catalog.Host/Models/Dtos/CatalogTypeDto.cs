using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Dtos;

public class CatalogTypeDto
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int Id { get; set; }

    [Required]
    public string Type { get; set; } = null!;
}