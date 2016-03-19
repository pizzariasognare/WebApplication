using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("CustomerAddress")]
    public class CustomerAddress
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        public int customer_id { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "O CEP deve ter só números.")]
        public string zip_code { get; set; }

        [Required(ErrorMessage = "O logradouro é obrigatório.")]
        public string address { get; set; }

        public int? number { get; set; }

        public string complement { get; set; }

        [Required(ErrorMessage = "O bairro é obrigatório.")]
        public string neighborhood { get; set; }

        [Required(ErrorMessage = "O cidade é obrigatória.")]
        public string city { get; set; }

        [Required(ErrorMessage = "O UF é obrigatória.")]
        public string acronym_city { get; set; }

        public string reference_point { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }

        public short enabled { get; set; }
    }
}