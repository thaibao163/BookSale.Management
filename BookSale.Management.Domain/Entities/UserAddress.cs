using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Domain.Entities
{
    public class UserAddress : BaseEntity
    {
        [Required]
        [StringLength(10)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(1000)]
        public string? Address { get; set; }

        [Required]
        [StringLength (1000)]
        public string? FullName { get; set; }

        [Required]
        [StringLength(1000)]
        public string Email { get; set; }

        public bool? IsActive { get; set; }

        [Required]
        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    }
}
