using Basket.Host.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Basket.Host.Models.Response
{
    public class GetProductsResponse
    {
        [Required]
        public IEnumerable<BasketProductDto>? Products { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Size { get; set; }
    }
}
