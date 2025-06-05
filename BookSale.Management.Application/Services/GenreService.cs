using AutoMapper;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModels;
using BookSale.Management.Domain.Abstract;

namespace BookSale.Management.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenreViewModels> GetById(int id)
        {
            var genre = await _unitOfWork.GenreRepository.GetById(id);
            if(genre == null)
            {
                return null;
            }
            return _mapper.Map<GenreViewModels>(genre);
        }

        public async Task<ResponseDataTable<GenreDto>> GetGenrePagination(RequestDataTable requestDataTable)
        {
            var allGenres = await _unitOfWork.GenreRepository.GetAllGenres();

            var genres = allGenres.Where(x => x.IsActive);

            var genreDTO = _mapper.Map<IEnumerable<GenreDto>>(genres);

            int totalRecords = genreDTO.Count();

            var result = genreDTO.Skip(requestDataTable.SkipItems).Take(requestDataTable.PageSize).ToList();

            return new ResponseDataTable<GenreDto>
            {
                Draw = requestDataTable.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = totalRecords,
                Data = result
            };
        }
    }
}
