namespace DNU_QnA_MVC_App.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public int Reputation { get; set; }
        public string Role { get; set; }
        public string? AvatarUrl { get; set; }

        public string? EmailConfirmationToken { get; set; }
        public bool EmailConfirmed { get; set; } = false;

        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }

        // Navigation properties
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}