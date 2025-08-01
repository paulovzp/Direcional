
using Direcional.Domain;
using Direcional.Infrastructure.Exceptions;
using Direcional.Infrastructure.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Direcional.Application;

public class AuthAppService : IAuthAppService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly JwtSettingsModel _jwtSettings;
    private DateTime _currentTime => DateTime.UtcNow;

    public AuthAppService(IUsuarioRepository usuarioRepository,
        IOptions<JwtSettingsModel> jwtSettings)
    {
        _usuarioRepository = usuarioRepository;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<UserAuthResponse> Authenticate(UserAuthRequest authRequest)
    {
        var usuario = await _usuarioRepository.ObterPorEmail(authRequest.Email) ?? throw new DirecionalNotFoundException("Dados de login inválidos");

        if(!usuario.ValidarSenha(authRequest.Password))
            throw new DirecionalNotFoundException("Dados de login inválidos");
        DateTime expirationDate = DateTime.UtcNow.AddDays(_jwtSettings.DaysToExpire);
        var token = GenerateJwtToken(expirationDate, usuario.Id.ToString(), usuario.Email, usuario.Nome, usuario.Tipo.ToString());
        return new UserAuthResponse(token, usuario.Tipo.ToString(), _currentTime, expirationDate);
    }

    private string GenerateJwtToken(DateTime expireDate, string userId, string userEmail, string userName, string userRole)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaimsList(userId, userEmail, userName, userRole),
            Expires = expireDate,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = creds
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaimsList(string userId, string userEmail, string userName, string userRole)
    {
        var claims = new ClaimsIdentity(
        [
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Email, userEmail),
            new Claim(ClaimTypes.Role, userRole)
        ]);

        return claims;
    }
}
