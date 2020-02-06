
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Domain.Models;
namespace PizzaBoxWebApp.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult CreateStore()
        {
            return View();
        }
        public IActionResult ViewStores()
        {
            return View();
        }
        public IActionResult SelectStore()
        {
            
            return View();
        }

        [HttpGet]
        public IActionResult ViewStoreOrders(StoreInfo s)
        {
            ViewData["StoreId"] = s.StoreId;
            return View();
        }
        [HttpGet]
        public IActionResult ModifyStore(StoreInfo s)
        {
            ViewData["StoreId"] = s.StoreId;
            return View();
        }
        
      
    }
}