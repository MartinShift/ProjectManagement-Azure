using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace ProjectManagement.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ProjectContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(ProjectContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
        _dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}
