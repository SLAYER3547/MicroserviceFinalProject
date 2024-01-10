using Mango.Services.AuthAPI.Models.Dto;

namespace Mango.Web.Models
{
    public class LoginResponseDto
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
