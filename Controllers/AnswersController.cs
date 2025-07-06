using Microsoft.AspNetCore.Mvc;
using DNU_QnA_MVC_App.Data;
using DNU_QnA_MVC_App.Models.InputModels;

namespace DNU_QnA_MVC_App.Controllers
{
    public class AnswersController : Controller
    {
        private readonly DnuQnADbContext _context;

        public AnswersController(DnuQnADbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(CreateAnswerInputModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // TODO: Save answer to DB

            return RedirectToAction("Details", "Questions", new { id = model.QuestionId });
        }
    }
}