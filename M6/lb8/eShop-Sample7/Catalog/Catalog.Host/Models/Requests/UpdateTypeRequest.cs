using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests;

public class UpdateTypeRequest
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int Id { get; set; }

    [Required]
    public string Type { get; set; } = null!;
}