using System.ComponentModel.DataAnnotations;

namespace DNU_QnA_MVC_App.Models.InputModels
{
    public class CreateQuestionInputModel
    {
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        public List<string> Tags { get; set; }
    }
}