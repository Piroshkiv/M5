using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests;

public class CreateTypeRequest
{
    [Required]
    public string Type { get; set; } = null!;
}