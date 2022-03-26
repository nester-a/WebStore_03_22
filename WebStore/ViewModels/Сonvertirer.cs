using System.Text;
using WebStore.Models;

namespace WebStore.ViewModels
{
    public static class Сonvertirer
    {
        public static BlogViewModel BlogToViewModel(Blog blog)
        {
            var viewModel = new BlogViewModel()
            {
                Id = blog.Id,
                Title = blog.Title,
                User = blog.User,
                CreateDay = blog.Created.Date.ToString(),
                CreateTime = blog.Created.TimeOfDay.ToString(),
                StarsCount = blog.StarsCount,
                ImgSource = blog.ImgSource,
                Body = blog.Body.Split('\n'),
            };

            return viewModel;
        }

        public static Blog ViewModelToBlog(BlogViewModel viewModel)
        {
            var blog = new Blog()
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                User = viewModel.User,
                Created = DateTimeConverter(viewModel.CreateDay, viewModel.CreateTime),
                StarsCount= viewModel.StarsCount,
                ImgSource= viewModel.ImgSource,
                Body = BodyConverter(viewModel.Body),

            };
            return blog;
        }
        private static DateTime DateTimeConverter(string day, string time)
        {
            string[] dayArray = day.Split('/');
            string[] timeArray = time.Split(':');
            int year = int.Parse(dayArray[0]);
            int month = int.Parse(dayArray[1]);
            int thisDay = int.Parse(dayArray[2]);
            int hour = int.Parse(timeArray[0]);
            int min = int.Parse(timeArray[1]);
            int sec = int.Parse(timeArray[2]);
            return new DateTime(year, month,thisDay, hour, min, sec);
        }
        private static string BodyConverter(string[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in array)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }
    }
}
