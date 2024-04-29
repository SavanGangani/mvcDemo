using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.Models;
using Repositories.Repository;

namespace mvc.Controllers
{
    // [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger,IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
           List<Customer> customers = _userRepository.GetAllTask();
                return View(customers);
        }

          [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

         [HttpPost]
        public IActionResult Add(Customer customer)
        {
            int tokenid1 = _userRepository.Insert(customer);
            if (tokenid1 > 0)
            {
                TempData["tokenmessage"] = "your token number is " + tokenid1.ToString();
                return RedirectToAction("Index", "User");
            }

            else
            {
                TempData["tokenerror"] = "error while generating token";
                return View();
            }

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}