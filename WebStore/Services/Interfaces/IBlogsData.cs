using WebStore.Models;

namespace WebStore.Services.Interfaces
{
    public interface IBlogsData
    {
        IEnumerable<Blog> GetAll();
        Blog GetById(int id);
        int Add(Blog blog);
    }
}
