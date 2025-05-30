using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DashboardController : Controller
    {
        [HttpGet]
        [Breadcrumb("Dashboard")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
 