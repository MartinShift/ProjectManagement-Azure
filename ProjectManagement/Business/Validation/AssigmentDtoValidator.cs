using FluentValidation;
using ProjectManagement.Business.Models;

namespace ProjectManagement.Business.Validation;

public class AssignmentDtoValidator : AbstractValidator<AssignmentDto>
{
    public AssignmentDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Assignment name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Assignment description is required.");
        RuleFor(x => x.DueDate).GreaterThan(x => x.CreatedDate).WithMessage("Due date must be after created date.");
        RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required.");
    }
}
