using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.Repository;
using Repositories.Models;

namespace mvc.Controllers
{
    // [Route("[controller]")]
    public class ExecutiveController : Controller
    {
        private readonly ILogger<ExecutiveController> _logger;
        private readonly IExecutiveRepository _executiveRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExecutiveController(ILogger<ExecutiveController> logger, IExecutiveRepository executiveRepository, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _executiveRepository = executiveRepository;
            _httpContextAccessor = httpContextAccessor;

        }

        public IActionResult Index(Executive executive)
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("usertype");
            ViewBag.session = username;

            var tokens = _executiveRepository.GetAllForUser(username);
            return View(tokens);
        }




        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Executive login)
        {
            var loggedInUser = _executiveRepository.Login(login);
            if (loggedInUser != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("userid", loggedInUser.t_userid);
                _httpContextAccessor.HttpContext.Session.SetString("upassword", loggedInUser.t_userpassword);

                _httpContextAccessor.HttpContext.Session.SetString("usertype", loggedInUser.t_usertype);
                return RedirectToAction("Index", "Executive"); // Redirect to the index page of HomeController
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid username or password."; // Provide error message
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public IActionResult TaskUpdate(string id)
        {
            var data = _executiveRepository.GetDataFromId(id);
            Console.WriteLine("Data: " + data);
            return View(data);
        }

        [HttpPost]
        public IActionResult TaskUpdate(Customer customer)
        {
            _executiveRepository.UpdateTask(customer);
            return RedirectToAction("Index"); 
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}