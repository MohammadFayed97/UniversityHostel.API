namespace Account.Server.TokenServices;

public class TokenService : ITokenService
{
    private readonly JwtConfiguration _jwtConfiguration;
    public TokenService(JwtConfiguration jwtConfiguration) => _jwtConfiguration = jwtConfiguration;

    public string CreateToken(AppUser user, IEnumerable<string> roles)
    {
        var signingCredential = GetSigningCredentials();
        var claims = GetClaims(user, roles);
        var tokenOptions = GenerateTokenOptions(signingCredential, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    public SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtConfiguration.Key);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public IEnumerable<Claim> GetClaims(AppUser user, IEnumerable<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("fullname", user.FullName)
        };

        if (!roles.Any())
            return claims;

        foreach (var role in roles)
        {
            if (string.IsNullOrEmpty(role))
                continue;

            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        claims.Add(new Claim("exp", _jwtConfiguration.DurationInMinutes.ToString()));

        return claims;
    }

    public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        return new JwtSecurityToken
        (
            issuer: _jwtConfiguration.Issuer,
            audience: _jwtConfiguration.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.DurationInMinutes)),
            signingCredentials: signingCredentials
        );
    }

    public string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();

        random.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,

            ValidIssuer = _jwtConfiguration.Issuer,
            ValidAudience = _jwtConfiguration.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key))
        };

        SecurityToken securityToken;
        ClaimsPrincipal principles = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out securityToken);

        JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
        if(jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid Token");
        }

        return principles;
    }
}
