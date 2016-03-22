using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("PizzaFlavorIngredient")]
    public class PizzaFlavorIngredient
    {
        [Key]        
        [Column(Order = 1)]
        public int pizza_flavor_id { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ingredient_id { get; set; }        
    }
}