using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Noodle.Models;
using Noodle.ViewModels;

namespace Noodle.Models
{
    public class SellersController : Controller
    {
        // GET: Sellers
        private ApplicationDbContext _context;
        public SellersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult New()
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            return View("SellerForm");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SellerFormViewModel seller)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            if (seller.Sellers.Id == 0)
                _context.Sellers.Add(seller.Sellers);
            else
            {
                var sellerInDb = _context.Sellers.Single(c => c.Id == seller.Sellers.Id);

                sellerInDb.Nombre = seller.Sellers.Nombre;
                sellerInDb.Cedula = seller.Sellers.Cedula;
                sellerInDb.Descripcion = seller.Sellers.Descripcion;
                sellerInDb.Direccion = seller.Sellers.Direccion;
                sellerInDb.Telefono = seller.Sellers.Telefono;
            }
            _context.SaveChanges();
            return RedirectToAction("Index","Sellers");
        }
        public ActionResult Edit(int id)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var seller = _context.Sellers.SingleOrDefault(c => c.Id == id);

            if (seller == null)
                return HttpNotFound();
            var ViewModel = new SellerFormViewModel
            {
                Sellers = seller
            };
            return View("SellerForm", ViewModel);
        }
        public ActionResult Details(int id)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var seller = _context.Sellers.SingleOrDefault(c => c.Id == id);

            if (seller == null)
                return HttpNotFound();
            var ViewModel = new OneSellerViewModel
            {
                Seller = seller
            };
            return View(ViewModel);
        }
        public ActionResult Index()
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var sellers = _context.Sellers.ToList();
            var ViewModel = new SellerViewModel
            {
                Sellers = sellers
            };
            return View(ViewModel);
        }
        public ActionResult Remove(int id)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var seller = _context.Sellers.SingleOrDefault(c => c.Id == id);

            if (seller == null)
                return HttpNotFound();
            var ViewModel = new OneSellerViewModel
            {
                Seller = seller
            };
            return View(ViewModel);
        }
        public ActionResult ConfirmRemove(int id)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var seller = _context.Sellers.SingleOrDefault(c => c.Id == id);
            _context.Sellers.Remove(seller);
            _context.SaveChanges();
            return RedirectToAction("Index", "Sellers");
        }
    }
}