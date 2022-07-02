using ASP_DataTransfer_DbContext_Fiorella.DataContext;
using ASP_DataTransfer_DbContext_Fiorella.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ASP_DataTransfer_DbContext_Fiorella.Areas.Admin.Controllers
{
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
            

            return Content($"{category.Name} {category.Description}");
        }

    }
}
