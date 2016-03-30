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
            this.meta = new List<string>();
        }

        public ReturnStatus(bool sucesss, string message)
        {
            this.success = success;
            this.message = message;
            this.meta = new List<string>();
        }

        public bool success { get; set; }
        public string message { get; set; }
        public List<string> meta { get; set; }
    }
}