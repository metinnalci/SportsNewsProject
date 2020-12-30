using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SportsNewsProject.Models.ORM.Context;
using SportsNewsProject.Models.ORM.Entities;
using SportsNewsProject.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsNewsProject.Controllers
{
    public class RegisterController : Controller
    {
        private readonly SportsNewsContext _newscontext;

        public RegisterController(SportsNewsContext newscontext)
        {
            _newscontext = newscontext;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(MainRegisterVM model)
        {
            if (ModelState.IsValid)
            {
                User existingUser = _newscontext.Users.FirstOrDefault(x => x.EMail == model.EMail);

                if(existingUser == null)
                {
                    string confirmcode = Guid.NewGuid().ToString();
                    User user = new User();
                    user.Name = model.Name;
                    user.SurName = model.Surname;
                    user.NickName = model.Username;
                    user.BirthDate = model.BirthDate;
                    user.EMail = model.EMail;
                    user.Password = model.Password;
                    user.ConfirmCode = confirmcode;
                    user.IsActive = false;

                    _newscontext.Users.Add(user);
                    _newscontext.SaveChanges();

                    //email gönderme kodu. EMail ile kullanıcıya 31. satırdaki confirmcode u yolla. 
                    //http://localhost:5000/Register/Confirm/22336525112asd

                    string confirmurl = "https://localhost:44356/Register/Confirm/" + confirmcode;

                    MimeMessage message = new MimeMessage();

                    MailboxAddress from = new MailboxAddress("SportsNewsTeam", "sportsnewsteam.noreply@gmail.com");
                    message.From.Add(from);

                    MailboxAddress to = new MailboxAddress(user.Name, user.EMail);
                    message.To.Add(to);

                    message.Subject = "no-reply";

                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = "Please click on the link to confirm your email address: " + confirmurl;

                    message.Body = bodyBuilder.ToMessageBody();

                    SmtpClient client = new SmtpClient();

                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate("sportsnewsteam.noreply@gmail.com", "$Rdot3PxrtV9QQpYFzVYA#w%RpU2!BGC5UN8cSXNhAs@iq@GvZ");


                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();

                    return RedirectToAction("PendingPage", "Register");
                }
                else
                {
                    ModelState.AddModelError("EMail", "Bu email adresini kullanan bir hesap var, lütfen farklı bir email adresi girin! Ya da zaten hesabınız varsa giriş yapmayı deneyin...");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        
        public IActionResult PendingPage()
        {
            return View();
        }


        [HttpGet("Register/Confirm/{confirmcode}")]
        public async Task<IActionResult> Confirmcode(string confirmcode)
        {
            User user = _newscontext.Users.FirstOrDefault(q => q.ConfirmCode == confirmcode);

            if (user != null)
            {
                user.IsActive = true;
                _newscontext.SaveChanges();

                //kullanıcıyı login yap

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.EMail),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.UserData,"Site")
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(principal);

                user.LastLogin = DateTime.Now;

                _newscontext.SaveChanges();
            }
            return Redirect("/Home/Index");
        }
    }
}
