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
    public class TradersController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();

        // GET: Traders
        public ActionResult Index(string searching)
        {
            decimal x = 0, y = 0, z = 0;
            var trade = db.Traders.ToList();
            foreach (var item in trade)
            {
                x += Convert.ToDecimal(item.Number * item.PricePerOne);
                y += Convert.ToDecimal(item.Cash);
                z += Convert.ToDecimal(item.Change);
            }
            ViewBag.hh = x;
            ViewBag.gg = y;
            ViewBag.ff = z;
            return View(db.Traders.Where(h => h.Name.StartsWith(searching) || searching == null).OrderByDescending(a => a.id).ToList());
        }

        // GET: Traders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trader trader = db.Traders.Find(id);
            if (trader == null)
            {
                return HttpNotFound();
            }
            return View(trader);
        }

        // GET: Traders/Create
        public ActionResult Create()
        {
            ViewBag.cat_Id = new SelectList(db.FreezerCeteogries, "id", "Type");
            // ViewBag.PeriodId = new SelectList(db.periods.Where(x => x.isActive), "Id", "PeriodName");
            ViewBag.FrID = new SelectList(db.Fridages, "Fr_id", "Fr_Name");

            return View();
        }

        // POST: Traders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trader trader)
        {
            if (ModelState.IsValid)
            {
                trader.Number = (trader.Weight * trader.NumberOfOne) / 1000;
                trader.PricePerOne = db.FreezerCeteogries.Find(trader.cat_Id).PricePerTon;

                trader.Change = (trader.Number * db.FreezerCeteogries.Find(trader.cat_Id).PricePerTon) - trader.Cash;
                trader.Date = DateTime.Now;
                db.Traders.Add(trader);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cat_Id = new SelectList(db.FreezerCeteogries, "id", "Type", trader.cat_Id);
          //  ViewBag.PeriodId = new SelectList(db.periods.Where(x => x.isActive), "Id", "PeriodName", trader.PeriodId);

            return View(trader);
        }

        // GET: Traders/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trader trader = db.Traders.Find(id);
            if (trader == null)
            {
                return HttpNotFound();
            }
            ViewBag.cat_Id = new SelectList(db.FreezerCeteogries, "id", "Type", trader.cat_Id);

            ViewBag.FrID = new SelectList(db.Fridages, "Fr_id", "Fr_Name");
            // ViewBag.PeriodId = new SelectList(db.periods.Where(x => x.isActive), "Id", "PeriodName", trader.PeriodId);


            return View(trader);
        }

        // POST: Traders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Trader trader)
        {
            if (ModelState.IsValid)
            {
                trader.Number = (trader.Weight * trader.NumberOfOne) / 1000;
                trader.PricePerOne = db.FreezerCeteogries.Find(trader.cat_Id).PricePerTon;
                trader.Change = (trader.Number * db.FreezerCeteogries.Find(trader.cat_Id).PricePerTon) - trader.Cash;

                db.Entry(trader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FrID = new SelectList(db.Fridages, "Fr_id", "Fr_Name");
            ViewBag.cat_Id = new SelectList(db.FreezerCeteogries, "id", "Type", trader.cat_Id);
       //     ViewBag.PeriodId = new SelectList(db.periods.Where(x => x.isActive), "Id", "PeriodName", trader.PeriodId);


            return View(trader);
        }
        [HttpGet]
        public ActionResult CashChange(int? id)
        {
            ViewBag.Change = false;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trader trader = db.Traders.Find(id);
            if (trader == null)
            {
                return HttpNotFound();
            }
            return View(trader);
        }
        [HttpPost]
        public ActionResult CashChange(int id, decimal reminderChange)
        {
            if (ModelState.IsValid)
            {
                var trader = db.Traders.Find(id);
                if (reminderChange > trader.Change)
                {
                    ViewBag.Change = true;
                    return View();
                }
                trader.Change -= reminderChange;
                trader.Cash += reminderChange;
                db.Entry(trader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Change = false;
            return View();
        }
        // GET: Traders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trader trader = db.Traders.Find(id);
            if (trader == null)
            {
                return HttpNotFound();
            }
            return View(trader);
        }

        // POST: Traders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trader trader = db.Traders.Find(id);
            db.Traders.Remove(trader);
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
