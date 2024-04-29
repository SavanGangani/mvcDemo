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
    public class ShoppingController : Controller
    {
         private readonly IShoppingRepository _shoppingrepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserController> _logger;

        public ShoppingController(ILogger<UserController> logger, IShoppingRepository shoppingrepo, IHttpContextAccessor httpContextAccessor)
        {
            _shoppingrepo = shoppingrepo;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        [Produces("application/json")]
        public IActionResult Index()
        {
            // string username = HttpContext.Session.GetString("username");
            // if (username == null)
            // {
            //     // ViewBag.IsAuthenticated = false;
            //     return RedirectToAction("Login", "User");
            // }
            // // ViewBag.IsAuthenticated = true;
            return View();
        }

        public IActionResult GetAllAlbum()
        {
            var album = _shoppingrepo.GetAlbums();
            return Json(album);
        }

        [HttpGet]
        public IActionResult AddToCart(int c_albumid, int c_quantity, double c_price)
        {
            string username = HttpContext.Session.GetString("username");
            int userid = (int)HttpContext.Session.GetInt32("userid");
            double totalPrice = c_quantity * c_price;
            Shopping shop = new Shopping
            {
                c_albumid = c_albumid,
                c_quantity = c_quantity,
                c_price = c_price,
                c_total_price = totalPrice,
                c_userid = userid,
                c_username = username
            };

            if (_shoppingrepo.AddToCart(shop))
            {
                return Json(new { success = true, message = "Successfully Added to Cart" });
            }
            else
            {
                return Json(new { success = false, message = "Not Added!!" });
            }
        }

        public IActionResult Cart()
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

        public IActionResult GetAllCart()
        {
            int userid = (int)HttpContext.Session.GetInt32("userid");
            var allCart = _shoppingrepo.GetAllCart(userid);
            return Json(allCart);
        }

        [HttpPost]
        public IActionResult RemoveCart(int c_shoppingid)
        {
            if (_shoppingrepo.RemoveCart(c_shoppingid))
            {
                return Json(new { success = true, message = "Remove from Cart" });
            }
            else
            {
                return Json(new { success = false, message = "Not Removed!!" });

            }
        }


        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public IActionResult Checkout()
        {
            int userid = (int)HttpContext.Session.GetInt32("userid");
            if (userid == null)
            {
                // ViewBag.IsAuthenticated = false;
                return RedirectToAction("Login", "User");
            }
            // ViewBag.IsAuthenticated = true;
            ViewBag.userid = userid;
            ViewBag.orderId = RandomString(5);
            return View();
        }

        public JsonResult Placeorder(CheckOut order)
        {
            order.c_userid = (int)HttpContext.Session.GetInt32("userid");
            Int64 result = 0;
            order.c_orderdate = DateTime.Now;
            Int64 orderId = _shoppingrepo.Placeorder(order);
            if (orderId != 0)
            {
                bool removedFromCart = _shoppingrepo.RemoveCart(order.c_shoppingid);

                result = orderId;
            }
            return Json(result);
        }

       public ActionResult OrderConfirmed(Int64 orderId)
{
    int userid = (int)HttpContext.Session.GetInt32("userid");
    if (userid == null)
    {
        ViewBag.IsAuthenticated = false;
        return RedirectToAction("Login", "User");
    }
    // ViewBag.IsAuthenticated = true;
    ViewBag.OrderId = orderId; // Pass orderId to the view using ViewBag
    return View();
}

    }
}