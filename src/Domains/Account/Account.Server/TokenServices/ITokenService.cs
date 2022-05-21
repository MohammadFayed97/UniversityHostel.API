namespace Account.Server.TokenServices;

public interface ITokenService
{
    string CreateToken(AppUser user, IEnumerable<string> roles);
    JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims);
    IEnumerable<Claim> GetClaims(AppUser user, IEnumerable<string> roles);
    SigningCredentials GetSigningCredentials();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    string GenerateRefreshToken();
}
