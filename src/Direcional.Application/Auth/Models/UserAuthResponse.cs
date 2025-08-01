namespace Direcional.Application;

public class UserAuthResponse
{
    public UserAuthResponse()
    {
    }

    public UserAuthResponse(string token, string type, DateTime created, DateTime expiration)
    {
        AccessToken = token;
        Expiration = expiration;
        Created = created;
        Type = type;
        RefreshToken = Guid.NewGuid().ToString();
    }

    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string Type { get; set; }
    public DateTime Created { get; set; }
    public DateTime Expiration { get; set; }
}
