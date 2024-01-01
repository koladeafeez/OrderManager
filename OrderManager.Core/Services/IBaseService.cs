

using OrderManager.Data.Models;
using System.Linq.Expressions;

namespace OrderManager.Core.Services
{
    public interface IBaseService<TEntity, TCreate> where TEntity : EntityBase
    {
   
        void Delete(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity?> GetMultiple(Func<TEntity, bool> predicate);
        TEntity? GetSingle(Func<TEntity, bool> predicate);
        TEntity? GetSingle(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes);

    }
}
