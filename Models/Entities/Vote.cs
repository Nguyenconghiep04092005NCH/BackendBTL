namespace DNU_QnA_MVC_App.Models.Entities
{
    public class Vote
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public int? QuestionId { get; set; }
        public virtual Question? Question { get; set; }

        public int? AnswerId { get; set; }
        public virtual Answer? Answer { get; set; }

        public bool IsUpVote { get; set; }
    }
}