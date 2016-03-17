using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("Customer")]
    public class Customer
    {        
        [Key]
        [Column("id")]
        public int id { get; set; }
       
        public int? user_id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string name { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "O telefone deve ter só números.")]
        public string phone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ConvertEmptyStringToNull = true, ApplyFormatInEditMode = true)]
        public DateTime? birth_date { get; set; }

        public short enabled { get; set; }        
        
        public User User;

        public List<CustomerAddress> CustomerAddress;
    }
}