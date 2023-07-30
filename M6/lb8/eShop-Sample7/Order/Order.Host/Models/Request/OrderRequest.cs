using Order.Host.Data.Entities;

namespace Order.Host.Models.Request
{
    public class OrderRequest
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
