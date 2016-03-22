using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("Ingredient")]
    public class Ingredient
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "O nome do ingrediente é obrigatório.")]
        public string name { get; set; }       
    }
}