using Basket.Host.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Basket.Host.Models.Response
{
    public class AddProductResponse
    {
        [Required]
        public BasketProductDto? Product { get; set; }
    }
}
