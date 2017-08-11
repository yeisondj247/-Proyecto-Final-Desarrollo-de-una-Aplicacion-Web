using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Noodle.Models
{
    public class Sellers
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public String Nombre { get; set; }
        [Required]
        [StringLength(15)]
        public String Cedula { get; set; }
        [Required]
        [StringLength(15)]
        public String Telefono { get; set; }
        [Required]
        [StringLength(50)]
        public String Direccion { get; set; }
        [Required]
        [StringLength(100)]
        public String Descripcion { get; set; }

    }
}