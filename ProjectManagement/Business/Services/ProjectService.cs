using AutoMapper;
using FluentValidation;
using ProjectManagement.Business.Models;
using ProjectManagement.Business.Services.Interfaces;
using ProjectManagement.Business.Validation;
using ProjectManagement.Data.Entities;
using ProjectManagement.Data.Repositories.Interfaces;

namespace ProjectManagement.Business.Services;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ProjectDtoValidator _validator;

    public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = new ProjectDtoValidator();
    }

    public async Task<ProjectDto> AddAsync(ProjectDto projectDto)
    {
        var validationResult = _validator.Validate(projectDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var project = _mapper.Map<Project>(projectDto);
        await _unitOfWork.Projects.AddAsync(project);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<ProjectDto> GetByIdAsync(string projectId)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(projectId);
        if (project == null)
        {
            throw new KeyNotFoundException($"Project with ID {projectId} not found.");
        }

        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<IEnumerable<ProjectDto>> GetAllAsync()
    {
        var projects = await _unitOfWork.Projects.GetAllAsync();
        return _mapper.Map<IEnumerable<ProjectDto>>(projects);
    }

    public async Task<ProjectDto> UpdateAsync(string projectId, ProjectDto projectDto)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(projectId);
        if (project == null)
        {
            throw new KeyNotFoundException($"Project with ID {projectId} not found.");
        }

        var validationResult = _validator.Validate(projectDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        _mapper.Map(projectDto, project);
        await _unitOfWork.Projects.UpdateAsync(project);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<bool> DeleteAsync(string projectId)
    {
        _ = await _unitOfWork.Projects.GetByIdAsync(projectId) ?? throw new KeyNotFoundException($"Project with ID {projectId} not found.");
        await _unitOfWork.Projects.DeleteAsync(projectId);
        await _unitOfWork.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<ProjectDto>> GetByUserAsync(string userId)
    {
        var projects = (await _unitOfWork.Projects.GetAllAsync()).Where(x=> x.Members.Any(x=> x.Id == userId));
        return _mapper.Map<IEnumerable<ProjectDto>>(projects);
    }
}
