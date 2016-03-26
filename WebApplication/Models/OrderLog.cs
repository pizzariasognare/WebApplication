using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("OrderLog")]
    public class OrderLog
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required(ErrorMessage = "O pedido é obrigatório.")]
        public int order_id { get; set; }

        [Required(ErrorMessage = "O status do pedido é obrigatório.")]
        public int order_status_id { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório.")]
        public int user_id { get; set; }

        [MaxLength(100)]
        public string note { get; set; }

        [Required(ErrorMessage = "A data do pedido é obrigatória.")]
        public DateTime order_log_datetime { get; set; }

        public OrderStatus OrderStatus;

        public User User;
    }
}