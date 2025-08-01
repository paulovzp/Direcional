
namespace Direcional.Application;

public interface IAuthAppService
{
    Task<UserAuthResponse> Authenticate(UserAuthRequest authRequest);
}
