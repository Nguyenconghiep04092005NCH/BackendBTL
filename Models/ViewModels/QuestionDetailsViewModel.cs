using System;
using System.Collections.Generic;

namespace DNU_QnA_MVC_App.Models.ViewModels
{
    public class QuestionDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string UserFullName { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public int VoteCount { get; set; }
        public List<string> Tags { get; set; } = new();
        public List<AnswerViewModel> Answers { get; set; } = new();
        
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Views { get; set; }
        public int AnswersCount { get; set; }
        public int Votes { get; set; }
        public object AvatarUrl { get; }
    }
}