
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace OrderManager.Data.Models.Repositories
{
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8604 // Possible null reference argument.
    public class BaseRepository<TEntity>(AppDbContext appDbContext) : IBaseRepository<TEntity> 
        where TEntity : EntityBase
    {
        private readonly AppDbContext _context = appDbContext;

        public async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await _context.Set<TEntity>().AsSplitQuery().AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(Guid id) => 
            await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public void Insert(TEntity entity) =>
            _context.Set<TEntity>().Add(entity);


        public void BulkInsert(IList<TEntity> entity)
        {
            _context.Set<TEntity>().AddRange(entity);
        }

        public void Update(TEntity entity) =>
             _context.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity) =>
            _context.Set<TEntity>().Remove(entity);


        public async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
        {
                var query = _context.Set<TEntity>().AsNoTracking();
                return await includes
                    .Aggregate(
                        query?.AsQueryable(),
                        (current, include) => current.Include(include)
                    )?.FirstOrDefaultAsync(e => e.Id == id);
        }



        public TEntity? GetSingle(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity?> GetAll(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).ToList();
        }

        public TEntity? GetSingle(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>().AsNoTracking();
            return includes
                   .Aggregate(
                       query?.AsQueryable(),
                       (current, include) => current.Include(include)
                   )?.FirstOrDefault(predicate);
        }


        public IEnumerable<TEntity?> GetAll(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>().AsNoTracking().AsSplitQuery();
            return includes
                  .Aggregate(
                      query?.AsQueryable(),
                      (current, include) => current.Include(include)
                  ).Where(predicate)?.ToList();

        }

        public void Save()
        {
            try
            {
                 _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8603 // Possible null reference return.
}
