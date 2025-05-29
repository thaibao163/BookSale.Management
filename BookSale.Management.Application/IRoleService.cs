using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookSale.Management.Application
{
    public interface IRoleService
    {
        Task<IEnumerable<SelectListItem>> GetRoleForDropdownlist();
    }
}