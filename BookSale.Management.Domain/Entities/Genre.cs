using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.Domain.Entities
{
    public class Genre : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
