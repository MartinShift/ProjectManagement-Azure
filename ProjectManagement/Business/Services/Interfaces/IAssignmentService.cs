using ProjectManagement.Business.Models;

namespace ProjectManagement.Business.Services.Interfaces;

public interface IAssignmentService : IService<AssignmentDto>
{
    Task<IEnumerable<AssignmentDto>> GetByUserAsync(string userId);
}
