using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;
using System.Diagnostics;

namespace OnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).ToList());
        }

        // GET Product Details Action Method
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
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
    }
}