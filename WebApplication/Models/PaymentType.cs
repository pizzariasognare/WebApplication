using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("PaymentType")]
    public class PaymentType
    {
        public const int DINHEIRO = 1;        

        [Key]
        [Column("id")]
        public int id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string name { get; set; }

        public short enabled { get; set; }
    }
}