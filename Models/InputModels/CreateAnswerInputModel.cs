namespace DNU_QnA_MVC_App.Models.InputModels
{
    public class CreateAnswerInputModel
    {
        public int QuestionId { get; set; }
        public string Content { get; set; } = null!;
    }

    public class UpdateAnswerInputModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
    }
    
}