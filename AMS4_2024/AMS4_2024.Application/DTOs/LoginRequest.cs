using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS4_2024.Application.DTOs
{
    public class LoginRequest 
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
