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
    public class CustomersController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();

        // GET: Customers
        public ActionResult Index(string searching)
        {
            decimal x = 0, y = 0, z = 0;
            var trade = db.Customers.ToList();
            foreach (var item in trade)
            {
                x += Convert.ToDecimal(item.Number * item.NewPricePerOneLa);
                y += Convert.ToDecimal(item.Cash);
                z += Convert.ToDecimal(item.Change);
            }
            ViewBag.hh = x;
            ViewBag.gg = y;
            ViewBag.ff = z;
            return View(db.Customers.Where(h => h.Name.StartsWith(searching) || searching == null).OrderByDescending(a => a.id).ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.cat_Id = new SelectList(db.FreezerCeteogries, "id", "Type");
          //  ViewBag.PeriodId = new SelectList(db.periods.Where(x => x.isActive), "Id", "PeriodName");

            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.Size == "صغير")
                {
                    customer.Change = (customer.Number * db.FreezerCeteogries.Find(customer.Cat_id).PricePerSmall) - customer.Cash;
                    customer.PricePerOneLa = db.FreezerCeteogries.Find(customer.Cat_id).PricePerSmall;
                }
                else
                {
                    customer.Change = (customer.Number * db.FreezerCeteogries.Find(customer.Cat_id).PricePerLarge) - customer.Cash;
                    customer.PricePerOneLa = db.FreezerCeteogries.Find(customer.Cat_id).PricePerLarge;
                }
                customer.Date = DateTime.Now;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cat_Id = new SelectList(db.FreezerCeteogries, "id", "Type", customer.Cat_id);
          //  ViewBag.PeriodId = new SelectList(db.periods.Where(x => x.isActive), "Id", "PeriodName", customer.PeriodId);

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.cat_Id = new SelectList(db.FreezerCeteogries, "id", "Type", customer.Cat_id);
          //  ViewBag.PeriodId = new SelectList(db.periods.Where(x => x.isActive), "Id", "PeriodName", customer.PeriodId);

            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.Size == "صغير")
                {
                    customer.Change = (customer.Number * db.FreezerCeteogries.Find(customer.Cat_id).PricePerSmall) - customer.Cash;
                    customer.NewPricePerOneLa = db.FreezerCeteogries.Find(customer.Cat_id).PricePerSmall;
                }
                else
                {
                    customer.Change = (customer.Number * db.FreezerCeteogries.Find(customer.Cat_id).PricePerLarge) - customer.Cash;
                    customer.PricePerOneLa = db.FreezerCeteogries.Find(customer.Cat_id).PricePerLarge;
                }

                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cat_Id = new SelectList(db.FreezerCeteogries, "id", "Type", customer.Cat_id);
          //  ViewBag.PeriodId = new SelectList(db.periods.Where(x => x.isActive), "Id", "PeriodName", customer.PeriodId);

            return View(customer);
        }
        [HttpGet]
        public ActionResult CashChange(int? id)
        {
            ViewBag.Change = false;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public ActionResult CashChange(int id, decimal reminderChange)
        {
            if (ModelState.IsValid)
            {
                var customer = db.Customers.Find(id);
                if (reminderChange > customer.Change)
                {
                    ViewBag.Change = true;
                    return View();
                }
                customer.Change -= reminderChange;
                customer.Cash += reminderChange;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Change = false;
            return View();
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
