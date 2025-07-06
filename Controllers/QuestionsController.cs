using Microsoft.AspNetCore.Mvc;

namespace DNU_QnA_MVC_App.Controllers
{
    public class QuestionsController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}