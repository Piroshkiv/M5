using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services
{
    public class OrderInfoService : BaseDataService<OrderDbContext>, IOrderInfoService
    {
        private readonly IOrderInfoRepository _orderInfoRepository;

        public OrderInfoService(
            IDbContextWrapper<OrderDbContext> dbContextWrapper,
            ILogger<BaseDataService<OrderDbContext>> logger,
            IOrderInfoRepository orderInfoRepository)
            : base(dbContextWrapper, logger)
        {
            _orderInfoRepository = orderInfoRepository;
        }

        public Task<OrderInfoDto> AddAsync(OrderInfoDto orderInfo)
        {
            return ExecuteSafeAsync(() => _orderInfoRepository.AddAsync(orderInfo));
        }

        public Task<int> DeleteAsync(int id)
        {
            return ExecuteSafeAsync(() => _orderInfoRepository.DeleteAsync(id));
        }

        public Task<OrderInfoDto> FormAsync(int id)
        {
            return ExecuteSafeAsync(() => _orderInfoRepository.FormAsync(id));
        }

        public Task<OrderInfoDto> GetAsync(int id)
        {
            return ExecuteSafeAsync(() => _orderInfoRepository.GetAsync(id));
        }

        public Task<IEnumerable<OrderInfoDto>> GetByUserAsync(int id)
        {
            return ExecuteSafeAsync(() => _orderInfoRepository.GetByUserAsync(id));
        }

        public Task<OrderInfoDto> SendAsync(int id)
        {
            return ExecuteSafeAsync(() => _orderInfoRepository.SendAsync(id));
        }

        public Task<OrderInfoDto> TakeAsync(int id)
        {
            return ExecuteSafeAsync(() => _orderInfoRepository.TakeAsync(id));
        }

        public Task<OrderInfoDto> UpdateAsync(OrderInfoDto orderInfo)
        {
            return ExecuteSafeAsync(() => _orderInfoRepository.UpdateAsync(orderInfo));
        }
    }
}
