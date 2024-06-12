using ProjectManagement.Business.Models;

namespace ProjectManagement.Business.Services.Interfaces;

public interface IProjectService : IService<ProjectDto>
{
    public Task<IEnumerable<ProjectDto>> GetByUserAsync(string userId);
}
