namespace Direcional.Application.Common;

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

