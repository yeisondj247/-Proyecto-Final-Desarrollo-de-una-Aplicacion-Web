using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Noodle.Models;
using Noodle.ViewModels;

namespace Noodle.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        private ApplicationDbContext _context;
        public ProductsController()
        {
            _context = new ApplicationDbContext();
            RedirectToAction("Index", "Home");
    }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        /*public ActionResult Index()
        {
            var products = new List<Products>
            {
                new Products {Id = 1, Name ="Mouse", Category = "Electronics", Lot = 5, LotMax = 10, LotMin = 0, Price = 500, SellerId = 232 }
            };
        }*/
        public ActionResult New(int id = 0)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var sellers = _context.Sellers.ToList();
            var viewModel = new ProductFormViewModel
            {
                Sellers = sellers,
                Id = id
            };
            return View("ProductForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ProductFormViewModel product)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            if (product.Products.Id == 0)
                _context.Products.Add(product.Products);
            else
            {
                var customerInDb = _context.Products.Single(c => c.Id == product.Products.Id);

                customerInDb.Name = product.Products.Name;
                customerInDb.Lot = product.Products.Lot;
                customerInDb.Price = product.Products.Price;
                customerInDb.SellersId = product.Products.SellersId;
            }
            _context.Configuration.ValidateOnSaveEnabled = false;
            _context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
        public ActionResult Index(int Id = 0)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var products = _context.Products.Include(c => c.Sellers).OrderByDescending(m => m.Id).ToList();
            var productsinbill = _context.ProductsInBill.ToList();
            var ViewModel = new ProductViewModel
            {
                ProductsInBill = productsinbill,
                Products = products
            };
            return View(ViewModel);
        }
        public ActionResult Available()
        {
            var products = _context.Products.Include(c => c.Sellers).ToList();
            var ViewModel = new ProductViewModel
            {
                Products = products
            };
            return View(ViewModel);
        }
        public ActionResult Details(int id = 0)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var product = _context.Products.SingleOrDefault(c => c.Id == id);

            if (product == null)
                return HttpNotFound();
            var ViewModel = new OneProductViewModel
            {
                Product = product
            };
            return View(ViewModel);
        }
        public ActionResult Empty(int id = 0)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var product = _context.Products.SingleOrDefault(c => c.Id == id);

            if (product == null)
                return HttpNotFound();
            if (product.Lot > 0)
                return RedirectToAction("Details", "Products", new { Id = id });
            var ViewModel = new OneProductViewModel
            {
                Product = product
            };
            return View(ViewModel);
        }
        public ActionResult Edit(int id = 0)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var product = _context.Products.SingleOrDefault(c => c.Id == id);

            if (product == null)
                return HttpNotFound();
            var ViewModel = new ProductFormViewModel
            {
                Products = product,
                Sellers = _context.Sellers.ToList()
            };
            return View("ProductForm", ViewModel);
        }
        public ActionResult Remove(int id = 0)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var product = _context.Products.SingleOrDefault(c => c.Id == id);

            if (product == null)
                return HttpNotFound();
            var ViewModel = new OneProductViewModel
            {
                Product = product
            };
            return View(ViewModel);
        }
        public ActionResult ConfirmRemove(int id = 0)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var product = _context.Products.SingleOrDefault(c => c.Id == id);
            if (product == null)
                return HttpNotFound();
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
    }
}