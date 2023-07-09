using System.ComponentModel.DataAnnotations;

namespace Basket.Host.Models.Request
{
    public class AddProductRequest
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Id { get; set; }
    }
}
