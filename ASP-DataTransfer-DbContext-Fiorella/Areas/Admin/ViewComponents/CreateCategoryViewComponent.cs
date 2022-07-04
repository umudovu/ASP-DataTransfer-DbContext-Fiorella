using ASP_DataTransfer_DbContext_Fiorella.DataContext;
using ASP_DataTransfer_DbContext_Fiorella.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DataTransfer_DbContext_Fiorella.Areas.Admin.ViewComponents
{
    public class CreateCategoryViewComponent: ViewComponent
    {
        private readonly AppDbContext _context;

        public CreateCategoryViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Category category = new Category();

            return View(await Task.FromResult(category));
        }
    }
}
