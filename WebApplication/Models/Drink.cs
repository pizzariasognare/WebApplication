using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("Drink")]
    public class Drink
    {
        public Drink()
        {
            this.DrinkType = new DrinkType();
        }

        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required(ErrorMessage = "O tipo de bebida é obrigatório.")]
        public int drink_type_id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string name { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        public decimal price { get; set; }

        public string image { get; set; }

        public short enabled { get; set; }

        public DrinkType DrinkType;
    }
}