using BookSale.Management.Domain.Entities;

namespace BookSale.Managment.DataAccess.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
    }
}