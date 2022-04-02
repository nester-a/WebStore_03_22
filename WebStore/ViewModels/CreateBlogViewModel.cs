namespace WebStore.ViewModels
{
    public class CreateBlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string ImgSource { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
