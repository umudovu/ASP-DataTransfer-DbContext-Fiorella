using ASP_DataTransfer_DbContext_Fiorella.DataContext;
using ASP_DataTransfer_DbContext_Fiorella.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DataTransfer_DbContext_Fiorella.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : BaseController
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [Area("Admin")]
        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.OrderBy(x=>x.Id).ToList();

            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error404","Error");
            }
            bool IsExsist = _context.Categories.Any(x=>x.Name.ToLower()==category.Name.ToLower());

            if (IsExsist)
            {
                ModelState.AddModelError("Name", "Bu adda model var");
                return RedirectToAction("index");
            }
           await _context.Categories.AddAsync(category);
           await _context.SaveChangesAsync();


            return RedirectToAction("index");
        }

        public async Task<IActionResult> Remove(int? id)
        {

            if (id == null) return NotFound();

            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null) return NotFound();

            _context.Categories.Remove(category);
           await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }


        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();

            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Category dbCategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

            if(dbCategory == null) return NotFound();

            Category dbCategoryName = _context
                    .Categories.FirstOrDefault(x => x.Name.ToLower() == category.Name.ToLower());

            if (dbCategoryName != null)
            {
                if (dbCategory.Name.ToLower() != dbCategoryName.Name.ToLower())
                {
                    ModelState.AddModelError("Name", "Model Name is exsist");
                    return View("Update");
                }
            } 

            dbCategory.Name=category.Name;
            dbCategory.Description = category.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

    }
}
