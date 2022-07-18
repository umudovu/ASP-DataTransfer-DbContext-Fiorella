using ASP_DataTransfer_DbContext_Fiorella.DataContext;
using ASP_DataTransfer_DbContext_Fiorella.Models;
using ASP_DataTransfer_DbContext_Fiorella.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DataTransfer_DbContext_Fiorella.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BasketController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

            double subtotal = 0;
            int basketCount = 0;

            if (products.Count > 0)
            {
                foreach (BasketVM pr in products)
                {
                    subtotal += pr.Price * pr.BasketCount;
                    basketCount += pr.BasketCount;
                }
            }
            var obj = new
            {
                SubTotal = subtotal,
                BasketCount = basketCount
            };

            return Ok(obj);
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

        [HttpPost]
        public IActionResult ItemPlus(int? id)
        {
            string basket = Request.Cookies["basket"];
            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            BasketVM product = products.FirstOrDefault(p => p.Id == id);
                
            if(product==null) return NotFound();

            product.BasketCount++;

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));

            double subtotal = 0;
            int basketCount = 0;

            foreach (BasketVM pr in products)
            {
                subtotal += pr.Price * product.BasketCount;
                basketCount += pr.BasketCount;
            }

            var obj = new
            {
                count = product.BasketCount,
                SubTotal = subtotal,
                BasketCount = basketCount
            };
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult ItemMinus(int? id)
        {
            string basket = Request.Cookies["basket"];

            double subtotal = 0;
            int basketCount = 0;

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

                products.Remove(product);

                List<BasketVM> productsNew = products.FindAll(p => p.Id != id);

                Response.Cookies.Append("basket", JsonConvert.SerializeObject(productsNew));

                foreach (BasketVM pr in productsNew)
                {
                    subtotal += pr.Price * product.BasketCount;
                    basketCount += pr.BasketCount;
                }

                var obje = new
                {
                    count = 0,
                    SubTotal = subtotal,
                    BasketCount = basketCount
                };

                return Ok(obje);
            }


            
                foreach (BasketVM pr in products)
                {
                    subtotal += pr.Price * product.BasketCount;
                    basketCount += pr.BasketCount;
                }
            


            var obj = new
            {
                count = product.BasketCount,
                SubTotal = subtotal,
                BasketCount = basketCount
            };

            return Ok(obj);
        }


        [HttpPost]
        public IActionResult ItemRemove(int? id)
        {
            string basket = Request.Cookies["basket"];

            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            if (products == null) return NotFound();

            List<BasketVM> productsNew = products.FindAll(p => p.Id != id);

            BasketVM product = products.FirstOrDefault(p => p.Id == id);

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(productsNew));

            double subtotal = 0;
            int basketCount = 0;

            if (products.Count > 0)
            {
                foreach (BasketVM pr in products)
                {
                    subtotal += pr.Price * product.BasketCount;
                    basketCount += pr.BasketCount;
                }
            }

            var obj = new
            {
                SubTotal = subtotal,
                BasketCount = basketCount
            };
            return Ok(obj);
        }

        [HttpGet]
        public IActionResult GetBasket(int? id)
        {
            string basket = Request.Cookies["basket"];
            double subtotal = 0;
            int basketCount = 0;

            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            BasketVM product = products.FirstOrDefault(p => p.Id == id);


            if (products.Count > 0)
            {
                foreach (BasketVM pr in products)
                {
                    subtotal += pr.Price * product.BasketCount;
                    basketCount += pr.BasketCount;
                }
            }

            var obj = new
            {
                count = product.BasketCount,
                SubTotal = subtotal,
                BasketCount = basketCount
            };

            return Ok(obj);
        }


        [HttpPost]
        public async Task<ActionResult> Sale()
        {

            if (!User.Identity.IsAuthenticated) return View("login", "account");

            AppUser user =await _userManager.FindByNameAsync(User.Identity.Name);

            Sale sale = new Sale();
            sale.SaleDate = DateTime.Now;
            sale.AppUserId = user.Id;

            List<BasketVM> basketProducts =
                    JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            List<SaleProduct> saleProducts = new List<SaleProduct>();

            foreach (var item in basketProducts)
            {
                Product dbProduct =await _context.Products.FindAsync(item.Id);
                SaleProduct saleProduct = new SaleProduct();
                saleProduct.ProductId = item.Id;
                saleProduct.ProductCount = item.BasketCount;
                saleProduct.Price=dbProduct.Price;
                saleProduct.SaleId = sale.Id;
                saleProducts.Add(saleProduct);
                sale.Total+=item.BasketCount*dbProduct.Price;
            }
            sale.SaleProducts=saleProducts;

            await _context.AddAsync(sale);
            await _context.SaveChangesAsync();



            return RedirectToAction("ShowItem");
        }


    }
}
