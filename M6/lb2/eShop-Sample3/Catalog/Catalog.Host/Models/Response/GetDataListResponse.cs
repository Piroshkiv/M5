namespace Catalog.Host.Models.Response
{
    public class GetDataListResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = null !;
    }
}
