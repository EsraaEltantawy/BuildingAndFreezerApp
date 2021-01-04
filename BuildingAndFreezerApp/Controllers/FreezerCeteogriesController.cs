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
    public class FreezerCeteogriesController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();

        // GET: FreezerCeteogries
        public ActionResult Index()
        {
            return View(db.FreezerCeteogries.ToList());
        }

        // GET: FreezerCeteogries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreezerCeteogry freezerCeteogry = db.FreezerCeteogries.Find(id);
            if (freezerCeteogry == null)
            {
                return HttpNotFound();
            }
            return View(freezerCeteogry);
        }

        // GET: FreezerCeteogries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FreezerCeteogries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FreezerCeteogry freezerCeteogry)
        {
            if (ModelState.IsValid)
            {
                db.FreezerCeteogries.Add(freezerCeteogry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(freezerCeteogry);
        }

        // GET: FreezerCeteogries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreezerCeteogry freezerCeteogry = db.FreezerCeteogries.Find(id);
            if (freezerCeteogry == null)
            {
                return HttpNotFound();
            }
            return View(freezerCeteogry);
        }

        // POST: FreezerCeteogries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FreezerCeteogry freezerCeteogry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(freezerCeteogry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(freezerCeteogry);
        }

        // GET: FreezerCeteogries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreezerCeteogry freezerCeteogry = db.FreezerCeteogries.Find(id);
            if (freezerCeteogry == null)
            {
                return HttpNotFound();
            }
            return View(freezerCeteogry);
        }

        // POST: FreezerCeteogries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            foreach (var item in db.Customers.Where(x => x.Cat_id == id))
            {
                db.Customers.Remove(item);
            }
            foreach (var item in db.Traders.Where(x => x.cat_Id == id))
            {
                db.Traders.Remove(item);
            }
            FreezerCeteogry freezerCeteogry = db.FreezerCeteogries.Find(id);
            db.FreezerCeteogries.Remove(freezerCeteogry);
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
