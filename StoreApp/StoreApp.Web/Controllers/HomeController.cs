using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;

namespace Name
{
    public class HomeController : Controller
    {
        private IStoreRepository _storeRepository;
        public HomeController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}