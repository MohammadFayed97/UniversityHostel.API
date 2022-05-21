namespace Account.Server.Repositories;

public interface IApplicationUserRepository
{
    Task<IdentityResult> RegisterUser(UserForRegisterViewModel userForRegister);
    Task<bool> ValidateUser(UserForLoginViewModel userForLogin);
    Task<AppUser> GetUserByUserName(string userName);
    Task<AppUser> GetUserByEmail(string email);
    Task<IEnumerable<string>> GetUserRoles(AppUser user);
    Task UpdateUser(AppUser user);
}
