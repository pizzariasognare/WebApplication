using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("Employer")]
    public class Employer
    {
        [Key]
        [Column("id")]
        public int id { get; set; }
       
        public int? user_id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string name { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "O telefone deve ter só números.")]
        public string phone { get; set; }        

        public short enabled { get; set; }

        public User User;
    }
}