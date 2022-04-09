using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InSQL
{
    public class SqlBlogsData : IBlogsData
    {
        private readonly WebStoreDB db;
        private readonly ILogger<SqlBlogsData> logger;

        public SqlBlogsData(WebStoreDB db, ILogger<SqlBlogsData> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public int Add(Blog blog)
        {
            return AddAsync(blog).Result;
        }

        public async Task<int> AddAsync(Blog blog)
        {
            if (blog is null) throw new ArgumentNullException(nameof(blog));

            if (db.Blogs.Contains(blog)) return blog.Id;

            blog.Id = db.Blogs.Max(b => blog.Id) + 1;

            logger.LogInformation("Запись блога в БД...");
            await using (await db.Database.BeginTransactionAsync())
            {
                db.Blogs.Add(blog);

                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Blogs] ON");

                await db.SaveChangesAsync();

                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Blogs] OFF");

                await db.Database.CommitTransactionAsync();
            }
            logger.LogInformation("Запись блога в БД выполнена");

            return blog.Id;
        }
        public IEnumerable<Blog> GetAll()
        {
            IQueryable<Blog> query = db.Blogs;

            return query;
        }

        public Blog GetById(int id)
        {
            Blog query = db.Blogs.SingleOrDefault(b => b.Id == id);
            return query;
        }
    }
}
