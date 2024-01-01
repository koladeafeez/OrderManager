

using System.Linq.Expressions;

namespace OrderManager.Data.Models.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Guid id);
        void Insert(TEntity obj);
        void Update(TEntity obj);
        void  Delete(TEntity obj);
        void Save();
        void BulkInsert(IList<TEntity> entity);
        Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes);
        TEntity? GetSingle(Func<TEntity, bool> predicate);
        IEnumerable<TEntity?> GetAll(Func<TEntity, bool> predicate);
        TEntity? GetSingle(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity?> GetAll(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes);
    }
}
