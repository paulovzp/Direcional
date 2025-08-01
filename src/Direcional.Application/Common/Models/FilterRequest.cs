namespace Direcional.Application.Common;

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

