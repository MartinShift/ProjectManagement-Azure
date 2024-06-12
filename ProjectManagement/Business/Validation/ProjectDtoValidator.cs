using FluentValidation;
using ProjectManagement.Business.Models;

namespace ProjectManagement.Business.Validation;

public class ProjectDtoValidator : AbstractValidator<ProjectDto>
{
    public ProjectDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Project name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Project description is required.");
        RuleFor(x => x.StartDate).LessThan(x => x.EndDate).WithMessage("Start date must be before end date.");
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage("End date must be after start date.");
    }
}
