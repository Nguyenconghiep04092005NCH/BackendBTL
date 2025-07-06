using System.ComponentModel.DataAnnotations;

namespace DNU_QnA_MVC_App.Models.InputModels
{
    public class EditProfileInputModel
    {
        [Required]
        public string FullName { get; set; } = null!;

        public string? AvatarUrl { get; set; }
    }
}