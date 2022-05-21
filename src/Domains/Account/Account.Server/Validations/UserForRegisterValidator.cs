namespace Account.Server.Validations;

public class UserForRegisterValidator : AbstractValidator<UserForRegisterViewModel>
{
    public UserForRegisterValidator()
    {
        RuleFor(e => e.FirstName).NotEmpty();
        RuleFor(e => e.LastName).NotEmpty();
        RuleFor(e => e.UserName).NotEmpty().MinimumLength(14).MinimumLength(14);
        RuleFor(e => e.Email).NotEmpty().EmailAddress();
        RuleFor(e => e.Password).NotEmpty().MinimumLength(8);
    }
}
