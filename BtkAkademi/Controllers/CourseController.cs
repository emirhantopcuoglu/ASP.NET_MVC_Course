using BtkAkademi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BtkAkademi.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            var model = Repository.Applications;
            return View(model);
        }
        public IActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // Güvenlik amaçlı doğrulama
        public IActionResult Apply([FromForm] Candidate model)
        {
            if (Repository.Applications.Any(a => a.Email.Equals(model.Email)))
            {
                ModelState.AddModelError("", "There is already an application for you."); // Başvuru kontrolü
            }
            if (ModelState.IsValid)     // Model validation
            {
                Repository.Add(model);
                return View("Feedback", model);
            }
            return View();
        }
    }
}