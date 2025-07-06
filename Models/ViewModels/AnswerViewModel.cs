using System;

namespace DNU_QnA_MVC_App.Models.ViewModels
{
    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string AuthorName { get; set; } = null!;
        public object UserFullName { get; set; }
        public object UserAvatarUrl { get; set; }
        public object VoteCount { get; set; }
        public bool IsAccepted { get; set; }
    }
}