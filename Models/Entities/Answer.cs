namespace DNU_QnA_MVC_App.Models.Entities
{
    public class Answer
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; } = null!;

        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}