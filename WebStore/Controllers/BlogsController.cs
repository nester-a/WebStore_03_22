using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Blog, BlogViewModel>()
                .ForMember("CreateTime", opt => opt.MapFrom(ct => ct.Created.ToShortTimeString()))
                .ForMember("CreateDay", opt => opt.MapFrom(cd => cd.Created.ToShortDateString()))
                .ForMember("Body", opt => opt.MapFrom(bd => Сonvertirer.BlogBodyConverter(bd.Body))));
            var mapper = new Mapper(config);
            var allBlogs = mapper.Map<List<BlogViewModel>>(blogs.GetAll());

            
            return View(allBlogs);
        }

        public IActionResult WebStoreBlog(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Blog, BlogViewModel>()
                .ForMember("CreateTime", opt => opt.MapFrom(ct => ct.Created.ToShortTimeString()))
                .ForMember("CreateDay", opt => opt.MapFrom(cd => cd.Created.ToShortDateString()))
                .ForMember("Body", opt => opt.MapFrom(bd => Сonvertirer.BlogBodyConverter(bd.Body))));
            var mapper = new Mapper(config);

            var blog = mapper.Map<BlogViewModel>(blogs.GetById(id));

            if (blog is null) return NotFound();
            return View(blog);
        }

        public IActionResult Create() => View(new CreateBlogViewModel());

        [HttpPost]
        public IActionResult Create(CreateBlogViewModel model)
        {
            var newModel = new CreateBlogViewModel
            {
                Id = model.Id,
                Title = model.Title,
                User = model.User,
                ImgSource = model.ImgSource,
                Body = model.Body,
            };

            if (newModel.Id == 0) 
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateBlogViewModel, Blog>());
                var mapper = new Mapper(config);
                var newBlog = mapper.Map<Blog>(newModel);
                blogs.Add(newBlog);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
