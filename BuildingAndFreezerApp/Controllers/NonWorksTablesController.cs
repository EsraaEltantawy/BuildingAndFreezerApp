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

    public class NonWorksTablesController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();

        // GET: NonWorksTables
        public ActionResult Index()
        {
            return View(db.NonWorksTables.OrderByDescending(x => x.Id).ToList());
        }

        // GET: NonWorksTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonWorksTable nonWorksTable = db.NonWorksTables.Find(id);
            if (nonWorksTable == null)
            {
                return HttpNotFound();
            }
            return View(nonWorksTable);
        }

        // GET: NonWorksTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NonWorksTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nonName")] NonWorksTable nonWorksTable)
        {
            if (ModelState.IsValid)
            {
                db.NonWorksTables.Add(nonWorksTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nonWorksTable);
        }

        // GET: NonWorksTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonWorksTable nonWorksTable = db.NonWorksTables.Find(id);
            if (nonWorksTable == null)
            {
                return HttpNotFound();
            }
            return View(nonWorksTable);
        }

        // POST: NonWorksTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nonName")] NonWorksTable nonWorksTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nonWorksTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nonWorksTable);
        }

        // GET: NonWorksTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonWorksTable nonWorksTable = db.NonWorksTables.Find(id);
            if (nonWorksTable == null)
            {
                return HttpNotFound();
            }
            return View(nonWorksTable);
        }

        // POST: NonWorksTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            foreach (var item in db.NonWorks.Where(x => x.NonId == id))
            {
                db.NonWorks.Remove(item);
            }
            NonWorksTable nonWorksTable = db.NonWorksTables.Find(id);
            db.NonWorksTables.Remove(nonWorksTable);
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
