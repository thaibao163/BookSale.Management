using BookSale.Management.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetBookList();
    }
}
