using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Noodle.Models;
using Noodle.ViewModels;

namespace Noodle.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private ApplicationDbContext _context;

        public LoginController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Validate(LoginFormViewModel login)
        {
            var config = _context.Config.SingleOrDefault(c => c.Id == 1);
            if (login.Config.User == config.User && login.Config.Pass == config.Pass)
            {
                Session["login"] = login;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Login", new { id=404 });
            }
        }
        public ActionResult Index(int id = 0)
        {
            var viewModel = new LoginFormViewModel{
                Id = id
            };
            return View(viewModel);
        }
        public ActionResult Out()
        {
            Session["login"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}