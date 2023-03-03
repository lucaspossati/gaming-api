using API.Domain.VM;
using FluentValidation;

namespace Core.Validator.User
{
    public class CreateCompanyValidator : AbstractValidator<CompanyVM>
    {
        public CreateCompanyValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("Name is required")
                .MinimumLength(2).WithMessage("Name needs to have more than 1 character")
                .MaximumLength(100).WithMessage("Name can't have more than 100 characters");
        }
    }
}
