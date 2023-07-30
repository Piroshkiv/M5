namespace Order.Host.Data.Entities
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? SendData { get; set; }
        public DateTime? TakeData { get; set; }
        public bool OrderFormed { get; set; } = false;
        public List<OrderProduct> Products { get; set; }
    }
}
