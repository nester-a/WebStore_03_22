using WebStore.Data;
using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
    public class InMemoryBlogsData : IBlogsData
    {
        private readonly ILogger<InMemoryBlogsData> logger;
        int maxId;

        public InMemoryBlogsData(ILogger<InMemoryBlogsData> logger)
        {
            this.logger = logger;
            maxId = TestData.Blogs.Max(blog => blog.Id);
        }

        public int Add(Blog blog)
        {
            if (blog is null) throw new ArgumentNullException(nameof(blog));

            if (TestData.Blogs.Contains(blog)) return blog.Id;

            blog.Id = ++maxId;
            TestData.Blogs.Add(blog);


            return blog.Id;
        }

        public IEnumerable<Blog> GetAll()
        {
            return TestData.Blogs;
        }

        public Blog GetById(int id)
        {
            return TestData.Blogs.SingleOrDefault(blog => blog.Id == id);
        }
    }
}
