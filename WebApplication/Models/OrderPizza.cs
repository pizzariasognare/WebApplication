using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("OrderPizza")]
    public class OrderPizza
    {
        [Key]                
        public int id { get; set; }

        [Required(ErrorMessage = "O pedido é obrigatório.")]
        public int order_id { get; set; }

        [Required(ErrorMessage = "A pizza é obrigatório.")]
        public int pizza_id { get; set; }        

        public Pizza Pizza;

        public Order Order;
    }
}