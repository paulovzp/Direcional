namespace Direcional.Application.Common;

public class PaginationResponse<T>
{
    public PaginationResponse(IEnumerable<T> data, int totalCount)
    {
        Data = data;
        TotalCount = totalCount;
    }

    public int TotalCount { get; private set; }
    public IEnumerable<T> Data { get; private set; }
}

public class FilterRequest<T>
{
    private const int maxPageCount = 100;
    private int _pageCount = maxPageCount;
    public int Page { get; set; } = 1;
    public int PageSize
    {
        get { return _pageCount; }
        set { _pageCount = (value > maxPageCount) ? maxPageCount : value; }
    }

    public T Filter { get; set; }
    public FilterOrderByRequest Ordenation { get; set; } = new();

}

public class FilterOrderByRequest
{
    public string OrderBy { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
    public bool IsDescending()
    {
        if (!string.IsNullOrEmpty(OrderBy))
        {
            return Direction.Split(' ').Last().ToLowerInvariant().StartsWith("desc");
        }
        return true;
    }
}

