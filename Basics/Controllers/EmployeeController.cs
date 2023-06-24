using Microsoft.AspNetCore.Mvc;
using Basics.Models;
namespace Basics.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index1()   
        {
            string message = $"Hello World. {DateTime.Now.ToString()}";
            return View("Index1", message); // View klasöründen "Index1.cshtml" dosyası alınır, message stringi yazdırılır
        }

        public ViewResult Index2()  
        {
            var names = new String[]{
                "Ahmet",
                "Mehmet",
                "Ayşe"
            };
            return View(names); 
        }

        public IActionResult Index3()   
        {
            var list = new List<Employee>{
                new Employee(){Id=1,FirstName="Ahmet",LastName="Can",Age=20},
                new Employee(){Id=2,FirstName="Buse",LastName="Yıldız",Age=22},
                new Employee(){Id=3,FirstName="Merve",LastName="Cansu",Age=30}
            };
            return View("Index3",list); 
        }
    }
}