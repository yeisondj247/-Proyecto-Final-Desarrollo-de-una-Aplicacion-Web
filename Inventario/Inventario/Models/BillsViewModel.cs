using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Noodle.Models;

namespace Noodle.ViewModels
{
    public class BillsViewModel
    {
        public List<Bills> Bills { get; set; }
        public int Id { get; set; }
    }
}