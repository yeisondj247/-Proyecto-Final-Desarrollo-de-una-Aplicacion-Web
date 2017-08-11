using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Noodle.Models;

namespace Noodle.ViewModels
{
    public class ProductsInBillFormViewModel
    {
        public int Id { get; set; }
        public ProductsInBill Products { get; set; }
    }
}