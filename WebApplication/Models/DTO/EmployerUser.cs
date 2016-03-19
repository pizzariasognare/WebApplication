using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class EmployerUser
    {

        public Employer Employer { get; set; }
        public User User { get; set; }
    }
}