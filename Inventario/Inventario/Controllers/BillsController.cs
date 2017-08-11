using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Noodle.Models;
using Noodle.ViewModels;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

namespace Noodle.Controllers
{
    public class BillsController : Controller
    {
        // GET: Bills
        private ApplicationDbContext _context;

        public BillsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose( bool disposing )
        {
            _context.Dispose();
        }

        public ActionResult Index( int id = 0 ) //Ver todas las facturas
        {
            if ( Session[ "login" ] == null )
                return RedirectToAction( "Index" , "Login" );
            var Bills = _context.Bills.OrderByDescending( m => m.Id ).ToList();
            var ViewModel = new BillsViewModel
            {
                Bills = Bills ,
                Id = id
            };
            return View( ViewModel );
        }
        public ActionResult Edit( int id ) //Editar producto en carrito
        {
            if ( Session[ "login" ] == null )
                return RedirectToAction( "Index" , "Login" );
            var product = _context.ProductsInBill.SingleOrDefault( c => c.Id == id );

            if ( product == null )
                return HttpNotFound();
            var ViewModel = new ProductsInBillFormViewModel
            {
                Products = product
            };
            return View( "BillForm" , ViewModel );
        }
        public ActionResult New( int id ) //Formulario para agregar producto a carrito
        {
            if ( Session[ "login" ] == null )
                return RedirectToAction( "Index" , "Login" );
            var productbillInDb = _context.ProductsInBill.SingleOrDefault( m => m.ProductsId == id );
            if ( productbillInDb != null )
                return RedirectToAction( "Edit" , "Bills" , new { Id = productbillInDb.Id } );
            var products = _context.Products.SingleOrDefault( c => c.Id == id );
            var bill = new ProductsInBill
            {
                Id = 0 ,
                ProductsId = id ,
                Products = products ,
                Lot = 0
            };
            if ( products == null )
                return HttpNotFound();
            var clients = _context.Clients.ToList();
            var viewModel = new ProductsInBillFormViewModel
            {
                Products = bill
            };
            return View( "BillForm" , viewModel );
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save( ProductsInBillFormViewModel bill ) //Guardar producto en carrito
        {
            if ( Session[ "login" ] == null )
                return RedirectToAction( "Index" , "Login" );
            var productInDb = _context.Products.Single( c => c.Id == bill.Products.ProductsId );
            var configInDb = _context.Config.Single( c => c.Id == 1 );
            if ( productInDb.Lot < bill.Products.Lot )
                return RedirectToAction( "Empty" , "Products" , new { Id = bill.Products.ProductsId } );
            if ( bill.Products.Id == 0 )
            {
                _context.ProductsInBill.Add( bill.Products );
            }

            else
            {
                var productbillInDb = _context.ProductsInBill.Single( c => c.Id == bill.Products.Id );

                productbillInDb.ProductsId = bill.Products.ProductsId;
                productbillInDb.Lot = bill.Products.Lot;
            }
            _context.SaveChanges();
            return RedirectToAction( "Index" , "Products" );
        }
        public ActionResult Remove( int id ) //Eliminar factura
        {
            if ( Session[ "login" ] == null )
                return RedirectToAction( "Index" , "Login" );
            var bill = _context.Bills.SingleOrDefault( c => c.Id == id );

            if ( bill == null )
                return HttpNotFound();
            var ViewModel = new OneBillViewModel
            {
                Bill = bill
            };
            return View( ViewModel );
        }
        public ActionResult ConfirmRemove( int id ) //Confirmar eliminar factura
        {
            if ( Session[ "login" ] == null )
                return RedirectToAction( "Index" , "Login" );
            var bill = _context.Bills.SingleOrDefault( c => c.Id == id );
            _context.Bills.Remove( bill );
            _context.SaveChanges();
            return RedirectToAction( "Index" , "Bills" );
        }
        public ActionResult RemoveProduct( int id ) //Eliminar prducto del carrito
        {
            if ( Session[ "login" ] == null )
                return RedirectToAction( "Index" , "Login" );
            var productInBill = _context.ProductsInBill.SingleOrDefault( c => c.Id == id );
            _context.ProductsInBill.Remove( productInBill );
            _context.SaveChanges();
            return RedirectToAction( "Products" , "Bills" );
        }
        public ActionResult Details( int? id ) //Detalles de la factura
        {
            if ( Session[ "login" ] == null )
                return RedirectToAction( "Index" , "Login" );
            if ( id == null )
                return HttpNotFound();
            var bill = _context.Bills.SingleOrDefault( c => c.Id == id );
            if ( bill == null )
                return HttpNotFound();

            var products = _context.ProductsForBill.Where( c => c.BillsId == id ).ToList();
            var client = _context.Clients.SingleOrDefault( c => c.Id == bill.ClientsId );
            var ViewModel = new ProductInBillViewModel
            {
                Name = "Factura " + bill.Id ,
                ProductsFor = products ,
                ClientName = client.Nombre ,
                Total = bill.Price ,
                Id = bill.Id

            };
            return View( ViewModel );
        }
        public ActionResult DetailsPDF( int? id ) //Detalles de la factura para PDF
        {
            if ( id == null )
                return HttpNotFound();
            var bill = _context.Bills.SingleOrDefault( c => c.Id == id );
            if ( bill == null )
                return HttpNotFound();

            var products = _context.ProductsForBill.Where( c => c.BillsId == id ).ToList();
            var client = _context.Clients.SingleOrDefault( c => c.Id == bill.ClientsId );
            var ViewModel = new ProductInBillViewModel
            {
                Name = "Factura " + bill.Id ,
                ProductsFor = products ,
                ClientName = client.Nombre ,
                Total = bill.Price ,
                Id = bill.Id

            };
            return View( ViewModel );
        }
        public ActionResult Print( int id ) //Imprimir
        {
            var bill = _context.Bills.SingleOrDefault( c => c.Id == id );

            if ( bill == null )
                return HttpNotFound();
            return new Rotativa.ActionAsPdf( "DetailsPDF" , new { Id = id } );
        }
        public ActionResult Products() //Ver productos del carrito
        {
            if ( Session[ "login" ] == null )
                return RedirectToAction( "Index" , "Login" );
            var products = _context.ProductsInBill.ToList();
            if ( _context.ProductsInBill.ToList().Count == 0 )
                return RedirectToAction( "Index" , "Products" );
            var total = _context.ProductsInBill.Sum( m => m.Lot * m.Products.Price );
            var ViewModel = new ProductInBillViewModel
            {
                Products = products ,
                Total = total
            };
            return View( ViewModel );
        }
        public ActionResult Process() //Ver productos del carrito para facturar
        {
            if ( Session[ "login" ] == null )
                return RedirectToAction( "Index" , "Login" );
            var products = _context.ProductsInBill.ToList();
            if ( _context.ProductsInBill.ToList().Count == 0 )
                return RedirectToAction( "Index" , "Products" );
            var total = _context.ProductsInBill.Sum( m => m.Lot * m.Products.Price );
            var clients = _context.Clients.ToList();
            var ViewModel = new ProductInBillViewModel
            {
                Products = products ,
                Total = total ,
                Clients = clients
            };
            return View( ViewModel );
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessConfirm( ProductInBillViewModel Form ) //Crear factura
        {
            if ( Session[ "login" ] == null )
                return RedirectToAction( "Index" , "Login" );
            var products = _context.ProductsInBill.ToList();
            if ( products.Count == 0 )
                return RedirectToAction( "Index" , "Products" );
            var client = _context.Clients.SingleOrDefault( m => m.Id == Form.ClientsId );
            if ( client == null )
                return HttpNotFound();
            var newBill = new Bills 
            {
                ClientsId = Form.ClientsId ,
                Price = products.Sum( m => m.Products.Price * m.Lot ) ,
                Fecha = DateTime.Today.ToShortDateString() //Funcion para obtener la fecha actual 
            };
            _context.Bills.Add( newBill ); // ultima factura
            _context.SaveChanges();
            var BillId = _context.Bills.Max( item => item.Id );  //
            foreach ( var product in products )
            {
                var productsInDb = _context.Products.SingleOrDefault( m => m.Id == product.ProductsId );
                productsInDb.Lot -= product.Lot;
                productsInDb.Name = productsInDb.Name;
                productsInDb.Price = productsInDb.Price;
                productsInDb.SellersId = productsInDb.SellersId;
                var productbill = new ProductsForBill
                {
                    ProductName = product.Products.Name ,
                    ProductPrice = product.Products.Price ,
                    BillsId = BillId ,
                    Lot = product.Lot
                };
                _context.ProductsInBill.Remove( product );
                _context.ProductsForBill.Add( productbill );
            }
            _context.Configuration.ValidateOnSaveEnabled = false;
            _context.SaveChanges();
            return RedirectToAction( "Index" , "Bills" );
        }
        public ActionResult ProductsClear() //Limpiar carrito de compras
        {
            var products = _context.ProductsInBill.ToList();
            foreach ( var product in products )
                _context.ProductsInBill.Remove( product );
            _context.SaveChanges();
            return RedirectToAction( "Index" , "Products" );
        }
    }
}