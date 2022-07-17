using ASP_DataTransfer_DbContext_Fiorella.DataContext;
using ASP_DataTransfer_DbContext_Fiorella.Helpers;
using ASP_DataTransfer_DbContext_Fiorella.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DataTransfer_DbContext_Fiorella.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly AppDbContext _context;

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index(int page=1,int pageSize=10)
        {
            var users =await _userManager.Users.ToListAsync();

            var list = await PagedList<AppUser>.CreateAsync(users, page, pageSize);

            return View(list);
        }


       
    }
}
