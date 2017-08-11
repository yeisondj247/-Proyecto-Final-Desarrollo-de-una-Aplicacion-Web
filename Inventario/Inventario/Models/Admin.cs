using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Noodle.Models
{
    public class Admin
    {
        public int Id { get; set; }
        [MinLength(4, ErrorMessage = "El nombre de usuario debe ser mayor que 3 caracteres.")]
        [MaxLength(50, ErrorMessage = "El nombre de usuario debe ser menor que 50 caracteres.")]
        [Display(Name = "Usuario")]
        public String User { get; set; }
        [Display(Name = "Password")]
        [MinLength(4, ErrorMessage = "La contraseña debe ser mayor que 3 caracteres.")]
        [MaxLength(50, ErrorMessage = "La contraseña debe ser memnor que 50 caracteres.")]
        public String Pass { get; set; }
    }
}