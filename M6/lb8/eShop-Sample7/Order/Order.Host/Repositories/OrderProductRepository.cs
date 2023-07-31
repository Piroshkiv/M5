using Microsoft.EntityFrameworkCore;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Repositories.Interfaces;

namespace Order.Host.Repositories
{
    public class OrderProductRepository: IOrderProductRepository
    {
        private readonly OrderDbContext _dbContext;
        private readonly ILogger<OrderInfoRepository> _logger;
        private readonly IMapper _mapper;

        public OrderProductRepository(
            IDbContextWrapper<OrderDbContext> dbContextWrapper,
            ILogger<OrderInfoRepository> logger,
            IMapper mapper)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<OrderProductDto> GetAsync(int id)
        {
            var order = await _dbContext.OrderProducts
                .Include(o => o.OrderInfo)
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<OrderProductDto>(order);
        }

        public async Task<OrderProductDto> AddAsync(OrderProductDto orderProduct)
        {
            var item = await _dbContext.OrderProducts.AddAsync(_mapper.Map<OrderProduct>(orderProduct));
            _logger.LogInformation($"aa: {orderProduct.Order}");
            _logger.LogInformation($"aa: {item.Entity.OrderId}");

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Order {item.Entity.Id} was added");
            return _mapper.Map<OrderProductDto>(item.Entity);
        }

        public async Task<OrderProductDto> UpdateAsync(OrderProductDto orderProduct)
        {
            var item = _dbContext.Update(_mapper.Map<OrderProduct>(orderProduct));

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Order {item.Entity.Id} was updated");
            return _mapper.Map<OrderProductDto>(item.Entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var item = _dbContext.Remove(new OrderProductDto { Id = id });

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Order {item.Entity.Id} was removed");
            return item.Entity.Id;
        }

        public async Task<IEnumerable<OrderProductDto>> GetByOrderAsync(int id)
        {
            var order = await _dbContext.OrderProducts
                .Include(o => o.OrderInfo)
                .Where(o => o.OrderId == id).ToListAsync();

            return order.Select(o => _mapper.Map<OrderProductDto>(o));
        }
    }
}
