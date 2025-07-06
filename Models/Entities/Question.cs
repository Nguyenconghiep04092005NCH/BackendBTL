using System;
using System.Collections.Generic;

namespace DNU_QnA_MVC_App.Models.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }

        public User? User { get; set; }
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}