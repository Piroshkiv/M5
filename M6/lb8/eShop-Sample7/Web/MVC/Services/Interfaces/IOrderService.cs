using MVC.Models.Requests;
using MVC.ViewModels.Order;
using System.Net;

namespace MVC.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderInfo> AddOrder(OrderInfoRequest product);
        Task<OrderProduct> AddProduct(OrderProductRequest product);
        Task<OrderInfo> SetFormed(int orderId);
        Task<OrderInfo> OrderById(int id);
        Task<OrderInfo> Orders();
    }
}
