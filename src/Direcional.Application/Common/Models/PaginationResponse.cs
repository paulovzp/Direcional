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