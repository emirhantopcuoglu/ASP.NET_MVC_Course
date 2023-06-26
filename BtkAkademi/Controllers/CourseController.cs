using BtkAkademi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BtkAkademi.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index() 
        { 
            return View();
        }
        public IActionResult Apply() 
        { 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // Güvenlik amaçlı doğrulama
        public IActionResult Apply([FromForm] Candidate model) 
        { 
            return View();
        }
    }
}