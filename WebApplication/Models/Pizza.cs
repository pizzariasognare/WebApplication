using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("Pizza")]
    public class Pizza
    {
        public Pizza()
        {
            this.PizzaFlavor = new PizzaFlavor();
            this.PizzaSize = new PizzaSize();
        }

        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required(ErrorMessage = "O sabor da pizza é obrigatório.")]
        public int pizza_flavor_id { get; set; }

        [Required(ErrorMessage = "O tamanho da pizza é obrigatório.")]
        public int pizza_size_id { get; set; }

        [Required(ErrorMessage = "O preço da pizza é obrigatório.")]
        public decimal price { get; set; }

        public short enabled { get; set; }

        public PizzaFlavor PizzaFlavor;

        public PizzaSize PizzaSize;
    }
}