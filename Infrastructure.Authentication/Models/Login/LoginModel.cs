using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Authentication.Models.Login
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Flag { get; set; }
    }
}
