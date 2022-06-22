using ASP_DataTransfer_DbContext_Fiorella.DataContext;
using ASP_DataTransfer_DbContext_Fiorella.Models;
using ASP_DataTransfer_DbContext_Fiorella.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ASP_DataTransfer_DbContext_Fiorella.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context )
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Product> products = _context.Products.Take(4).Include(p=>p.Category).ToList();
            int productCount = _context.Products.Count();
            ViewBag.productCount = productCount;

            return View(products);
        }

       

        public IActionResult LoadMore(int skip)
        {
            List<Product> products = _context.Products.Skip(skip).Take(4).Include(p => p.Category)
                .ToList();

            return PartialView("_PartialProduct", products);
        }   

    }
}
