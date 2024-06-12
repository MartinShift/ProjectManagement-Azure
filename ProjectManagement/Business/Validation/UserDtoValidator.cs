using FluentValidation;
using ProjectManagement.Business.Models;

namespace ProjectManagement.Business.Validation;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
    }
}
