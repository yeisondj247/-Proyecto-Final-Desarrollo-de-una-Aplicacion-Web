using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Noodle.Models;
using Noodle.ViewModels;

namespace Noodle.Controllers
{
    public class ClientsController : Controller
    {
        // GET: Client
        private ApplicationDbContext _context;

        public ClientsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var clients = _context.Clients.ToList();
            var ViewModel = new ClientViewModel
            {
                Clients = clients
            };
            return View(ViewModel);
        }
        public ActionResult New()
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            return View("ClientForm");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ClientFormViewModel client)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            if (client.Clients.Id == 0)
                _context.Clients.Add(client.Clients);
            else
            {
                var clientInDb = _context.Clients.Single(c => c.Id == client.Clients.Id);

                clientInDb.Nombre = client.Clients.Nombre;
                clientInDb.Cedula = client.Clients.Cedula;
                clientInDb.Email = client.Clients.Email;
                clientInDb.Direccion = client.Clients.Direccion;
                clientInDb.Telefono = client.Clients.Telefono;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Clients");
        }
        public ActionResult Edit(int id = 0)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var client = _context.Clients.SingleOrDefault(c => c.Id == id);

            if (client == null)
                return HttpNotFound();
            var ViewModel = new ClientFormViewModel
            {
                Clients = client
            };
            return View("ClientForm", ViewModel);
        }
        public ActionResult Remove(int id = 0)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var client = _context.Clients.SingleOrDefault(c => c.Id == id);

            if (client == null)
                return HttpNotFound();
            var ViewModel = new OneClientViewModel
            {
                Client = client
            };
            return View(ViewModel);
        }
        public ActionResult ConfirmRemove(int id = 0)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var client = _context.Clients.SingleOrDefault(c => c.Id == id);
            if (client == null)
                return HttpNotFound();
            _context.Clients.Remove(client);
            _context.SaveChanges();
            return RedirectToAction("Index", "Clients");
        }
        public ActionResult Details(int id = 0)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var client = _context.Clients.SingleOrDefault(c => c.Id == id);

            if (client == null)
                return HttpNotFound();
            var ViewModel = new OneClientViewModel
            {
                Client = client
            };
            return View(ViewModel);
        }
    }
}