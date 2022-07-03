using Microsoft.AspNetCore.Mvc;

namespace ASP_DataTransfer_DbContext_Fiorella.Controllers
{
    [Area("Admin")]
    public class ErrorController : Controller
    {

        public IActionResult Error403()
        {
            return View();
        }

        public IActionResult Error404()
        {
            return View();
        }
        public IActionResult Error405()
        {
            return View();
        }
        public IActionResult Error500()
        {
            return View();
        }
    }
}
