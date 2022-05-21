namespace CommonLibrary
{
    using FluentValidation;

    public class BaseValidator<TViewModel> : AbstractValidator<TViewModel>
        where TViewModel : BaseViewModel
    {
        public BaseValidator()
        {
        }
    }
}

