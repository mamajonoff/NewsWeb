using NewsWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace NewsWeb.ViewModels
{
    public class EditNewsViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Body { get; set; } = string.Empty;
        [Required]
        public IFormFile? ImageFile { get; set; }
        public DateTime PostedDate { get; set; }
        public string ImagePath { get; set; }
        [Required]
        public Guid CategoryId { get; set; }

        public static explicit operator EditNewsViewModel(News news)
        {
            return new EditNewsViewModel()
            {
                Id = news.Id,
                Title = news.Title,
                Body = news.Body,
                CategoryId = news.CategoryId,
                ImagePath = news.ImagePath,
                PostedDate = news.PostedDate,
                ImageFile = null
            };
        }

        public static explicit operator News(EditNewsViewModel viewModel)
        {
            return new News()
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Body = viewModel.Body,
                CategoryId = viewModel.CategoryId,
                ImagePath = viewModel.ImagePath,
                PostedDate = viewModel.PostedDate
            };
        }
    }
}
