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
    public class periodsController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();

        // GET: periods
        public ActionResult Index()
        {
            return View(db.periods.ToList());
        }

        public ActionResult Details(int id)
        {
            FreezerRepositry free = new FreezerRepositry();
           // free.Customer = db.Customers.Where(x => x.PeriodId == id);
           // free.Trader = db.Traders.Where(x => x.PeriodId == id);
            return View(free);
        }
        // GET: periods/Create
        public ActionResult Create()
        {

            ViewBag.FrID = new SelectList(db.Fridages, "Fr_id", "Fr_Name");
            return View();
        }

        // POST: periods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(period period)
        {
            if (ModelState.IsValid)
            {
                period.isActive = true;
                db.periods.Add(period);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(period);
        }

        // GET: periods/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            period period = db.periods.Find(id);
            if (period == null)
            {
                return HttpNotFound();
            }

            ViewBag.FrID = new SelectList(db.Fridages, "Fr_id", "Fr_Name");
            return View(period);
        }

        // POST: periods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(period period)
        {
            if (ModelState.IsValid)
            {
                period.isActive = true;
                db.Entry(period).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(period);
        }

        // POST: periods/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult End(int id)
        {
            period period = db.periods.Find(id);
            period.isActive = false;
            db.Entry(period).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
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
