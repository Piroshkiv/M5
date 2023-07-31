using Order.Host.Models.Dtos;

namespace Order.Host.Models.Response
{
    public class GetDataResponse<T>
    {
        public T Data { get; set; }
    }
}
