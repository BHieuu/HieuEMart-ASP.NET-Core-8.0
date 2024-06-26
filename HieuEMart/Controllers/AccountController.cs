using HieuEMart.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HieuEMart.Controllers
{
	public class AccountController : Controller
	{
		private UserManager<AppUserModel> _userManager;
		private SignInManager<AppUserModel> _signInManager;
		public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager) 
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Create()
		{
			return View();
		}
		public async Task<IActionResult> Login()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(UserModel user)
		{
			 if (ModelState.IsValid) 
			{
				AppUserModel newUser = new AppUserModel { UserName =user.UserName, Email = user.Email };
				IdentityResult result = await _userManager.CreateAsync(newUser);
				if (result.Succeeded)
				{
					TempData["success"] = "Tạo tài khoản thành công";
					return Redirect("/account");
				}
				foreach(IdentityError error in result.Errors) 
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View(user);
		}
	}
}
