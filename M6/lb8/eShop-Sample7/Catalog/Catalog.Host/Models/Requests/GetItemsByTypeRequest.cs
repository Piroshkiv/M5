using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class GetItemsByTypeRequest
    {
        [Required]
        public string Type { get; set; } = null!;
    }
}
