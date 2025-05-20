using BookSale.Management.Domain.Abstract;
using BookSale.Management.Domain.Entities;
using BookSale.Managment.DataAccess.Data;

namespace BookSale.Managment.DataAccess.Repository
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await base.GetAll(x => x.IsActive);
        }
    }
}
