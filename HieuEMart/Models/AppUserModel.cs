using Microsoft.AspNetCore.Identity;

namespace HieuEMart.Models
{
	public class AppUserModel : IdentityUser
	{
		public string Occupation { get; set; }
	}
}
