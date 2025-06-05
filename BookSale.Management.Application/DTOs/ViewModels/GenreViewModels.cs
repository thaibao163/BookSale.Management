using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.Application.DTOs.ViewModels
{
    public class GenreViewModels
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Genre must be not empty")]
        public string Name { get; set; }
    }
}
