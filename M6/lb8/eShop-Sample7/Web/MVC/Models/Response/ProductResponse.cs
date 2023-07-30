using MVC.ViewModels.Basket;
namespace MVC.Models.Response
{
    public class ProductResponse
    {
        [Required]
        public BasketProduct? Product { get; set; }

    }
}
