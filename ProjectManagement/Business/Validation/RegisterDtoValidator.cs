using FluentValidation;
using ProjectManagement.Business.Models;
using ProjectManagement.Data.Repositories.Interfaces;

namespace ProjectManagement.Business.Validation
{
    public class RegisterDtoValidator : AbstractValidator<RegisterModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
            RuleFor(x => x.Login).NotEmpty().WithMessage("Login is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match.");
            RuleFor(x => x.Login).Must(x => unitOfWork.Users.GetByLoginAsync(x).Result == null).WithMessage("Login is already taken.");
        }

    }
}
