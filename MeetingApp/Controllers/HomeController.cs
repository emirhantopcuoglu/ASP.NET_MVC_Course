using Microsoft.AspNetCore.Mvc;
using MeetingApp.Models;

namespace MeetingApp.Controllers
{
    public class HomeController : Controller
    {
        // localhost
        // localhost/home
        // localhost/home/index
        public IActionResult Index()
        {
            int saat = DateTime.Now.Hour;
            // ViewBag.Selamlama = saat > 12 ? "İyi Günler" : "Günaydın";
            ViewData["Selamlama"] = saat > 12 ? "İyi Günler" : "Günaydın";
            int UserCount = Repository.Users.Where(u => u.WillAttend == true).Count();

            var meetingInfo = new MeetingInfo()
            {
                Id = 1,
                Location = "İstanbul, Abc Kongre Merkezi",
                Date = new DateTime(2024, 1, 1, 20, 0, 0),
                NumberOfPeople = UserCount,
            };
            return View(meetingInfo);
        }
    }
}