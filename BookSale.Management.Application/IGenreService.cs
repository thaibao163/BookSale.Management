using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModels;

namespace BookSale.Management.Application
{
    public interface IGenreService
    {
        Task<GenreViewModels> GetById(int id);
        Task<ResponseDataTable<GenreDto>> GetGenrePagination(RequestDataTable requestDataTable);
    }
}