namespace Fina.Core.Requests
{
    public abstract class PagedRequest : Request
    {
        public int pageSize { get; set; } = Configuration.DefaultPageSize;
        public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
    }
}
