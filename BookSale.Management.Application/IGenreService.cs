using BookSale.Management.Application.DTOs;

namespace BookSale.Management.Application
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetGenreList();
    }
}