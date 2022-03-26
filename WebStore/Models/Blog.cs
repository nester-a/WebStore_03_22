namespace WebStore.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public int StarsCount { get; set; }
        public string ImgSource { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
