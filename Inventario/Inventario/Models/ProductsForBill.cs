using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noodle.Models
{
    public class ProductsForBill
    {
        public int Id { get; set; }
        public String ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int BillsId { get; set; }
        [ForeignKey("BillsId")]
        public virtual Bills Bills { get; set; }
        public int Lot { get; set; }
    }
}