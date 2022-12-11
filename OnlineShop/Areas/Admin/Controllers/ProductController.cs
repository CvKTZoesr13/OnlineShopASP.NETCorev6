using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;
namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IWebHostEnvironment _he;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment he)
        {
            _db=db;
            _he=he;
        }

        public IActionResult Index()
        {
            return View(_db.Products.Include(c=>c.ProductTypes).Include(f=>f.SpecialTag).ToList());
        }

        // Get Create method
        public IActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "SpecialTag");
            return View();
        }
        // Post Create method
        [HttpPost]
        public async Task<IActionResult> Create(Products product, IFormFile image)
        {
            if(!ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }

                if (image==null)
                {
                    product.Image = "Images/sixmorevodka-studio-zed-l02.jpg";
                }
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        //GET Edit Action Method
        public IActionResult Edit(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "SpecialTag");
            if(id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductTypes).Include(c=>c.SpecialTag)
                .FirstOrDefault(c=>c.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST Edit Action Method
        [HttpPost]
        public async Task<IActionResult> Edit(Products product, IFormFile image)
        {
            if (!ModelState.IsValid) // tim hieu them ve ModelState.IsValid, tai sao data lai khong hop le! 
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }

                if (image==null)
                {
                    product.Image = "Images/sixmorevodka-studio-zed-l02.jpg";
                }
                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        // GET Details Action Method, (chua tao view)
        public  IActionResult Details(int? id)
        {
            return View();
        }
    }
}
