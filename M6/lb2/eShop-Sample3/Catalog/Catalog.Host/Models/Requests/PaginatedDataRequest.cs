namespace Catalog.Host.Models.Requests;

public class PaginatedDataRequest
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }
}