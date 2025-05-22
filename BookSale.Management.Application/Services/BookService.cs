using BookSale.Management.Application.DTOs;
using BookSale.Management.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BookDto>> GetBookList()
        {
            var data = await _unitOfWork.BookRepository.GetAllBooks();

            return data.Select(x => new BookDto
            {
                Id = x.Id,
                Code = x.Code,
                Title = x.Title,
                Author = x.Author,
                Available = x.Available,
                Publisher = x.Publisher,
                Cost = x.Cost
            });
        }
    }
}
