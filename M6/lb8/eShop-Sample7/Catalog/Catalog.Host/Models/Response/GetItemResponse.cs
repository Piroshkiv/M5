using System.ComponentModel.DataAnnotations;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Models.Response
{
    public class GetItemResponse
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string PictureUrl { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public int AvailableStock { get; set; }
    }
}
