using Microsoft.AspNetCore.Mvc;
using NewsWeb.Models;
using NewsWeb.Services;

namespace NewsWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryInterface categoryInterface;

        public CategoryController(ICategoryInterface categoryInterface)
        {
            this.categoryInterface = categoryInterface;
        }

        public IActionResult Index()
        {
            var list = categoryInterface.GetCategories();
            return View(list);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Id = Guid.NewGuid();
                categoryInterface.AddCategory(category);

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(Guid Id)
        {
            var category = categoryInterface.GetCategoryById(Id);   
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryInterface.UpdateCategory(category);

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(Guid Id)
        {
            categoryInterface.DeleteCategory(Id);
            return RedirectToAction("Index");
        }
    }
}
