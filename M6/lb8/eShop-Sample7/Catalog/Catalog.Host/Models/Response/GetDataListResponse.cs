using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Response
{
    public class GetDataListResponse<T>
    {
        [Required]
        public IEnumerable<T> Data { get; set; } = null !;
    }
}
