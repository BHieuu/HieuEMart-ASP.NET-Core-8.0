using System.ComponentModel.DataAnnotations;

namespace HieuEMart.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Yêu cầu nhập Tên đăng nhập")]
		public string UserName { get; set; }
		[DataType(DataType.Password), Required(ErrorMessage = "Yêu cầu nhập Mật khẩu")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Yêu cầu nhập Email"), EmailAddress]
		public string Email { get; set; }
	}
}
