using Microsoft.AspNetCore.Mvc;
using NewsWeb.Models;
using NewsWeb.Services;
using NewsWeb.ViewModels;

namespace NewsWeb.Controllers
{
    public class NewsController : Controller
    {
        private readonly ICategoryInterface categoryInterface;
        private readonly INewsInterface newsInterface;
        private readonly IImageInterface imageInterface;

        public NewsController(ICategoryInterface categoryInterface,
                              INewsInterface newsInterface,
                              IImageInterface imageInterface)
        {
            this.categoryInterface = categoryInterface;
            this.newsInterface = newsInterface;
            this.imageInterface = imageInterface;
        }
        public IActionResult Index()
        {
            var news = newsInterface.GetNews();
            return View(news);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddNewsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                News news = new()
                {
                    Id = Guid.NewGuid(),
                    Title = viewModel.Title,
                    Body = viewModel.Body,
                    CategoryId = viewModel.CategoryId,
                    PostedDate = DateTime.Now,
                    NumberOfViews = 0,
                    ImagePath = imageInterface.SaveImage(viewModel.ImageFile)
                };
                newsInterface.AddNews(news);

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(Guid id)
        {
            var news = newsInterface.GetNewsById(id);
            imageInterface.DeleteImage(news.ImagePath);
            newsInterface.DeleteNews(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var news = newsInterface.GetNewsById(id);

            return View((EditNewsViewModel)news);
        }

        [HttpPost]
        public IActionResult Edit(EditNewsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.ImageFile is not null)
                {
                    imageInterface.DeleteImage(viewModel.ImagePath);
                    viewModel.ImagePath = imageInterface.SaveImage(viewModel.ImageFile);
                }

                viewModel.PostedDate = DateTime.Now;
                newsInterface.UpdateNews((News)viewModel);

                return RedirectToAction("Index");
            }

            return RedirectToAction("Edit", viewModel.Id);
        }
    }
}
