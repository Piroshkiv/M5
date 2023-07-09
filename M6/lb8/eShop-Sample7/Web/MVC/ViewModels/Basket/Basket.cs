namespace MVC.ViewModels.Basket
{
    public class Basket
    {
        [Required]
        public IEnumerable<BasketProduct> Products { get; set; } = null!;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Size { get; set; }
    }
}
