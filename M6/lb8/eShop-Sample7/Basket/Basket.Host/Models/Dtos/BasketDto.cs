using System.ComponentModel.DataAnnotations;

namespace Basket.Host.Models.Dtos
{
    public class BasketDto
    {
        public IEnumerable<BasketProductDto> Products { get; set; } = null!;
        public int Size { get; set; }
    }
}
