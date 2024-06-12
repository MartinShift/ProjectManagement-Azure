namespace ProjectManagement.Data.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProjectRepository Projects { get; }
    IAssignmentRepository Assignments { get; }
    IUserRepository Users { get; }
    Task SaveAsync();
}