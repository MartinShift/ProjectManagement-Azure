namespace ProjectManagement.Business.Services.Interfaces;

public interface IService<T>
{
    Task<T> AddAsync(T entity);
    Task<T> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> UpdateAsync(string id, T entity);
    Task<bool> DeleteAsync(string id);
}
