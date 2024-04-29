using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories.Models;
using Repositories.Repository;

namespace MVC.Controllers
{
    // [Route("[controller]")]
    public class AdminController : Controller
    {
       private readonly IAdminRepository _adminrepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserController> _logger;

        public AdminController(ILogger<UserController> logger, IAdminRepository adminrepo, IHttpContextAccessor httpContextAccessor)
        {
            _adminrepo = adminrepo;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        [Produces("application/json")]
        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("username");
            if (username == null)
            {
                // ViewBag.IsAuthenticated = false;
                return RedirectToAction("Login", "User");
            }
            // ViewBag.IsAuthenticated = true;
            return View();
        }

        public IActionResult GetAllAlbumType()
        {
            var type = _adminrepo.GetAllAlbumType();
            return Json(type);
        }

        public IActionResult GetAllAlbum()
        {
            var session = HttpContext.Session.GetInt32("userid");
            ViewBag.UserId = session;
            var album = _adminrepo.GetAlbums();
            return Json(album);
        }

        [HttpPost]
        public IActionResult AddAlbum(Album album)
        {
            if (_adminrepo.AddAlbum(album))
            {
                return Json(new { success = true, message = "Successfully Added" });
            }
            else
            {
                return Json(new { success = false, message = "Not Inserted!!" });
            }
        }

        // [HttpGet]
        // public IActionResult EditAlbum(int id){
        //     var album = _adminrepo.GetDetails(id);
        //     if(album == null){
        //         return NotFound();
        //     }
        // }

        [HttpPost]
        public IActionResult EditAlbum(Album album)
        {
            if (_adminrepo.EditAlbum(album))
            {
                return Json(new { success = true, message = "Successfully Updated" });
            }
            else
            {
                return Json(new { success = false, message = "Not Updated!!" });
            }
        }

        [HttpPost]
        public IActionResult DeleteAlbum(int c_albumid)
        {
            if (_adminrepo.DeleteAlbum(c_albumid))
            {
                return Json(new { success = true, message = "Successfully Deleted" });
            }
            else
            {
                return Json(new { success = true, message = "Successfully not Deleted" });

            }
        }

        [HttpPost]
        public IActionResult SaveImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }

            // var folderPath = "/Users/yashveekotadiya/Desktop/casepoint/KendoExam/MVC/wwwroot/images";
                var uploadsFolder = Path.Combine("F:\\PPSU\\sem-08\\casepoint\\CoreMvc\\WeeklyExam\\KendoExam1\\KendoExam\\MVC\\wwwroot", "images");
                // string uniqueFilename = Guid.NewGuid().ToString() + "_" file.FileName;

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, file.FileName);
                string filepath = Path.Combine(uploadsFolder, filePath);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var imageUrl = Path.Combine("/images", file.FileName); // Assuming the URL to access the image is /images/{filename}
            return Ok(new { imageUrl });
        }







    }
}