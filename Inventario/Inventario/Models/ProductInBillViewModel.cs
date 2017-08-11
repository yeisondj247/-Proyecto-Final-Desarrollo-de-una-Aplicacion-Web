using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Noodle.Models;

namespace Noodle.ViewModels
{
    public class ProductInBillViewModel
    {
        public List<ProductsInBill> Products { get; set; }
        public List<ProductsForBill> ProductsFor { get; set; }
        public int Total { get; set; }
        public IEnumerable<Clients> Clients { get; set; }
        public int ClientsId { get; set; }
        public String Name { get; set; }
        public String ClientName { get; set; }
        public int Id { get; set; }
    }
}