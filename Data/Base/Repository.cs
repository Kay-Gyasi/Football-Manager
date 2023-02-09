using Microsoft.EntityFrameworkCore.Storage;

namespace Data.Base;

public class Repository<T, TKey> : IRepository<T, TKey> where T : Entity<TKey>
{
    private readonly AppDbContext _context;
    private readonly ILogger<Repository<T, TKey>> _logger;
    protected readonly DbSet<T> _dbSet;  
    protected Repository(AppDbContext context, ILogger<Repository<T, TKey>> logger) 
    {  
        _context = context;
        _logger = logger;
        _dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity, bool saveChanges = false) 
    {
        try
        {
            await _dbSet.AddAsync(entity);
            if (saveChanges)
                await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        }
    }
    
    public async Task UpdateAsync(T entity, bool saveChanges = false)
    {
        try
        {
            await Task.Run(() => _dbSet.Update(entity));
            if (saveChanges) await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        }
    }
    
    public async Task DeleteAsync(T entity, bool saveChanges = false) 
    {
        try
        {
            await Task.Run(() => _dbSet.Remove(entity));
            if (saveChanges) await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        } 
    }
    
    public async Task SoftDeleteAsync(T entity, bool saveChanges = false)
    {
        try
        {
            await Task.Run(()=> entity.Audit?.Delete());
            if (saveChanges) await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        }
    }
    
    public async Task<T?> FindByIdAsync(int id) 
    {
        try
        {
            return await _dbSet.FindAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        }  
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IDbContextTransaction> BeginTransaction()
    {
        return await _context.Database.BeginTransactionAsync();
    }
}

public interface IRepository<T, TKey> where T : Entity<TKey> 
{  
    Task AddAsync(T entity, bool saveChanges = false);  
    Task SoftDeleteAsync(T entity, bool saveChanges = false);
    Task DeleteAsync(T entity, bool saveChanges = false);
    Task UpdateAsync(T entity, bool saveChanges = false);  
    Task<T?> FindByIdAsync(int id);
    Task<bool> SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransaction();
}