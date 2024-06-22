using HieuEMart.Models;
using HieuEMart.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HieuEMart.Controllers
{
    public class CategoryController : Controller
    {
		private readonly DataContext _dataContext;
        public CategoryController(DataContext context)
        {
			_dataContext = context; 

		}
		public async Task <IActionResult> Index(string Slug = "")
        {
            CategoryModel category = _dataContext.Categories.Where( c => c.Slug == Slug ).FirstOrDefault();

            if (category == null) return RedirectToAction("Index");

            var ProductByCategory = _dataContext.Products.Where(p => p.CategoryId == category.Id);

            return View(await ProductByCategory.OrderByDescending(p => p.Id).ToListAsync());
        }
    }
}
