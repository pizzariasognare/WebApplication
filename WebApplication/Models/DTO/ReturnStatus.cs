using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ReturnStatus
    {
        public ReturnStatus()
        {
            this.success = false;
        }

        public ReturnStatus(bool sucesss, string message)
        {
            this.success = success;
            this.message = message;
        }

        public bool success { get; set; }
        public string message { get; set; }
    }
}