using System.ComponentModel.DataAnnotations;

namespace DNU_QnA_MVC_App.Models.InputModels
{
    public class ForgotPasswordInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}