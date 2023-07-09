using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Basket.Host.Models.Dtos
{
    public class BasketProductDto
    {
        public int Product { get; set; }

        public int Quantity { get; set; }
    }
}
