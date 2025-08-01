﻿namespace Direcional.Application;

public class UserAuthRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string GrantType { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
