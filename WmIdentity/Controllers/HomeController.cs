using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WmIdentity.Models;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using WmIdentity.ViewModels;

namespace WmIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<MyUser> _signInManager;


        public HomeController(IStringLocalizer<HomeController> localizer, IEmailSender emailSender, SignInManager<MyUser> signInManager)
        {
            this._localizer = localizer;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[Authorize]
        public IActionResult About()
        {
            //ViewData["Message"] = _localizer["Your application description page."];
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailAction(CustomerMailFormVM vm)
        {
            #region
            //var message = new MimeMessage();

            ////from
            //message.From.Add(new MailboxAddress("WmComex", "henrique_vc@hotmail.com"));

            ////to
            //message.To.Add(new MailboxAddress("dot net training academy", "henrique_vc@hotmail.com"));

            ////subjet
            //message.Subject = "my subject";

            ////body
            //message.Body = new TextPart("plain")
            //{
            //    Text = "my body message"
            //};

            ////configure
            //using (var client = new SmtpClient())
            //{
            //    client.Connect("smtp.hotmail.com", 587, true);
            //}

            //message.Priority = MessagePriority.Urgent;
            #endregion

            string subject = "Enviada por: " + vm.nome + ". Assunto: " + vm.subject; 

            await _emailSender.SendEmailAsync(vm.email, subject, vm.message);


            return View("EmailSuccess");
        }
    }
}
