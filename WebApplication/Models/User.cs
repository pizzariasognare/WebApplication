using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required(ErrorMessage = "O perfil do usuário é obrigatório.")]
        public int profile_id { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        public string email { get; set; }

        [Required(ErrorMessage = "O senha é obrigatória.")]
        public string password { get; set; }

        public short enabled { get; set; }
        
        public Profile Profile;
    }
}