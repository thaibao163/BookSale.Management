using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
