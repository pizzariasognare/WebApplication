using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "O data é obrigatória.")]
        public DateTime order_date { get; set; }

        public int? payment_type_id { get; set; }

        [Required(ErrorMessage = "O endereço do cliente é obrigatório.")]
        public int customer_address_id { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O preço é obrigatório.")]
        public decimal price { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O desconto é obrigatório.")]
        public decimal discount { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O preço final é obrigatório.")]
        public decimal final_price { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O troco é obrigatório.")]
        public decimal change { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O pagamento é obrigatório.")]
        public decimal payment { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "O taxa de entrega é obrigatório.")]
        public decimal delivery_price { get; set; }

        [DataType(DataType.MultilineText)]
        public string note { get; set; }

        public PaymentType PaymentType;

        public CustomerAddress CustomerAddress;

        public OrderLog LastOrderLog;

        public List<OrderPizza> OrderPizza;

        public List<OrderDrink> OrderDrink;
    }
}