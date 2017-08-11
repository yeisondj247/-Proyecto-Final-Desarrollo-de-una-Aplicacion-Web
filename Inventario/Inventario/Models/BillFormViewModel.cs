using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Noodle.Models;

namespace Noodle.ViewModels
{
    public class BillFormViewModel
    {
        public ProductsInBill ProductInBill { get; set; }
        public IEnumerable<Products> Products { get; set; }
        public IEnumerable<Clients> Clients { get; set; }
    }
}