using HieuEMart.Models;
using HieuEMart.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HieuEMart.Areas.Admin.Models
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        public ProductController(DataContext context)
        {
            _dataContext = context;

        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Products.OrderByDescending(p => p.Id).Include(p => p.Category).ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name");
            ViewBag.brands = new SelectList(_dataContext.Brands, "Id", "Name");
            return View();
        }
        public async Task<IActionResult> CreateProduct(ProductModel product)
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);
            return View(product);
        }
    }
}
