using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noodle.Models
{
    public class Products
    {
        public int Id { get; set; }
        [Display(Name = "Nombre del producto")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }


        [Required]
        [Range(0, 1000000)]
        [Display(Name = "Cantidad")]
        public int Lot { get; set; }
        [Required]
        [Range(1, 1000000, ErrorMessage = "El precio debe ser mayor que 0")]
        [Display(Name = "Precio")]
        public int Price { get; set; }

        [Display(Name = "Vendedor")]

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Debes seleccionar un vendedor")]
        public int SellersId { get; set; }
        [ForeignKey("SellersId")]
        public virtual Sellers Sellers { get; set; }
    }
}
