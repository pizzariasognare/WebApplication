using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public abstract class AppController : Controller
    {
        public AppUser CurrentUser
        {
            get
            {
                return new AppUser(this.User as ClaimsPrincipal);
            }
        }
    }
}