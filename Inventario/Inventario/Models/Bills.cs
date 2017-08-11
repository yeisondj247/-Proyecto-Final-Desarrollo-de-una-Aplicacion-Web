using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noodle.Models
{
    public class Bills
    {
        public int Id { get; set; }
        [Display(Name = "Seleccione un cliente")]
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Debes seleccionar un cliente")]
        public int ClientsId { get; set; }
        [ForeignKey("ClientsId")]
        public virtual Clients Clients { get; set; }
        [Display(Name = "Seleccione un producto")]
        [Required]
        [Range(1,Int32.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
        public int Price { get; set; }
        [Required]
        [StringLength(50)]
        public String Fecha { get; set; }
    }
}