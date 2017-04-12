using KWFCI.Models;
using KWFCI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KWFCI.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<StaffUser> userManager;
        private SignInManager<StaffUser> signInManager;

        public AuthController(UserManager<StaffUser> usrMgr, SignInManager<StaffUser> sim)
        {
            userManager = usrMgr;
            signInManager = sim;
        }

        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginVM());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (ModelState.IsValid)
            {
                StaffUser user = await userManager.FindByNameAsync(vm.UserName);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                            await signInManager.PasswordSignInAsync(
                                user, vm.Password, false, false);
                    if (result.Succeeded)
                    {
                        //Redirects to the home index if login succeeds
                        return Redirect("/");
                    }
                }
                ModelState.AddModelError("", "Invalid name or password.");
            }
            return View(vm);
        }
    }
}
