using BookSale.Managment.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookSale.Managment.DataAccess.Repository
{
    public class GenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        //ls.where(x=>x.name=="xyz").ToList()
        public async Task<List<T>> GetAll(Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
            {
                return await _applicationDbContext.Set<T>().ToListAsync();
            }
            return await _applicationDbContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T?> GetSingleAsyns(Expression<Func<T, bool>> expression = null)
        {
            return await _applicationDbContext.Set<T>().SingleOrDefaultAsync(expression);
        }

        public async Task Create(T entity)
        {
            await _applicationDbContext.Set<T>().AddAsync(entity).ConfigureAwait(false);
        }

        public async void Update(T entity)
        {
            _applicationDbContext.Set<T>().Attach(entity);
            _applicationDbContext.Entry(entity).State=EntityState.Modified;
        }

        public async void Delete(T entity)
        {
            _applicationDbContext.Set<T>().Attach(entity);
            _applicationDbContext.Entry(entity).State = EntityState.Deleted;
        }

        public async Task Commit()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

    }
}
