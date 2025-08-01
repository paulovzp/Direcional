using Direcional.Infrastructure.Enums;
using Direcional.Infrastructure.Interfaces;
using System.Security.Claims;

namespace Direcional.Api.Session;

public class UserSession : IUserSession
{
    private readonly IHttpContextAccessor _context;
    public UserSession(IHttpContextAccessor context)
    {
        _context = context;
    }

    public string AuthenticatedUserToken
    {
        get
        {
            return _context.HttpContext.Request.Headers["Authorization"];
        }
    }

    public int Id
    {
        get
        {
            string userId = GetClaimValue(ClaimTypes.NameIdentifier);
            return string.IsNullOrEmpty(userId) ? 0 : Convert.ToInt32(userId);
        }
    }

    public string Name
    {
        get
        {
            string name = GetClaimValue(ClaimTypes.Name);
            return string.IsNullOrEmpty(name) ? string.Empty : name;
        }
    }

    public string Email
    {
        get
        {
            string email = GetClaimValue(ClaimTypes.Email);
            return string.IsNullOrEmpty(email) ? string.Empty : email;
        }
    }

    public TipoUsuario Role
    {
        get
        {
            string type = GetClaimValue(ClaimTypes.Role);

            if (Enum.TryParse(type, out TipoUsuario tipoUsuario))
                return tipoUsuario;

            return TipoUsuario.Anonimo;
        }
    }

    private string GetClaimValue(string claimType)
    {
        return _context.HttpContext.User?.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
    }
}
