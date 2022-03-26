namespace WebStore.ViewModels
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string CreateTime { get; set; } = string.Empty;
        public string CreateDay { get; set; } = string.Empty;
        public int StarsCount { get; set; }
        public string ImgSource { get; set; } = string.Empty;
        public string[] Body { get; set; }
    }
}
