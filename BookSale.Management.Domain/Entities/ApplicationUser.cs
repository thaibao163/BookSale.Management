using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(10)]
        public string? MobilePhone { get; set; }

        [StringLength(1000)]
        public string? Address { get; set; }

        public bool IsActive { get; set; }
    }
}
