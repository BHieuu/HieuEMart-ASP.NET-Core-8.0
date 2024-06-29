using System.ComponentModel.DataAnnotations;

namespace HieuEMart.Models.ViewModels
{
	public class LoginViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Yêu cầu nhập Tên đăng nhập")]
		public string UserName { get; set; }
		[DataType(DataType.Password), Required(ErrorMessage = "Yêu cầu nhập Mật khẩu")]
		public string Password { get; set; }
		public string ReturnUrl { get; set; }
	}
}
