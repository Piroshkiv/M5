using Infrastructure.Identity;

namespace MVC;

public class AppSettings
{
    public string CatalogUrl { get; set; }
    public string BasketUrl { get; set; }
    public string BasketProductUrl { get; set; }
    public string OrderUrl { get; set; }
    public string OrderInfoUrl { get; set; }
    public string OrderProductUrl { get; set; }
    public int SessionCookieLifetimeMinutes { get; set; }    
    public string CallBackUrl { get; set; }
    public string IdentityUrl { get; set; }
}
