namespace MVC.ViewModels.Basket
{
    public class BasketProduct
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Product { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "(1-10)")]
        public int Quantity { get; set; }
    }
}
