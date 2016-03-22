using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("ZipCode")]
    public class ZipCode
    {
        public ZipCode() { }

        public ZipCode(string zip_code)
        {
            this.zip_code = zip_code;
        }

        [Key]
        [Column("id")]
        public int id { get; set; }

        [MaxLength(8)]
        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public string zip_code { get; set; }

        public decimal delivery_price { get; set; }
    }
}