using BookSale.Management.Application.DTOs;
using BookSale.Management.Domain.Abstract;

namespace BookSale.Management.Application
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GenreDto>> GetGenreList()
        {
            var data = await _unitOfWork.GenreRepository.GetAllGenres();

            return data.Select(x => new GenreDto
            {
                Id = x.Id,
                Name = x.Name
            });
        }
    }
}
