using BookSale.Management.Application;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModels;
using BookSale.Management.Domain.Abstract;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [Breadcrumb("Genre List", "Application")]
        public IActionResult Index()
        {
            var genres = new GenreViewModels();
            return View(genres);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return Json(await _genreService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> GetGenrePagination(RequestDataTable requestDataTable)
        {
            var data = await _genreService.GetGenrePagination(requestDataTable);

            return Json(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveData(GenreViewModels genreViewModels)
        {
            if(ModelState.IsValid)
            {
                var data = genreViewModels;
            }
            return Json(1);
        }
    }
}
