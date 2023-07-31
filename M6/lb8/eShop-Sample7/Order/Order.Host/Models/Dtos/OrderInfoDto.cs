using Order.Host.Data.Entities;

namespace Order.Host.Models.Dtos
{
    public class OrderInfoDto
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? SendData { get; set; }
        public DateTime? TakeData { get; set; }
        public bool OrderFormed { get; set; }
        public List<OrderProductDto> Products { get; set; }
    }
}
