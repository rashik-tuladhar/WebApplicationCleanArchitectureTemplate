using System.ComponentModel.DataAnnotations;

namespace Infrastructure.CoreSetup.DTOs
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Old is required")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "New Password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Re-Type Password is required")]
        [Compare("NewPassword", ErrorMessage = "Password Miss Matched")]
        [DataType(DataType.Password)]
        public string ReTypePassword { get; set; }
    }
}
