using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SpecialTagsController : Controller
    {
        private ApplicationDbContext _db;

        public SpecialTagsController(ApplicationDbContext db)
        {
            _db=db;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            //var data = _db.SpecialTags.ToList();
            return View(_db.SpecialTags.ToList());
        }

        //Get Create Action Method
        public IActionResult Create()
        {
            return View();
        }
        //POST Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTags specialTags)
        {
            if(ModelState.IsValid)
            {
                _db.SpecialTags.Add(specialTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);
        }

        // GET Edit Action Method
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if(specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);   
        }
        // POST Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Edit (SpecialTags specialTags)
        {
            if(ModelState.IsValid)
            {
                _db.SpecialTags.Update(specialTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);
        }

        // GET Details Action Method
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if(specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        // POST Details Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details (SpecialTags specialTags)
        {
            return RedirectToAction(nameof(Index));
        }


        // GET Delete Action Method
        public IActionResult Delete (int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
           var specialTag = _db.SpecialTags.Find(id);
            if(specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }
        // POST Delete Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, SpecialTags specialTags)
        {
            if(id == null)
            {
                return NotFound();
            }
            if(id != specialTags.Id)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if(specialTag == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.SpecialTags.Remove(specialTag);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTag);
        }
    }
}
