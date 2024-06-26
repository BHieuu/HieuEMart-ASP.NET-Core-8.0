using HieuEMart.Models;
using HieuEMart.Models.ViewModels;
using HieuEMart.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HieuEMart.Controllers
{
	public class CartController : Controller
	{
		private readonly DataContext _dataContext;
		public CartController(DataContext _context)
		{
			_dataContext = _context;

		}
		public IActionResult Index()
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemViewModel cartVM = new()
			{
				CartItems = cartItems,
				GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)
			};
			return View(cartVM);
		}
		public ActionResult Checkout()
		{
			return View("~/Views/Checkout/Index.cshtml");
		}
		public async Task<IActionResult> Add(int Id)
		{
			ProductModel product = await _dataContext.Products.FindAsync(Id);
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel cartItems = cart.Where(c => c.ProductId == Id).FirstOrDefault();
			if (cartItems == null)
			{
				cart.Add(new CartItemModel(product));
			}
			else
			{
				cartItems.Quantity += 1;
			}
			HttpContext.Session.SetJson("Cart", cart);
			return Redirect(Request.Headers["Referer"].ToString());
		}
		public async Task<IActionResult> Decrease(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");

			CartItemModel carItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();

			if (carItem != null)
			{
				if (carItem.Quantity > 1)
				{
					--carItem.Quantity;
				}
				else
				{
					cart.Remove(carItem);
				}
			}
			HttpContext.Session.SetJson("Cart", cart);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Increase(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");

			CartItemModel carItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();

			if (carItem.Quantity != null)
			{
				++carItem.Quantity;
			}
			HttpContext.Session.SetJson("Cart", cart);

			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Remove(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			{
				cart.RemoveAll(p => p.ProductId == Id);
				if (cart.Count == 0)
				{
					HttpContext.Session.Remove("Cart");
				}
				else
				{
					HttpContext.Session.SetJson("Cart", cart);
				}
			}
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Clear(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			{
				HttpContext.Session.Remove("Cart");
				TempData["error"] = "Xóa thành công toàn bộ sản phẩm!";
				return RedirectToAction("Index");
                
            }
		}
	}
}