using Microsoft.AspNetCore.Mvc;

namespace DNU_QnA_MVC_App.Controllers
{
    public class GuestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}