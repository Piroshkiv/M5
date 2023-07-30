using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services
{
    public class OrderProductService: BaseDataService<OrderDbContext>, IOrderProductService
    {
        private readonly IOrderProductRepository _orderProductRepository;

    public OrderProductService(
        IDbContextWrapper<OrderDbContext> dbContextWrapper,
        ILogger<BaseDataService<OrderDbContext>> logger,
        IOrderProductRepository orderProductRepository)
        : base(dbContextWrapper, logger)
        {
            _orderProductRepository = orderProductRepository;
        }

        public Task<OrderProductDto> AddAsync(OrderProductDto orderProduct)
        {
            return ExecuteSafeAsync(() => _orderProductRepository.AddAsync(orderProduct));
        }

        public Task<int> DeleteAsync(int id)
        {
            return ExecuteSafeAsync(() => _orderProductRepository.DeleteAsync(id));
        }

        public Task<OrderProductDto> GetAsync(int id)
        {
            return ExecuteSafeAsync(() => _orderProductRepository.GetAsync(id));
        }

        public Task<IEnumerable<OrderProductDto>> GetByOrderAsync(int id)
        {
            return ExecuteSafeAsync(() => _orderProductRepository.GetByOrderAsync(id));
        }

        public Task<OrderProductDto> UpdateAsync(OrderProductDto orderProduct)
        {
            return ExecuteSafeAsync(() => _orderProductRepository.UpdateAsync(orderProduct));
        }
    }
}
