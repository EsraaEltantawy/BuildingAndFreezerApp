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
    public class FridagesController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();

        // GET: Fridages
        public ActionResult Index()
        {
            return View(db.Fridages.ToList());
        }

        // GET: Fridages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fridage fridage = db.Fridages.Find(id);
            if (fridage == null)
            {
                return HttpNotFound();
            }
            return View(fridage);
        }

        // GET: Fridages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fridages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Fr_id,Fr_Name,Fr_NumOfSection")] Fridage fridage)
        {
            if (ModelState.IsValid)
            {
                db.Fridages.Add(fridage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fridage);
        }

        // GET: Fridages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fridage fridage = db.Fridages.Find(id);
            if (fridage == null)
            {
                return HttpNotFound();
            }
            return View(fridage);
        }

        // POST: Fridages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Fr_id,Fr_Name,Fr_NumOfSection")] Fridage fridage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fridage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fridage);
        }

        // GET: Fridages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fridage fridage = db.Fridages.Find(id);
            if (fridage == null)
            {
                return HttpNotFound();
            }
            return View(fridage);
        }

        // POST: Fridages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fridage fridage = db.Fridages.Find(id);
            db.Fridages.Remove(fridage);
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
