using AutoMapper;
using FluentValidation;
using ProjectManagement.Business.Models;
using ProjectManagement.Business.Services.Interfaces;
using ProjectManagement.Business.Validation;
using ProjectManagement.Data.Entities;
using ProjectManagement.Data.Repositories.Interfaces;

namespace ProjectManagement.Business.Services;

public class AssignmentService : IAssignmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly AssignmentDtoValidator _validator;

    public AssignmentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = new AssignmentDtoValidator();
    }

    public async Task<AssignmentDto> AddAsync(AssignmentDto assignmentDto)
    {
        var validationResult = _validator.Validate(assignmentDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var assignment = _mapper.Map<Assignment>(assignmentDto);
        await _unitOfWork.Assignments.AddAsync(assignment);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<AssignmentDto>(assignment);
    }

    public async Task<AssignmentDto> GetByIdAsync(string assignmentId)
    {
        var assignment = await _unitOfWork.Assignments.GetByIdAsync(assignmentId);
        if (assignment == null)
        {
            throw new KeyNotFoundException($"Assignment with ID {assignmentId} not found.");
        }

        return _mapper.Map<AssignmentDto>(assignment);
    }

    public async Task<IEnumerable<AssignmentDto>> GetAllAsync()
    {
        var assignments = await _unitOfWork.Assignments.GetAllAsync();
        return _mapper.Map<IEnumerable<AssignmentDto>>(assignments);
    }

    public async Task<AssignmentDto> UpdateAsync(string assignmentId, AssignmentDto assignmentDto)
    {
        var assignment = await _unitOfWork.Assignments.GetByIdAsync(assignmentId);
        if (assignment == null)
        {
            throw new KeyNotFoundException($"Assignment with ID {assignmentId} not found.");
        }

        var validationResult = _validator.Validate(assignmentDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        _mapper.Map(assignmentDto, assignment);
        await _unitOfWork.Assignments.UpdateAsync(assignment);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<AssignmentDto>(assignment);
    }

    public async Task<bool> DeleteAsync(string assignmentId)
    {
        _ = await _unitOfWork.Assignments.GetByIdAsync(assignmentId) ?? throw new KeyNotFoundException($"Assignment with ID {assignmentId} not found.");
        await _unitOfWork.Assignments.DeleteAsync(assignmentId);
        await _unitOfWork.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<AssignmentDto>> GetByUserAsync(string userId)
    {
        var assignments = (await _unitOfWork.Assignments.GetAllAsync()).Where(x=> x.AssignedToUserId == userId);
        return _mapper.Map<IEnumerable<AssignmentDto>>(assignments);
    }
}