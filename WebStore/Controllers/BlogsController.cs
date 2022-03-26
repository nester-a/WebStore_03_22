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
        public IActionResult Index()
        {
            var allBlogs = new List<BlogViewModel>();
            foreach (var blog in blogs.GetAll())
            {
                allBlogs.Add(Сonvertirer.BlogToViewModel(blog));
            }
            return View(allBlogs);
        }

        public IActionResult WebStoreBlog(int id)
        {
            var blog = blogs.GetById(id);
            if (blog is null) return NotFound();
            return View(Сonvertirer.BlogToViewModel(blog));
        }
        public IActionResult Create() => View(new BlogViewModel());
        [HttpPost]
        public IActionResult Create(BlogViewModel model)
        {
            return View(model);
        }
    }
}
