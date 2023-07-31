using Microsoft.EntityFrameworkCore;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;

namespace Order.Host.Repositories.Interfaces
{
    public interface IOrderProductRepository
    {
        public Task<OrderProductDto> GetAsync(int id);

        public Task<IEnumerable<OrderProductDto>> GetByOrderAsync(int id);

        public Task<OrderProductDto> AddAsync(OrderProductDto orderProduct);

        public Task<OrderProductDto> UpdateAsync(OrderProductDto orderProduct);

        public Task<int> DeleteAsync(int id);
    }
}
