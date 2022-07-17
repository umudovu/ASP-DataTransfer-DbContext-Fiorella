using ASP_DataTransfer_DbContext_Fiorella.DataContext;
using ASP_DataTransfer_DbContext_Fiorella.Extentions;
using ASP_DataTransfer_DbContext_Fiorella.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DataTransfer_DbContext_Fiorella.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : BaseController
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products =await _context.Products.Include(x => x.Category).ToListAsync();

            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories =new SelectList(_context.Categories.ToList(),"Id","Name") ;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Create(Product product)
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");

            if (ModelState["Photo"] == null)
            {
                ModelState.AddModelError("Photo", "Photo is null");
                return View();
            }
            if (!product.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Only image");
                return View();
            }
            if (product.Photo.ImageSize(800))
            {
                ModelState.AddModelError("Photo", "Olcu oversize");
                return View();
            }



            Product newProduct = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ImgUrl = product.Photo.SaveImage(_env, "img")
            };

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();


            return RedirectToAction("index");
        }


        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null) return NotFound();
            var dbproduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (dbproduct == null) return NotFound();


            string path = Path.Combine(_env.WebRootPath, "img", dbproduct.ImgUrl);

            Helpers.Helper.DeleteImage(path);

             _context.Products.Remove(dbproduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");

            if (id == null) return NotFound();
            var dbproduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (dbproduct == null) return NotFound();

            return View(dbproduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Product product)
        {
            Product dbProduct = await _context.Products.FindAsync(product.Id);

            if (ModelState["Photo"] != null)
            {
                if (!product.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Only image");
                    return View();
                }
                if (product.Photo.ImageSize(800))
                {
                    ModelState.AddModelError("Photo", "Olcu oversize");
                    return View();
                }

                string path = Path.Combine(_env.WebRootPath, "img", dbProduct.ImgUrl);
                Helpers.Helper.DeleteImage(path);
                dbProduct.ImgUrl = product.Photo.SaveImage(_env, "img");
            }

            dbProduct.Name = product.Name;
            dbProduct.Price = product.Price;
            dbProduct.Count = product.Count;
            dbProduct.CategoryId = product.CategoryId;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
