using System.ComponentModel.Design.Serialization;
using System.Linq.Expressions;
using Chater.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Chater.Data.Repository;

public class BaseRepository<T>(AppDbContext _dbctx) : IBaseRepository<T> where T : class
{
  public async Task<T?> GetByIdAsync<TPKey>(TPKey id) => await _dbctx.Set<T>().FindAsync(id);
  public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression) => _dbctx.Set<T>().Where(expression);
  public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression, string[] includes)
  {
    IQueryable<T> query = _dbctx.Set<T>();

    foreach (var include in includes)
      query = query.Include(include);
    
    return query.Where(expression);
  }

  public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression) => await _dbctx.Set<T>().FirstOrDefaultAsync(expression);
  public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression, string[][] includes) {
    IQueryable<T> query = _dbctx.Set<T>();

    for (int i = 0; i < includes.Count(); ++i){
      string fullInclude = string.Join('.', includes[i]);
      query = query.Include(fullInclude);
    }

    return await query.FirstOrDefaultAsync(expression);
  }
  public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression) => await _dbctx.Set<T>().SingleOrDefaultAsync(expression);

  public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression) => await _dbctx.Set<T>().AnyAsync(expression);
  public async Task AddAsync(T entity) {
    await _dbctx.Set<T>().AddAsync(entity);
    await _dbctx.SaveChangesAsync();
  }

  public async Task RemoveAsync(T target){
    _dbctx.Set<T>().Remove(target);
    await _dbctx.SaveChangesAsync();
  }

  public IQueryable<T> Where(Expression<Func<T, bool>> expression) => _dbctx.Set<T>().Where(expression);

}
