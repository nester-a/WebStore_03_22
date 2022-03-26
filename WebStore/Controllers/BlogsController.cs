using Microsoft.AspNetCore.Mvc;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class BlogsController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult WebStoreBlog() => View();
        public IActionResult Create() => View(new BlogViewModel());
        [HttpPost]
        public IActionResult Create(BlogViewModel model)
        {
            return View(model);
        }
    }
}
