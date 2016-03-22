using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("PizzaFlavor")]
    public class PizzaFlavor
    {
        public PizzaFlavor()
        {
            this.Ingredient = new List<Ingredient>();
            this.Pizza = new List<Pizza>();
        }

        [Key]
        [Column("id")]
        public int id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "O nome do sabor da pizza é obrigatório.")]
        public string name { get; set; }

        [MaxLength(255)]        
        public string image { get; set; }       

        public short enabled { get; set; }

        public List<Ingredient> Ingredient;

        public List<Pizza> Pizza;
    }
}