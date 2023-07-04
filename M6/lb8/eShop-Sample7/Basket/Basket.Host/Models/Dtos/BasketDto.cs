using System.ComponentModel.DataAnnotations;

namespace Basket.Host.Models.Dtos
{
    public class BasketDto
    {
        [Required]
        public IEnumerable<BasketProductDto> Products { get; set; } = null!;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Size { get; set; }
    }
}
