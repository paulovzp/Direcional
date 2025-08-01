﻿namespace Direcional.Infrastructure.Models;

public class JwtSettingsModel
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int DaysToExpire { get; set; } = 1;
}
