namespace DNU_QnA_MVC_App.Models.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}