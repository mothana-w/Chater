using System.Linq.Expressions;
using Chater.Data.Model.Entities;

namespace Chater.Data.Repository;

public interface IBaseRepository<T> where T : class
{
  Task GetByIdAsync<TPKey>(TPKey id);
  IEnumerable<T> GetAll(Expression<Func<T, bool>> expression);
  Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression);
  Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression);
  Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
  Task AddAsync(T Entity);
  IQueryable<T> Where(Expression<Func<T, bool>> expression);

  Task RemoveAsync(T target);
}