namespace Order.Host.Data.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public virtual OrderInfo OrderInfo { get; set; }
    }
}
