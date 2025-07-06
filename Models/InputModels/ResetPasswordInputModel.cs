using System.ComponentModel.DataAnnotations;

namespace DNU_QnA_MVC_App.Models.ViewModels
{
    public class ResetPasswordInputModel
    {
        [Required(ErrorMessage = "Email không được bỏ trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [MinLength(8, ErrorMessage = "Mật khẩu tối thiểu 8 ký tự")]
        public string Password { get; set; }

        [Required]
        public string Token { get; set; }
    }
}