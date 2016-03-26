using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        public const int ABRINDO = 1;
        public const int ABERTO = 2;
        public const int EM_PRODUCAO = 3;
        public const int PRONTO = 4;
        public const int EM_ENTREGA = 5;
        public const int ENTREGUE = 6;
        public const int CANCELADO = 7;
        public const int FECHADO = 8;

        [Key]
        [Column("id")]
        public int id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string description { get; set; }
    }
}