using Microsoft.AspNetCore.Mvc;
using DNU_QnA_MVC_App.Repositories;

namespace DNU_QnA_MVC_App.Controllers
{
    public class VotesController : Controller
    {
        private readonly IVoteRepository _voteRepo;
        private readonly IQuestionRepository _questionRepo;

        public VotesController(IVoteRepository voteRepo, IQuestionRepository questionRepo)
        {
            _voteRepo = voteRepo;
            _questionRepo = questionRepo;
        }

        [HttpPost]
        public async Task<IActionResult> UpVote(int id)
        {
            var userId = 1; // TODO: lấy từ User.Identity sau này
            var vote = new Models.Entities.Vote
            {
                QuestionId = id,
                UserId = userId,
                IsUpVote = true
            };
            await _voteRepo.AddAsync(vote);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DownVote(int id)
        {
            var userId = 1; // TODO: lấy từ User.Identity sau này
            var vote = new Models.Entities.Vote
            {
                QuestionId = id,
                UserId = userId,
                IsUpVote = false
            };
            await _voteRepo.AddAsync(vote);
            return RedirectToAction("Index", "Home");
        }
    }
}