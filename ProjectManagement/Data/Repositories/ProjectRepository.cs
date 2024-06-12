using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data.Entities;
using ProjectManagement.Data.Repositories.Interfaces;

namespace ProjectManagement.Data.Repositories;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(ProjectContext context) : base(context)
    {
    }
}
