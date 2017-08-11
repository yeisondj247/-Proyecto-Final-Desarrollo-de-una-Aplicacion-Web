using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Noodle.Models;

namespace Noodle.ViewModels
{
    public class ProductFormViewModel
    {
        public IEnumerable<Sellers> Sellers { get; set; }
        public Products Products { get; set; }
        public int Id { get; set; }
    }
}
