using System.ComponentModel.DataAnnotations;

namespace DNU_QnA_MVC_App.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự.")]
        public string Password { get; set; } = null!;

        [Required]
        public string Token { get; set; } = null!;
    }
}