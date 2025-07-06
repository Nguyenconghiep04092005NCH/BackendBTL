using DNU_QnA_MVC_App.Data;
using DNU_QnA_MVC_App.Models.Entities;
using DNU_QnA_MVC_App.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DNU_QnA_MVC_App.Controllers
{
    public class TagsController : Controller
    {
        private readonly DnuQnADbContext _context;

        public TagsController(DnuQnADbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tags = _context.Tags
                .Select(t => new TagViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description
                })
                .ToList();

            return View(tags);
        }

        public IActionResult Manage(int? id)
        {
            if (id == null)
                return View(new TagViewModel());

            var tag = _context.Tags.Find(id.Value);
            if (tag == null)
                return NotFound();

            var vm = new TagViewModel
            {
                Id = tag.Id,
                Name = tag.Name,
                Description = tag.Description
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Manage(TagViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Id == 0)
            {
                // Create
                var tag = new Tag
                {
                    Name = model.Name,
                    Description = model.Description
                };
                _context.Tags.Add(tag);
            }
            else
            {
                // Update
                var tag = _context.Tags.Find(model.Id);
                if (tag == null)
                    return NotFound();

                tag.Name = model.Name;
                tag.Description = (string?)model.Description;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var tag = _context.Tags.Find(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
