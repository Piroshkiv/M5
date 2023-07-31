using Order.Host.Data;
using Order.Host.Models.Dtos;
using Order.Host.Data.Entities;
using StackExchange.Redis;
using Order.Host.Repositories.Interfaces;

namespace Order.Host.Repositories
{
    public class OrderInfoRepository: IOrderInfoRepository
    {
        private readonly OrderDbContext _dbContext;
        private readonly ILogger<OrderInfoRepository> _logger;
        private readonly IMapper _mapper;

        public OrderInfoRepository(
            IDbContextWrapper<OrderDbContext> dbContextWrapper,
            ILogger<OrderInfoRepository> logger,
            IMapper mapper)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<OrderInfoDto> GetAsync(int id)
        {
            var order = await _dbContext.OrdersInfo
                .Include(o => o.Products)
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<OrderInfoDto>(order);
        }

        public async Task<OrderInfoDto> AddAsync(OrderInfoDto orderInfo)
        {
            var item = await _dbContext.AddAsync(_mapper.Map<OrderInfo>(orderInfo));

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Order {item.Entity.Id} was added");
            return _mapper.Map<OrderInfoDto>(item.Entity);
        }

        public async Task<OrderInfoDto> UpdateAsync(OrderInfoDto orderInfo)
        {
            var item = _dbContext.Update(_mapper.Map<OrderInfo>(orderInfo));

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Order {item.Entity.Id} was updated");
            return _mapper.Map<OrderInfoDto>(item.Entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var item = _dbContext.Remove(new OrderInfoDto { Id = id });

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Order {item.Entity.Id} was removed");
            return item.Entity.Id;
        }

        public async Task<OrderInfoDto> FormAsync(int id)
        {
            var item = await _dbContext.OrdersInfo
                .Include(o => o.Products)
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();

            item.OrderFormed = true;

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Order {item.Id} was formed");
            return _mapper.Map<OrderInfoDto>(item);
        }

        public async Task<OrderInfoDto> SendAsync(int id)
        {
            var item = await _dbContext.OrdersInfo
                .Include(o => o.Products)
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();

            item.SendData = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Order {item.Id} was sended");
            return _mapper.Map<OrderInfoDto>(item);
        }

        public async Task<OrderInfoDto> TakeAsync(int id)
        {
            var item = await _dbContext.OrdersInfo
                .Include(o => o.Products)
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();

            item.TakeData = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Order {item.Id} was taken");
            return _mapper.Map<OrderInfoDto>(item);
        }

        public async Task<IEnumerable<OrderInfoDto>> GetByUserAsync(int id)
        {
            var order = await _dbContext.OrdersInfo
                .Include(o => o.Products)
                .Where(o => o.SubjectId == id).ToListAsync();

            return order.Select( o => _mapper.Map<OrderInfoDto>(o));
        }
    }
}
