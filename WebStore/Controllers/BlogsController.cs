using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogsData blogs;
        private readonly ILogger<BlogsController> logger;

        public BlogsController(IBlogsData blogs, ILogger<BlogsController> logger)
        {
            this.blogs = blogs;
            this.logger = logger;
        }
        public IActionResult Index() => View(blogs.GetAll());
        public IActionResult WebStoreBlog() => View();
        public IActionResult Create() => View(new BlogViewModel());
        [HttpPost]
        public IActionResult Create(BlogViewModel model)
        {
            return View(model);
        }
    }
}
