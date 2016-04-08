using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Models
{
    public class TotalOrdersDeliveredToday
    {
        public string name { get; set; }
        public int order_total { get; set; }
        public decimal delivery_price_total { get; set; }
    }
}