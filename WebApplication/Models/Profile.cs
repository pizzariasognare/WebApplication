using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("Profile")]
    public class Profile
    {
        public const int CLIENTE = 1;
        public const int ENTREGADOR = 2;
        public const int ATENDENTE = 3;
        public const int GERENTE = 4;
        public const int ADMINISTRADOR = 5;

        [Key]
        [Column("id")]
        public int id { get; set; }

        public string name { get; set; }

        public int level { get; set; }        
    }
}