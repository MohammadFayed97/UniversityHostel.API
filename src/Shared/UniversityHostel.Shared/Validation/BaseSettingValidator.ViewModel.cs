namespace CommonLibrary
{
    using FluentValidation;

    public class BaseSettingValidator<TViewModel> : BaseValidator<TViewModel>
        where TViewModel : BaseSettingViewModel
    {
        public BaseSettingValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.NameSecondLanguage).NotEmpty();
        }
    }
}

