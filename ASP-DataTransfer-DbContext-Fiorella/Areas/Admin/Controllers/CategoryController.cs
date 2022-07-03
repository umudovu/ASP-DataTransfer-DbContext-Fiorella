using ASP_DataTransfer_DbContext_Fiorella.DataContext;
using ASP_DataTransfer_DbContext_Fiorella.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ASP_DataTransfer_DbContext_Fiorella.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
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
        public IActionResult Create(Category category)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("index","Category");
        }

        public IActionResult Remove(int? id)
        {

            if (id == null) return NotFound();

            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction("index", "Category");
        }

    }
}
