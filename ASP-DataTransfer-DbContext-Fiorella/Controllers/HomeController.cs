using ASP_DataTransfer_DbContext_Fiorella.DataContext;
using ASP_DataTransfer_DbContext_Fiorella.Models;
using ASP_DataTransfer_DbContext_Fiorella.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ASP_DataTransfer_DbContext_Fiorella.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Sliders = _context.Sliders.ToList();
            homeVM.SliderContent = _context.SliderContents.FirstOrDefault();
            homeVM.Categories = _context.Categories.ToList();
            homeVM.Products = _context.Products.Include(p => p.Category).ToList();
            homeVM.Employees = _context.Employees.ToList();
            homeVM.Blogs = _context.Blogs.ToList();

            return View(homeVM);
        }

       


        public IActionResult SerachProduct(string search)
        {
            List<Product> products = _context.Products
                 .Include(p => p.Category)
                 .OrderBy(p => p.Id)
                 .Where(p => p.Name.ToLower()
                 .Contains(search.ToLower()))
                 .Take(5).ToList();

            return PartialView("_SearchPartial",products);
        }
    }
}
