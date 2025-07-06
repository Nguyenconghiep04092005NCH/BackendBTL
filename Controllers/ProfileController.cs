using Microsoft.AspNetCore.Mvc;

namespace DNU_QnA_MVC_App.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}namespace DNU_QnA_MVC_App.Models.ViewModels
{
    public class ProfileViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Reputation { get; set; }
        public string Role { get; set; }
    }
}
