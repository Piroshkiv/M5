using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class GetItemByIdRequest
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Value { get; set; }
    }
}
