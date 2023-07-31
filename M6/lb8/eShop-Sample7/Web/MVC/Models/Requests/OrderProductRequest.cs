namespace MVC.Models.Requests
{
    public class OrderProductRequest
    {
        public int Order { get; set; }
        public int Product { get; set; }
        public int Quentity { get; set; }
    }
}
