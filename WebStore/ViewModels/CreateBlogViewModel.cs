using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class CreateBlogViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Заголовок не указан")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Автор")]
        [Required(ErrorMessage = "Автор не указан")]
        public string User { get; set; } = string.Empty;

        [Display(Name = "Картинка")]
        public string? ImgSource { get; set; }


        [Display(Name = "Текст")]
        public string Body { get; set; } = string.Empty;
    }
}
