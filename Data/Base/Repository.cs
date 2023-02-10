using Data.Exceptions;
using Data.Helpers;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.Base;

public class Repository<T, TKey> : IRepository<T, TKey> where T : Entity<TKey>
{
    private readonly AppDbContext _context;
    private readonly ILogger<Repository<T, TKey>> _logger;
    private DbSet<T>? _dbSet;  
    protected Repository(AppDbContext context, ILogger<Repository<T, TKey>> logger) 
    {  
        _context = context;
        _logger = logger;
    }

    protected virtual DbSet<T> Entities 
        => _dbSet ??= _context.Set<T>();

    protected virtual IQueryable<T> GetBaseQuery() 
        => Entities;
    
    public async Task AddAsync(T entity, bool saveChanges = false) 
    {
        try
        {
            await Entities.AddAsync(entity);
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
            await Task.Run(() => Entities.Update(entity));
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
            await Task.Run(() => Entities.Remove(entity));
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
            if (id <= 0) throw new InvalidIdException($"{id} cannot be empty!");
            var keyProperty = _context.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties[0];
            return await GetBaseQuery().FirstOrDefaultAsync(e => EF.Property<int>
                (e, keyProperty!.Name) == id);
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            throw;
        }  
    }

    public async Task<PaginatedList<T>> GetPageAsync(PaginatedCommand command)
    {
        return await Task.Run(() => PaginatedList<T>
            .Create(GetBaseQuery(), command.PageNumber, command.PageSize));
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
    Task<PaginatedList<T>> GetPageAsync(PaginatedCommand command);
    Task<bool> SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransaction();
}