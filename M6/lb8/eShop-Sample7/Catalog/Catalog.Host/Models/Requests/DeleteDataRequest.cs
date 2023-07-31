using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests;

public class DeleteDataRequest
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int Id { get; set; }
}