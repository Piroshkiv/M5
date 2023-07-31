using MVC.ViewModels.Order;

namespace MVC.ViewModels.Basket
{
    public class BasketViewModel
    {
        public Basket Basket { get; set; }
        public IEnumerable<CatalogItem> Products { get; set; }
        public OrderInfo Order { get; set; }

    }
}
