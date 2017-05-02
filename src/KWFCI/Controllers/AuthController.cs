using KWFCI.Models;
using KWFCI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KWFCI.Repositories;
using MailKit.Net.Smtp;
using MimeKit;

namespace KWFCI.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<StaffUser> userManager;
        private SignInManager<StaffUser> signInManager;
        private IStaffProfileRepository staffProfRepo;

        public AuthController(UserManager<StaffUser> usrMgr, SignInManager<StaffUser> sim, IStaffProfileRepository repo)
        {
            userManager = usrMgr;
            signInManager = sim;
            staffProfRepo = repo;
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
                        Helper.StaffUserLoggedIn = user;
                        Helper.StaffProfileLoggedIn = Helper.DetermineProfile(staffProfRepo);
                        //Redirects to the home index if login succeeds
                        return Redirect("/");
                    }
                }
                ModelState.AddModelError("", "Invalid name or password.");
            }
            return View(vm);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return View("Login");
        }

        [Route("Reset")]
        public IActionResult ForgotPassword()
        {
            return View("Reset");
        }

        [Route("Reset/Send")]
        public IActionResult SendPasswordResetLink(string username)
        {
            StaffUser user = userManager.
                 FindByNameAsync(username).Result;

            if (user == null)
            {
                return View("Reset");
            }

            var token = userManager.
                  GeneratePasswordResetTokenAsync(user).Result;

            var resetLink = Url.Action("ResetPassword",
                            "Auth", new { token = token },
                             protocol: HttpContext.Request.Scheme);

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("KWFCI", "do-not-reply@kw.com"));
            email.Subject = "Password Reset";
            email.Body = new TextPart("plain")
            {
                Text = "Click the link to reset your password " + resetLink
            };
            email.To.Add(new MailboxAddress(user.UserName));

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 587, false);

                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("kwfamilycheckin", "Fancy123!");

                client.Send(email);
                client.Disconnect(true);
            }

            //TODO: Pop up Alert box saying the reset link has been sent to their email
            return View("Login");
        }

    

    }
}

