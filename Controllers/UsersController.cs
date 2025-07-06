using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DNU_QnA_MVC_App.Data;
using DNU_QnA_MVC_App.Models.ViewModels;

namespace DNU_QnA_MVC_App.Controllers
{
    public class UsersController : Controller
    {
        private readonly DnuQnADbContext _context;

        public UsersController(DnuQnADbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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

            var vm = new UserListViewModel
            {
                Users = users
            };

            return View(vm);
        }

        public IActionResult Profile(int id)
        {
            var user = _context.Users
                .Include(u => u.Questions)
                .Include(u => u.Answers)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            var viewModel = new UserProfileViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Reputation = user.Reputation,
                AvatarUrl = user.AvatarUrl,
                Questions = user.Questions.Select(q => new QuestionSummaryViewModel
                {
                    Id = q.Id,
                    Title = q.Title,
                    CreatedAt = q.CreatedAt
                }).ToList(),
                Answers = user.Answers.Select(a => new AnswerSummaryViewModel
                {
                    Id = a.Id,
                    Content = a.Content,
                    CreatedAt = a.CreatedAt
                }).ToList()
            };

            return View(viewModel);
        }
    }
}
