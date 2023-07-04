using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Basket.Host.Models.Dtos
{
    public class BasketProductDto
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Product { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "(1-10)")]
        public int Quantity { get; set; }
    }
}
