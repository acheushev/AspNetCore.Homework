using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Homework.Components
{
    public class Breadcrumbs : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var controller = ViewContext.RouteData.Values["Controller"];
            var action = ViewContext.RouteData.Values["Action"];

            if (controller is null||controller.ToString().ToLower() == "home")
                return View(model: (string)null);
            if (action.ToString().ToLower() == "index")
                return View(model: $"Home --> {controller}");
            else
                return View(model: $"Home --> {controller} --> {action}");
        }
    }
}
