using HieuEMart.Models;
using HieuEMart.Models.ViewModels;
using HieuEMart.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HieuEMart.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly DataContext _dataContext;
		public CheckoutController(DataContext context)
		{
			_dataContext = context;

		}
		public async Task<IActionResult> Checkout()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			if (userEmail == null)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var orderCode = Guid.NewGuid().ToString();
				var orderItem = new OrderModel();
				orderItem.OrderCode = orderCode;
				orderItem.UserName = userEmail;
				orderItem.Status = 1;
				orderItem.CreatedDate = DateTime.Now;
				_dataContext.Add(orderItem);
				_dataContext.SaveChanges();
				List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
				foreach (var cart in cartItems) 
				{
					var orderdetails = new OrderDetails();
					orderdetails.UserName = userEmail;
					orderdetails.OrderCode = orderCode;
					orderdetails.ProductId = cart.ProductId;
					orderdetails.Price = cart.Price;
					orderdetails.Quantity = cart.Quantity;
					_dataContext.Add(orderdetails);
					_dataContext.SaveChanges();
				}
				HttpContext.Session.Remove("Cart");
				TempData["success"] = "Đặt hàng thành công. Vui lòng chờ duyệt đơn hàng";
				return RedirectToAction("Index", "Cart");
			}
			return View();
		}
	}
}