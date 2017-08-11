using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noodle.Models
{
    public class ProductsInBill
    {
        public int Id { get; set; }
        [Required]
        public int ProductsId { get; set; }
        [ForeignKey("ProductsId")]
        public virtual Products Products { get; set; }
        public int Lot { get; set; }
    }
}