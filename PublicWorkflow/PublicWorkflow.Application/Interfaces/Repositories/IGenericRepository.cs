
using PublicWorkflow.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : AuditableExt
    {
        Task<long> AddAsync(T entity);
        Task<long> AddRangeAsync(List<T> entities);
        Task<long> CountAsync(Expression<Func<T, bool>> filter, bool ignoreFilters = false);
        Task<bool> DeleteAsync(T entity, bool isSoftDelete = true);
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> filter, bool ignoreFilters = false, params Expression<Func<T, object>>[] includeProperties);
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> filter, int pageNumber, int pageSize, bool ignoreFilters = false, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, bool ignoreFilters = false, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(long entityId, bool ignoreFilters = false);
        Task<List<T>> GetListAsync(bool ignoreFilters = false);
        Task<bool> UpdateAsync(T entity);
        Task<bool> UpdateRangeAsync(List<T> entities);
        Task<int> SqlQuery(string query);
    }
}
