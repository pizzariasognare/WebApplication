using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("PizzaSize")]
    public class PizzaSize
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "O tamanho da pizza é obrigatório.")]
        public string name { get; set; }

        public int size { get; set; }

        public int slices { get; set; }

        public decimal price { get; set; }        
    }
}