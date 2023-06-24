using Microsoft.AspNetCore.Mvc;

namespace Basics.Controllers
{
    public class EmployeeController : Controller
    {
        public String Index()   // https://localhost:7090/Employee adresinde "Hello World" döndürür.
        {
            return "Hello World";
        }

        public ViewResult Index2()  // Buradaki action ile (Index2) aynı isimde bir view aranır (Index2)
        {
            return View("Index");  // Eğer farklı isimde bir view göndermek istiyorsak belirtmeliyiz. View("Index") gibi
        }

        public IActionResult Index3()   // "IActionResult" actionları ifade eden daha kapsayıcı bir yapı.
        {
            return Content("Employee"); // Employee içeriğini (content) döndürür.
        }
    }
}