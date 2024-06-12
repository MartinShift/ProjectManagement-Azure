using ProjectManagement.Data.Entities;
using ProjectManagement.Data.Repositories.Interfaces;

namespace ProjectManagement.Data.Repositories;

public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
{
    public AssignmentRepository(ProjectContext context) : base(context)
    {
    }
}
