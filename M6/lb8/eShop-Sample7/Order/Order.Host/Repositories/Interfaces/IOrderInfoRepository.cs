using Microsoft.EntityFrameworkCore;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;

namespace Order.Host.Repositories.Interfaces
{
    public interface IOrderInfoRepository
    {
        public Task<OrderInfoDto> GetAsync(int id);

        public Task<IEnumerable<OrderInfoDto>> GetByUserAsync(int id);

        public Task<OrderInfoDto> AddAsync(OrderInfoDto orderInfo);

        public Task<OrderInfoDto> UpdateAsync(OrderInfoDto orderInfo);

        public Task<int> DeleteAsync(int id);

        public Task<OrderInfoDto> FormAsync(int id);

        public Task<OrderInfoDto> SendAsync(int id);

        public Task<OrderInfoDto> TakeAsync(int id);
    }
}
