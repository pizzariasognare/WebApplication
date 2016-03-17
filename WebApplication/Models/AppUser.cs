using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace WebApplication.Models
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
            : base(principal)
        {
        }

        public int id
        {
            get
            {
                return Convert.ToInt32(this.FindFirst(ClaimTypes.PrimarySid).Value);
            }
        }

        public string email
        {
            get
            {
                return this.FindFirst(ClaimTypes.Email).Value;
            }
        }

        public int profile_id
        {
            get
            {
                return Convert.ToInt32(this.FindFirst(ClaimTypes.Role).Value);
            }
        }
    }
}