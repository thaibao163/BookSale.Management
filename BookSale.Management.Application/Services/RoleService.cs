using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookSale.Management.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<SelectListItem>> GetRoleForDropdownlist()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            });
        }
    }
}
