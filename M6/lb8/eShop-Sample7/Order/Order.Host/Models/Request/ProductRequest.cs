namespace Order.Host.Models.Request
{
    public class ProductRequest
    {
        public int Order { get; set; }
        public int Product { get; set; }
        public int Quentity { get; set; }
    }
}
