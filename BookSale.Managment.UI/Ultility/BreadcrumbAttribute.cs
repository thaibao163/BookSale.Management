using BookSale.Management.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookSale.Management.UI.Ultility
{
    public class BreadcrumbAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly string _title;
        private readonly string _masterName;

        public BreadcrumbAttribute(string title, string masterName = "")
        {
            _title = title;
            _masterName = masterName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is Controller controller)
            {
                string controllerName = controller.GetType().Name.Replace("Controller", "");

                string path = string.IsNullOrEmpty(_masterName) ? $"{controllerName}" : $"{_masterName}/{controllerName}/{_title}";

                controller.ViewData["Breadcrumb"] = new BreadcrumbModel
                {
                    Title = _title,
                    Path = path
                };

                //link click

                //controller.ViewData["Breadcrumb"] = new BreadcrumbModel
                //{
                //    Title = "Account List",
                //    Path = "Apps/Account/Account List",
                //    UrlMap = new Dictionary<string, string>
                //        {
                //            { "Apps", "/apps" },
                //            { "Account", "/apps/account" },
                //            { "Account List", "/apps/account/list" }
                //        }
                //};
            }
        }

    }
}
