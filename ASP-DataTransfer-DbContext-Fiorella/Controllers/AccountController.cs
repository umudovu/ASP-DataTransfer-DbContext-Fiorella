using ASP_DataTransfer_DbContext_Fiorella.Models;
using ASP_DataTransfer_DbContext_Fiorella.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ASP_DataTransfer_DbContext_Fiorella.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager; 
        }

       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if(!ModelState.IsValid) return View();

            AppUser appUser = new AppUser
            {
                Fullname=registerVM.FullName,
                Email=registerVM.Email,
                UserName=registerVM.UserName,
                
            };

            IdentityResult result= await _userManager.CreateAsync(appUser, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(registerVM);
            }

            return RedirectToAction("Index","Home");
        }


        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser appUser= await _userManager.FindByEmailAsync(loginVM.Email);
            if(appUser == null)
            {
                ModelState.AddModelError("", "Email veya password invalid");
                return View(loginVM);
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, true, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Email veya password invalid");
                return View(loginVM);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email veya password invalid");
                return View(loginVM);
            }



            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
