using ProjectManagement.Data.Repositories.Interfaces;

namespace ProjectManagement.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ProjectContext _context;
    public IProjectRepository Projects { get; private set; }
    public IAssignmentRepository Assignments { get; private set; }
    public IUserRepository Users { get; private set; }

    public UnitOfWork(ProjectContext context, IProjectRepository projectRepository, IAssignmentRepository assignmentRepository, IUserRepository userRepository)
    {
        _context = context;
        Projects = projectRepository;
        Assignments = assignmentRepository;
        Users = userRepository;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
