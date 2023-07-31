using Order.Host.Data.Entities;

namespace Order.Host.Models.Dtos
{
    public class OrderProductDto
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int Product { get; set; }
        public int Quantity { get; set; }

    }
}
