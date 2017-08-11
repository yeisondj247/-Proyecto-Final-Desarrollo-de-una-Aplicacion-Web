using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Noodle.Models;

namespace Noodle.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public List<Products> Products { get; set; }
        public List<ProductsInBill> ProductsInBill { get; set; }
    }
}
