using Newtonsoft.Json;

namespace Direcional.Infrastructure.Models;

public class ErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public IDictionary<string, string[]> ErrorList { get; set; } = new Dictionary<string, string[]>();

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}