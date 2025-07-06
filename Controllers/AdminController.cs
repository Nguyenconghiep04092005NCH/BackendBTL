using Microsoft.AspNetCore.Mvc;
using DNU_QnA_MVC_App.Models.ViewModels;
using DNU_QnA_MVC_App.Data;

namespace DNU_QnA_MVC_App.Controllers
{
    public class AdminController : Controller
    {
        private readonly DnuQnADbContext _context;

        public AdminController(DnuQnADbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            var totalUsers = _context.Users.Count();
            var totalQuestions = _context.Questions.Count();
            var totalAnswers = _context.Answers.Count();
            var totalTags = _context.Tags.Count();

            var model = new AdminDashboardViewModel
            {
                TotalUsers = totalUsers,
                TotalQuestions = totalQuestions,
                TotalAnswers = totalAnswers,
                TotalTags = totalTags
            };

            return View(model);
        }

        public IActionResult ManageUsers()
        {
            var users = _context.Users
                .Select(u => new UserSummaryViewModel
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    Reputation = u.Reputation
                })
                .ToList();

            return View(users);
        }
        
    }
}