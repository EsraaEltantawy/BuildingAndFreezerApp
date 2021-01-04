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
    [Authorize(Roles = "Admins , Building", Users = "hossam50@gmail.com, ayman5383@gmail.com, MangerBuilding@gamil.com")]

    public class BankBuildingsController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();
        private BuildingRepositry repositry = new BuildingRepositry();
        // GET: BankBuildings
        public ActionResult Index()
        {

            return View(repositry);
        }


        [Authorize(Roles = "Admins")]
        // GET: BankBuildings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BankBuildings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public ActionResult Create(BankBuilding bankBuilding)
        {
            if (ModelState.IsValid)
            {
                var index = db.BankBuildings.Max(x => x.id);
                var bank = db.BankBuildings.Find(index);
                bankBuilding.CurrentPrices += bank.CurrentPrices;
                db.BankBuildings.Add(bankBuilding);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bankBuilding);
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
