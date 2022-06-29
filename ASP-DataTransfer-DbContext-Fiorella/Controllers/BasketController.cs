using ASP_DataTransfer_DbContext_Fiorella.DataContext;
using ASP_DataTransfer_DbContext_Fiorella.Models;
using ASP_DataTransfer_DbContext_Fiorella.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ASP_DataTransfer_DbContext_Fiorella.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;

        public BasketController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddItem(int? id)
        {
            if (id == null) return NoContent();

            string basket = Request.Cookies["basket"];

            Product product = _context.Products.FirstOrDefault(x=>x.Id==id);
            if (product == null) return NoContent();



            List<BasketVM> products;

            if (basket == null)
            {
                products = new List<BasketVM>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }

             

            BasketVM isexsist = products.Find(p=>p.Id==id);

            if (isexsist == null)
            {
                BasketVM basketVM = new BasketVM
                {
                    Id = product.Id,
                    BasketCount = 1,
                    Price= product.Price
                };
                products.Add(basketVM);
            }
            else
            {
                isexsist.BasketCount++;
            }
            

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));

          

            return RedirectToAction("Index","Home");
        }

        public IActionResult ShowItem()
        {
            string basket = Request.Cookies["basket"];
            double subtotal = 0;

            List<BasketVM> products;

            if (basket!=null)
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

                foreach (var item in products)
                {
                    Product product = _context.Products.FirstOrDefault(p => p.Id == item.Id);
                    item.Price = product.Price;
                    item.Name = product.Name;
                    item.ImgUrl = product.ImgUrl;
                    
                }

            }
            else
            {
                products = new List<BasketVM>();
            }
            ViewBag.Subtotal=subtotal;

            return View(products);
        }

        public IActionResult ItemPlus(int? id)
        {
            string basket = Request.Cookies["basket"];
            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            BasketVM product = products.FirstOrDefault(p => p.Id == id);
                
            if(product==null) return NotFound();

            product.BasketCount++;

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));

            return RedirectToAction("ShowItem", "Basket");
        }

        public IActionResult ItemMinus(int? id)
        {
            string basket = Request.Cookies["basket"];
            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            BasketVM product = products.FirstOrDefault(p => p.Id == id);

            if (product == null) return NotFound();

            if(product.BasketCount>1)
            {
                product.BasketCount--;
                Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            }
            else
            {
                List<BasketVM> productsNew = products.FindAll(p=>p.Id!=id);

                Response.Cookies.Append("basket", JsonConvert.SerializeObject(productsNew));
            }

            

            return RedirectToAction("ShowItem", "Basket");
        }

        public IActionResult ItemRemove(int? id)
        {
            string basket = Request.Cookies["basket"];

            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            if (products == null) return NotFound();

            List<BasketVM> productsNew = products.FindAll(p => p.Id != id);

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(productsNew));

            return RedirectToAction("ShowItem", "Basket");
        }
    }
}
