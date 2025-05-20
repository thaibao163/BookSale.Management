using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSale.Management.Domain.Entities
{
    public class Book : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string Code { get; set; }

        [Required]
        [StringLength(1000)]
        public string? Title { get; set; }

        [Required]
        public int Available { get; set; }

        [Required]
        public double Cost { get; set; }

        [StringLength(250)]
        public string? Publisher { get; set; }

        [Required]
        [StringLength(1000)]
        public string Author { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public int GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; }
    }
}
