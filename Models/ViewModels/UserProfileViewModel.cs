namespace DNU_QnA_MVC_App.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public int Reputation { get; set; }

        public List<QuestionSummaryViewModel> Questions { get; set; } = new();
        public List<AnswerSummaryViewModel> Answers { get; set; } = new();
        public object UserFullName { get; set; }
    }

    public class QuestionSummaryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public object VoteCount { get; set; }
        public IEnumerable<string?> Tags { get; set; }
    }

    public class AnswerSummaryViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}