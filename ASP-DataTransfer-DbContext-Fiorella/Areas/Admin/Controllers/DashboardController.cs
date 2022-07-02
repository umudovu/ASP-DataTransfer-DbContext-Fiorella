using Microsoft.AspNetCore.Mvc;

namespace ASP_DataTransfer_DbContext_Fiorella.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
