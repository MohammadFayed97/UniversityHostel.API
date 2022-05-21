namespace Account.Server.Repositories;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly UserManager<AppUser> _userManager;
    public ApplicationUserRepository(UserManager<AppUser> userManager) => _userManager = userManager;

    public async Task<bool> ValidateUser(UserForLoginViewModel userForLogin)
    {
        AppUser user = await _userManager.FindByNameAsync(userForLogin.UserName);

        return (user != null && await _userManager.CheckPasswordAsync(user, userForLogin.Password));
    }
    public async Task<AppUser> GetUserByUserName(string userName) => await _userManager.FindByNameAsync(userName);
    public async Task<AppUser> GetUserByEmail(string email) => await _userManager.FindByEmailAsync(email);
    public async Task UpdateUser(AppUser user)
    {
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            string error = string.Join("-", result.Errors.Select(e => e.Description));
            throw new Exception(error);
        }
    }
    public async Task<IdentityResult> RegisterUser(UserForRegisterViewModel userForRegister)
    {
        string userFullName = $"{userForRegister.FirstName} {userForRegister.LastName}";

        AppUser user = new AppUser
        {
            FullName = userFullName,
            UserName = userForRegister.UserName,
            PhoneNumber = userForRegister.PhoneNumber,
            Email = userForRegister.Email,
        };
        return await _userManager.CreateAsync(user, userForRegister.Password);
    }

    public async Task<IEnumerable<string>> GetUserRoles(AppUser user) => await _userManager.GetRolesAsync(user);

}
