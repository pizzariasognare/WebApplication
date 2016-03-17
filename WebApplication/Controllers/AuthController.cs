using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Repositories;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private IUserService user_service;

        public AuthController()
        {
            this.user_service = new UserService(new UserRepository());
        }

        public AuthController(IUserService user_service)
        {
            this.user_service = user_service;
        }

        [HttpGet]
        public ActionResult Login(string return_url)
        {
            var model = new Auth
            {
                return_url = return_url
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(Auth model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = new User();
            user = this.user_service.GetEnabledUser(model.email, model.password);
            if (user != null)
            {
                var identity = new ClaimsIdentity(new[] {
                                    new Claim(ClaimTypes.PrimarySid, user.id.ToString()),                                    
                                    new Claim(ClaimTypes.Email, user.email),
                                    new Claim(ClaimTypes.Role, user.profile_id.ToString())
                                 },
                                "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.return_url));
            }

            ModelState.AddModelError("", "E-mail ou senha inválido(s).");

            return View();
        }

        public ActionResult logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");

            return RedirectToAction("Index", "Home");
        }

        private string GetRedirectUrl(string return_url)
        {
            if (string.IsNullOrEmpty(return_url) || !Url.IsLocalUrl(return_url))
            {
                return Url.Action("Index", "Home");
            }

            return return_url;
        }
    }
}