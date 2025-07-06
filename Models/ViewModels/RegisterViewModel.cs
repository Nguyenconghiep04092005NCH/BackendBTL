// DNU_QnA_MVC_App.Models.ViewModels.RegisterViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace DNU_QnA_MVC_App.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(8, ErrorMessage = "Mật khẩu ít nhất 8 ký tự")]
        public string Password { get; set; }
    }
}