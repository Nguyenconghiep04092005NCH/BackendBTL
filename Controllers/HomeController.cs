using Microsoft.AspNetCore.Mvc;
using DNU_QnA_MVC_App.Services.Interfaces;

namespace DNU_QnA_MVC_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuestionService _questionService;

        public HomeController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<IActionResult> Index(string? search, string? sort, int page = 1, int pageSize = 5)
        {
            var questions = await _questionService.GetAllQuestionsAsync();

            // Tìm kiếm
            if (!string.IsNullOrWhiteSpace(search))
            {
                questions = questions
                    .Where(q =>
                        q.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        q.Content.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        q.Tags.Any(t => t.Contains(search, StringComparison.OrdinalIgnoreCase))
                    )
                    .ToList();
            }

            // Sắp xếp
            questions = sort switch
            {
                "newest" => questions.OrderByDescending(x => x.CreatedAt).ToList(),
                "oldest" => questions.OrderBy(x => x.CreatedAt).ToList(),
                "mostvotes" => questions.OrderByDescending(x => x.VoteCount).ToList(),
                _ => questions.OrderByDescending(x => x.CreatedAt).ToList()
            };

            // Phân trang
            int totalItems = questions.Count;
            var pagedQuestions = questions
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.Search = search;
            ViewBag.Sort = sort;

            return View(pagedQuestions);
        }
    }
}