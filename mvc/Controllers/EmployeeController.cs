using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc.Models;
using mvc.Repositories;

namespace mvc.Controllers
{
    // [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            var employee = _employeeRepository.GetEmployees();
            return View(employee);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            // if (ModelState.IsValid)
            // {
            _employeeRepository.AddEmployee(employee);
            return RedirectToAction("Index");
            // }
            // else
            // {
            // return View();
            // }
        }

        public IActionResult Delete(int id)
        {
            var employee = _employeeRepository.GetEmployee(id);
            _employeeRepository.DeleteEmployee(employee);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeRepository.GetEmployee(id);
            return View(employee);
        }


        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);
            return RedirectToAction("Index");
        }

        // public IActionResult Payroll

        public IActionResult Payroll(int id)
        {
            _employeeRepository.UpdateEmployeePayroll(id);
            Employee updatedEmployee = _employeeRepository.GetEmployee(id);
            return View(updatedEmployee);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}