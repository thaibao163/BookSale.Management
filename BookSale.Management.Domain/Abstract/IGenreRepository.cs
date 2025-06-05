using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Domain.Abstract
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenres();
        Task<Genre> GetById(int id);
    }
}