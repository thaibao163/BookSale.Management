using BookSale.Managment.DataAccess.Repository;

namespace BookSale.Management.Domain.Abstract
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IGenreRepository GenreRepository { get; }

        Task SaveChangeAsync();
    }
}