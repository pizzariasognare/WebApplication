using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("OrderDrink")]
    public class OrderDrink
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "O pedido é obrigatório.")]
        public int order_id { get; set; }

        [Required(ErrorMessage = "A bebida é obrigatório.")]
        public int drink_id { get; set; }        

        public Drink Drink;
    }
}