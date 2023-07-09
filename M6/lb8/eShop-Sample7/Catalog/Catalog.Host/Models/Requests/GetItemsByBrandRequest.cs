using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class GetItemsByBrandRequest
    {
        [Required]
        public string Brand { get; set; } = null!;
    }
}
