using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using WmIdentity.Models;
using WmIdentity.Services;
using WmIdentity.ViewModels;

namespace WmIdentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<MyUser> _userManagerService;
        private readonly IUserClaimsPrincipalFactory<MyUser> _claimsPrincipalFactory;
        private readonly SignInManager<MyUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly WmIdentityDbContext _context;


        public AccountController(UserManager<MyUser> userManger, IUserClaimsPrincipalFactory<MyUser> userClaimsPrincipalFactory, SignInManager<MyUser> signInManager, IEmailSender emailSender, WmIdentityDbContext _context)
        {
            this._userManagerService = userManger;
            this._signInManager = signInManager;
            this._claimsPrincipalFactory = userClaimsPrincipalFactory;
            this._emailSender = emailSender;
            this._context = _context;
        }

        [HttpGet]
        //[Authorize]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (ModelState.IsValid)
            {
                var user =  await _userManagerService.FindByNameAsync(vm.UserName);
                
                if (user == null)
                {
                    user = new MyUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = vm.UserName,
                        Email = vm.UserName
                    };

                   

                    var result = await _userManagerService.CreateAsync(user, vm.Password);

                    if (result.Succeeded)
                    {

                        return View("Success");

                        #region asdas
                        //         var token = await _userManagerService.GenerateEmailConfirmationTokenAsync(user);

                        //         var confirmationEmail = Url.Action("ConfirmEmailAddress", "Account",
                        //            new
                        //            {
                        //                token,
                        //                email = user.Email
                        //            },
                        //            Request.Scheme);


                        //         await _emailSender.SendEmailAsync(vm.UserName, "Confirm your email",
                        //$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(confirmationEmail)}'>clicking here</a>.");

                        //         //System.IO.File.WriteAllText("confirmationLink.txt", confirmationEmail);
                        #endregion
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View();
                    }
                }
                else
                {
                    return View("EmailExistenteError"); 
                }

            }
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> ConfirmEmailAddress(string token, string email)
        //{
        //    var user = await _userManagerService.FindByEmailAsync(email);

        //    if (user != null)
        //    {
        //        var result = await _userManagerService.ConfirmEmailAsync(user, token);

        //        if (result.Succeeded)
        //        {
        //            return View("Success");
        //        }
        //    }

        //    return View("Error");
        //}


        [HttpGet]
        public IActionResult Login()
        {
            if (_context.Users.Any())
            {
                ViewBag.ZeroUSers = true;
            }
            else
            {
                ViewBag.ZeroUSers = false;
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManagerService.FindByNameAsync(vm.UserName);

                if (user != null && !await _userManagerService.IsLockedOutAsync(user))
                {
                    if (await _userManagerService.CheckPasswordAsync(user, vm.Password))
                    {
                        //if (!await _userManagerService.IsEmailConfirmedAsync(user))
                        //{
                        //    ModelState.AddModelError("", "Email não confirmado!!");
                        //    return View();
                        //}

                        //await _userManagerService.ResetAccessFailedCountAsync(user);

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return RedirectToAction("Index", "Home");
                    }

                    await _userManagerService.AccessFailedAsync(user);

                    if (await _userManagerService.IsLockedOutAsync(user))
                    {
                        // email user, notifying them of lockout
                    }
                }
                ModelState.AddModelError("", "Nome ou senha incorretos!");
                

            }
            return View();

        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManagerService.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var token = await _userManagerService.GeneratePasswordResetTokenAsync(user);
                    var resetUrl = Url.Action("ResetPassword", "Account",
                        new { token = token, email = user.Email }, Request.Scheme);

                    System.IO.File.WriteAllText("resetLink.txt", resetUrl);


                }
                else
                {
                    // email user and inform them that they do not have an account
                }

                return View("Success");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            return View(new ResetPasswordVM { Token = token, Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManagerService.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _userManagerService.ResetPasswordAsync(user, model.Token, model.Password);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View();
                    }

                    if (await _userManagerService.IsLockedOutAsync(user))
                    {
                        await _userManagerService.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                    }
                    return View("Success");
                }
                ModelState.AddModelError("", "Invalid Request");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}