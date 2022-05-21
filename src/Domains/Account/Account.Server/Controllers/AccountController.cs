namespace Account.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IValidator<UserForRegisterViewModel> _userForRegisterValidator;
    private readonly IValidator<UserForLoginViewModel> _userForLoginValidator;

    public AccountController(ITokenService tokenService, IApplicationUserRepository applicationUserRepository,
        IValidator<UserForRegisterViewModel> userForRegisterValidator, IValidator<UserForLoginViewModel> userForLoginValidator)
    {
        _tokenService = tokenService;
        _applicationUserRepository = applicationUserRepository;
        _userForRegisterValidator = userForRegisterValidator;
        _userForLoginValidator = userForLoginValidator;
    }

    [HttpPost("RegisterUser")]
    public async Task<IActionResult> RegisterUser(UserForRegisterViewModel userForRegister)
    {
        if (userForRegister == null)
            return BadRequest("User is null");

        var validationResult = await _userForRegisterValidator.ValidateAsync(userForRegister);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        IdentityResult result = await _applicationUserRepository.RegisterUser(userForRegister);
        if (!result.Succeeded)
        {
            IEnumerable<string> errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { errors });
        }
        return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserForLoginViewModel userLogin)
    {
        var validationResult = await _userForLoginValidator.ValidateAsync(userLogin);
        if (!validationResult.IsValid)
        {
            string text = string.Join("-", validationResult.Errors.Select(e => e.ErrorMessage));
            return BadRequest(new AuthResponseViewModel { ErrorMessage = text });
        }

        if (!await _applicationUserRepository.ValidateUser(userLogin))
            return Unauthorized(new AuthResponseViewModel { ErrorMessage = "Invalid UserName Or Password"});

        AppUser user = await _applicationUserRepository.GetUserByUserName(userLogin.UserName);

        user.RefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        
        await _applicationUserRepository.UpdateUser(user);

        AuthResponseViewModel authResponse = new AuthResponseViewModel
        {
            IsAuthSuccessful = true,
            Token = _tokenService.CreateToken(user, await _applicationUserRepository.GetUserRoles(user)),
            RefreshToken = user.RefreshToken
        };
        return Ok(authResponse);
    }
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenViewModel refreshTokenViewModel)
    {
        if (refreshTokenViewModel == null)
            return BadRequest(new AuthResponseViewModel { ErrorMessage = "Invalid Client Request" });

        ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(refreshTokenViewModel.Token);

        AppUser user = await _applicationUserRepository.GetUserByUserName(principal.FindFirst(ClaimTypes.Name).Value);

        if(user is null || user.RefreshToken != refreshTokenViewModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest(new AuthResponseViewModel { ErrorMessage = "Invalid Client Request" });
        }

        user.RefreshToken = _tokenService.GenerateRefreshToken();
        await _applicationUserRepository.UpdateUser(user);

        AuthResponseViewModel authResponse = new AuthResponseViewModel
        {
            IsAuthSuccessful = true,
            Token = _tokenService.CreateToken(user, await _applicationUserRepository.GetUserRoles(user)),
            RefreshToken = user.RefreshToken
        };
        return Ok(authResponse);
    }
}
