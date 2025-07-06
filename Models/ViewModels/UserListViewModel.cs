namespace DNU_QnA_MVC_App.Models.ViewModels
{
    public class UserListViewModel
    {
        public List<UserSummaryViewModel> Users { get; set; } = new();
    }

    public class UserSummaryViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Reputation { get; set; }
    }
}