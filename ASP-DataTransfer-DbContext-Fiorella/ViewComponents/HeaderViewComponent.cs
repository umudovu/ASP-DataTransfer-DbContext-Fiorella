using ASP_DataTransfer_DbContext_Fiorella.DataContext;
using ASP_DataTransfer_DbContext_Fiorella.Models;
using ASP_DataTransfer_DbContext_Fiorella.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DataTransfer_DbContext_Fiorella.ViewComponents
{
    public class HeaderViewComponent: ViewComponent
    {
        private readonly AppDbContext _context;

        public HeaderViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            string basket = Request.Cookies["basket"];
            double subtotal = 0;
            int basketCount = 0;
            List<BasketVM> products=new List<BasketVM>();

            if (basket!=null)
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }

            if (products.Count>0)
            {
                foreach (BasketVM product in products)
                {
                    subtotal += product.Price * product.BasketCount;
                    basketCount+=product.BasketCount;
                }
            }

            ViewBag.SubTotal = subtotal;

            ViewBag.BasketCount = basketCount;

            Bio bio = _context.Bios.FirstOrDefault();

            return View(await Task.FromResult(bio));

        }
    }
}
