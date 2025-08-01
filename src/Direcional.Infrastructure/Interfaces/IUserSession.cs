using Direcional.Infrastructure.Enums;

namespace Direcional.Infrastructure.Interfaces;

public interface IUserSession
{
    int Id { get; }
    string Name { get; }
    string Email { get; }
    TipoUsuario Role { get; }
}