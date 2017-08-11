using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Noodle.Models;
using Noodle.ViewModels;

namespace Noodle.Controllers
{
    public class AdminController : Controller
    {
        // GET: Config
        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index(int Id = 0)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var config = _context.Config.SingleOrDefault(m => m.Id == 1);
            var ViewModel = new ConfigViewModel
            {
                Config = config,
                Id = Id
            };
            return View(ViewModel);
        }
        public ActionResult Cashout()
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var config = _context.Config.SingleOrDefault(c => c.Id == 1);
            var ViewModel = new ConfigViewModel
            {
                Config = config
            };
            return View(ViewModel);
        }
        public ActionResult ConfirmCashout()
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var configInDb = _context.Config.Single(c => c.Id == 1);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Edit()
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var config = _context.Config.SingleOrDefault(m => m.Id == 1);
            var ViewModel = new EditConfigViewModel
            {
                Config = config
            };
            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(EditConfigViewModel m)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Login");
            var configInDb = _context.Config.Single(c => c.Id == 1);
            if(m.Config.Pass == "")
            {
                return RedirectToAction("Edit", "Config");
            }
            else
            {
                configInDb.Pass = m.Config.Pass;
                configInDb.User = m.Config.User;
                _context.SaveChanges();
                return RedirectToAction("Index", "Config");
            }
        }
        public ActionResult DellAll(int Id = 0)
        {
            if (Id == 0 || Id > 6)
                return RedirectToAction("Index", "Config");
            var ViewModel = new ConfigDellAllViewModel
            {
                Id = Id
            };
            return View(ViewModel);
        }
    }
}