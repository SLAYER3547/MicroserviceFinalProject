using System.ComponentModel.DataAnnotations;

namespace Mango.Services.IdentityServer.Models
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Username is required.")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords do not match.")]
		public string ConfirmPassword { get; set; }

		[EmailAddress(ErrorMessage = "Invalid email address.")]
		public string Email { get; set; }
	}

}
