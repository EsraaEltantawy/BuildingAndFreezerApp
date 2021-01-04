using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BuildingAndFreezerApp.Models;

namespace BuildingAndFreezerApp.Controllers
{
    [Authorize(Roles = "Admins , Freezer", Users = "hossam50@gmail.com, ayman5383@gmail.com, MangerFreezer@gamil.com")]
    public class BankFreezersController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();
        FreezerRepositry repositry = new FreezerRepositry();
        // GET: BankFreezers
        public ActionResult Index()
        {
            var index = db.BankFreezers.Max(x => x.id);
            ViewBag.CurrentPrice = db.BankFreezers.Find(index).CurrentPrices;
            repositry.BankFreezer = db.BankFreezers.ToList();
            repositry.Trader = db.Traders.ToList();
            repositry.Customer = db.Customers.ToList();
            repositry.Payment = db.Payments.ToList();
            return View(repositry);
        }

        [Authorize(Roles = "Admins")]
        // GET: BankFreezers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BankFreezers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public ActionResult Create(BankFreezer bankFreezer)
        {
            if (ModelState.IsValid)
            {
                var index = db.BankFreezers.Max(x => x.id);
                var bank = db.BankFreezers.Find(index);
                bankFreezer.CurrentPrices += bank.CurrentPrices;
                db.BankFreezers.Add(bankFreezer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bankFreezer);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
