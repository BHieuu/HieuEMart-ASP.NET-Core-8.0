using HieuEMart.Models;
using HieuEMart.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HieuEMart.Controllers
{
	public class BrandController : Controller
	{
		private readonly DataContext _dataContext;
		public BrandController(DataContext context)
		{
			_dataContext = context;

		}
		public async Task<IActionResult> Index(string Slug = "")
		{
			BrandModel brand = _dataContext.Brands.Where(c => c.Slug == Slug).FirstOrDefault();

			if (brand == null) return RedirectToAction("Index");

			var ProductByBrand = _dataContext.Products.Where(p => p.BrandId == brand.Id);

			return View(await ProductByBrand.OrderByDescending(p => p.Id).ToListAsync());
		}
	}
}
