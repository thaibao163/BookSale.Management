using BookSale.Management.Domain.Abstract;
using BookSale.Management.Domain.Entities;
using BookSale.Managment.DataAccess.Data;

namespace BookSale.Managment.DataAccess.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await GetAll();
        }
    }
}
