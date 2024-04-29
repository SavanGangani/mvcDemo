using System.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories;
using Repositories.Models;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace mvc.Controllers
{
    // [Route("[controller]")]
    public class TripController : Controller
    {
        private readonly ILogger<TripController> _logger;
        private readonly ITripRepository _tripRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TripController(ILogger<TripController> logger, ITripRepository tripRepository, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _tripRepository = tripRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var trip = _tripRepository.GetAllTrip();
            return View(trip);
        }

        // [HttpGet]
        public IActionResult UserView()
        {
            var trip = _tripRepository.GetAllTrip();
            return View(trip);
        }

        [HttpGet]
        public IActionResult UserIndex()
        {

            return View();
        }

        [HttpPost]
        public IActionResult UserIndex(Trip trip)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Trip trip)
        {
            
            _tripRepository.AddTrip(trip);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _tripRepository.DeleteTrip(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var trip = _tripRepository.GetTripById(id);
            return View(trip);
        }

        [HttpPost]
        public IActionResult Update(Trip trip)
        {
            Console.WriteLine("Update " + trip.c_tripprice);
            _tripRepository.UpdateTrip(trip);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var trip = _tripRepository.GetTripById(id);
            return View(trip);
        }


        [HttpGet]
        public IActionResult Book(int id)
        {
            var trip = _tripRepository.GetTripById(id);
            return View(trip);
        }

        [HttpPost]
        public IActionResult Book(Trip trip)
        {
            trip.userid = _httpContextAccessor.HttpContext.Session.GetInt32("userid").Value;
            Console.WriteLine("OK : "+trip.userid);
            _tripRepository.BookTrip(trip);
            Console.WriteLine(trip.c_totalcost);
            return RedirectToAction("UserView", "Trip");
        }

        [HttpGet]
        public IActionResult Mytrip()
        {
            var mytrip = _tripRepository.MyTrip(_httpContextAccessor.HttpContext.Session.GetInt32("userid").Value);
            return View(mytrip);
        }


        public IActionResult Cancel(int id)
        {
            _tripRepository.Cancel(id);
            Console.WriteLine("Trip Cancel");
            return RedirectToAction("MyTrip");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}