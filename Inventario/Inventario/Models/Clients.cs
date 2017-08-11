using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Noodle.Models
{
    public class Clients
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public String Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public String Cedula { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        [StringLength(50)]
        public String Telefono { get; set; }
        [Required]
        [StringLength(50)]
        public String Direccion { get; set; }

    }
}