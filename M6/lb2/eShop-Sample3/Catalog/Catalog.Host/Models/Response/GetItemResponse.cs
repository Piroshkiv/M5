using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Models.Response
{
    public class GetItemResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string PictureUrl { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public int AvailableStock { get; set; }
    }
}
