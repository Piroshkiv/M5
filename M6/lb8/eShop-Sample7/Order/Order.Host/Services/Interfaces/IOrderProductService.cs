using Order.Host.Models.Dtos;

namespace Order.Host.Services.Interfaces
{
    public interface IOrderProductService
    {
        public Task<OrderProductDto> GetAsync(int id);
        public Task<IEnumerable<OrderProductDto>> GetByOrderAsync(int id);
        public Task<OrderProductDto> AddAsync(OrderProductDto orderProduct);
        public Task<OrderProductDto> UpdateAsync(OrderProductDto orderProduct);
        public Task<int> DeleteAsync(int id);
    }
}
