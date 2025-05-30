using BookSale.Management.Application;
using BookSale.Management.Application.DTOs;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public AccountController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [Breadcrumb("Account List","Apps")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAccountPagination(RequestDataTable requestDataTable)
        {
            var data = await _userService.GetUserByPagination(requestDataTable);

            return Json(data);
        }

        [HttpGet]
        [Breadcrumb("Account Form", "Apps")]
        public async Task<IActionResult> SaveDataAsync(string? id)
        {
            AccountDto accountDto = !string.IsNullOrEmpty(id) ? await _userService.GetUserById(id) : new();

            ViewBag.Roles = await _roleService.GetRoleForDropdownlist();

            return View(accountDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveData(AccountDto accountDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _roleService.GetRoleForDropdownlist();
                ModelState.AddModelError("errorsModel", "Invalid model");
            }

            var result = await _userService.Save(accountDto);

            if (result.Status)
            {
                return RedirectToAction("Index", "Account");
            }

            ViewBag.Roles = await _roleService.GetRoleForDropdownlist();
            ModelState.AddModelError("errorsModel", result.Message);

            return View(accountDto);
        }
    }
}
