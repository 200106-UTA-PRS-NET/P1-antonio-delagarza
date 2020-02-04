using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaBoxWebApp.Models;
using PizzaBox.Storing.Repositories;
using PizzaBox.Domain.Models;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace PizzaBoxWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Register as a new user
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(UsersViewModel u)
        {
            RepositoryUsers repositoryUsers = new RepositoryUsers();
            Users user = new Users()
            {
                Email = u.Email,
                Password = u.Password,
                FirstName = u.FirstName,
                LastName = u.LastName
            };
            try
            {
                repositoryUsers.Add(user);
            }
            catch (DbUpdateException)
            {
                return View("RegistrationError");
            }

            return RedirectToAction(nameof(Index));
        }

        //Sign in to an existing account
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(UsersViewModel u)
        {

            RepositoryUsers repositoryUsers = new RepositoryUsers();
            Users user = new Users()
            {
                Email = u.Email,
                Password = u.Password,
                FirstName = u.FirstName,
                LastName = u.LastName
            };
           
            repositoryUsers.SignIn(user.Email, user.Password, ref user);
            if (user != null)
            {

                TempData["UserEmail"] = user.Email;
                TempData["UserPassword"] = user.Password;
                
                return RedirectToAction(nameof(Index), "SignedIn");
                
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

       
    }
}
