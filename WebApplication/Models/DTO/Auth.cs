using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Models
{
    public class Auth
    {        
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        [Required(ErrorMessage = "E-mail é obrigatório.")]
        public string email { get; set; }
        
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [Required(ErrorMessage = "Senha é obrigatória.")]
        public string password { get; set; }

        [HiddenInput]
        public string return_url { get; set; }
    }
}