using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc.Repositories;

namespace mvc.Controllers
{
    // [Route("[controller]")]
    public class DesignationController : Controller
    {
        private readonly ILogger<DesignationController> _logger;
        private readonly IDesignationRepository _designationRepository;

        public DesignationController(ILogger<DesignationController> logger, IDesignationRepository designationRepository)
        {
            _logger = logger;
            _designationRepository = designationRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetDesignations()
        {
            return Json(_designationRepository.GetDesignations());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}