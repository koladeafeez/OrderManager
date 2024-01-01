
using OrderManager.Data.Models;
using OrderManager.Data.Models.Repositories;
using System.Linq.Expressions;

namespace OrderManager.Core.Services
{
    public class BaseService<TEntity, TCreate>(IBaseRepository<TEntity> baseRepo, IResponseFactory response) : IBaseService<TEntity, TCreate>
        where TEntity : EntityBase
    {
        protected readonly IBaseRepository<TEntity> baseRepository = baseRepo;
        protected readonly IResponseFactory _response = response;

        public async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await baseRepository.GetAllAsync();

        public async Task<TEntity?> GetByIdAsync(Guid id) =>
            await baseRepository.GetByIdAsync(id);

        public async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes) =>
            await baseRepository.GetByIdAsync(id,includes);


        public IEnumerable<TEntity?> GetMultiple(Func<TEntity, bool> predicate) =>
            baseRepository.GetAll(predicate);

        public IEnumerable<TEntity?> GetMultiple(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes) =>
           baseRepository.GetAll(predicate);

        public TEntity? GetSingle(Func<TEntity, bool> predicate) =>
            baseRepository.GetSingle(predicate);

        public TEntity? GetSingle(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes) =>
         baseRepository.GetSingle(predicate, includes);

        public void Delete(TEntity entity) =>
             baseRepository.Delete(entity);

        public virtual void BulkInsert(TCreate model)
        { }

    }
}
