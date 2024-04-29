using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.Models;
using Repositories.Repository;

namespace MVC.Controllers
{
    // [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserController(IUserRepository userRepository, IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }


        //  [Route("")]
        // public IActionResult Index()
        // {
        //     ViewBag.IsAuthenticated = false;

        //     return View();
        // }



        public IActionResult Register()
        {
            // ViewBag.IsAuthenticated = false;
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
           
                _userRepository.Register(user);
              
               
                return RedirectToAction("Login", "User");
        }



        public IActionResult Login()
        {
            ViewBag.IsAuthenticated = false;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(User user)
        {
            _httpContextAccessor.HttpContext.Session.SetString("email", user.c_email);
        _httpContextAccessor.HttpContext.Session.SetString("password", user.c_password);
        
            User loginUser = _userRepository.Login(user.c_email, user.c_password);
            Console.WriteLine("User Data : "+ loginUser);
            if (loginUser != null)
            {
            Console.WriteLine("Email ID And PAssword : "+user.c_email + user.c_password);
                var session = _httpContextAccessor.HttpContext.Session;

                if (user.c_email == "admin@gmail.com" && user.c_password == "123")
                {
                    Console.WriteLine("Admin");
                    return RedirectToAction("Index", "Admin");
                }else{
                    Console.WriteLine("User");

                    return RedirectToAction("Index", "Shopping");
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Wrong email or password. Please try again.";
                return View(user);
            }
        }



        public IActionResult Logout()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.Clear(); // Clear all session data
            ViewBag.IsAuthenticated = false;
            return RedirectToAction("Login", "User");
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}