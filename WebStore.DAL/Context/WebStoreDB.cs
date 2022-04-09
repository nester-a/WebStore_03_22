using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using WebStore.Models;

namespace WebStore.DAL.Context
{
    public class WebStoreDB : DbContext
    {
        string cs = "Server=localhost;Database=master;Trusted_Connection=True;";
        public DbSet<Product> Products { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public WebStoreDB(DbContextOptions<WebStoreDB> options) : base(options) { }

        //protected override void OnModelCreating(ModelBuilder model)
        //{
        //    base.OnModelCreating(model);

        //    //model.Entity<Sections>();
        //}
    }
}
