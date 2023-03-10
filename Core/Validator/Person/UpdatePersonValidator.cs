using FluentValidation;
using Manager.VM.Person;

namespace Core.Validator.User
{
    public class UpdatePersonValidator : AbstractValidator<PersonVM>
    {
        public UpdatePersonValidator()
        {
            RuleFor(p => p.Id)
                .NotNull().WithMessage("Id is required");

            RuleFor(p => p.FullName)
                .NotNull().WithMessage("Full name is required")
                .MinimumLength(2).WithMessage("Name needs to have more than 1 character")
                .MaximumLength(100).WithMessage("Name can't have more than 100 characters");

            RuleFor(p => p.PhoneNumber)
                .NotNull().WithMessage("Phone number is required")
                .MinimumLength(2).WithMessage("Phone number needs to have more than 1 character")
                .MaximumLength(50).WithMessage("Phone number can't have more than 50 characters");

            RuleFor(p => p.Address)
                .NotNull().WithMessage("Address is required")
                .MinimumLength(2).WithMessage("Address needs to have more than 1 character")
                .MaximumLength(200).WithMessage("Address can't have more than 200 characters");
        }
    }
}
