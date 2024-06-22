using HieuEMart.Repository;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return View();
        }
    }
}
