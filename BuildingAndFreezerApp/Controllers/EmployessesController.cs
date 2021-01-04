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

    public class EmployessesController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();

        // GET: Employesses
        public ActionResult Index()
        {
            return View(db.Employesses.OrderByDescending(x => x.id).ToList());
        }

        // GET: Employesses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employess employess = db.Employesses.Find(id);
            if (employess == null)
            {
                return HttpNotFound();
            }
            return View(employess);
        }

        // GET: Employesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,empName")] Employess employess)
        {
            if (ModelState.IsValid)
            {
                db.Employesses.Add(employess);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employess);
        }

        // GET: Employesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employess employess = db.Employesses.Find(id);
            if (employess == null)
            {
                return HttpNotFound();
            }
            return View(employess);
        }

        // POST: Employesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,empName")] Employess employess)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employess).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employess);
        }

        // GET: Employesses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employess employess = db.Employesses.Find(id);
            if (employess == null)
            {
                return HttpNotFound();
            }
            return View(employess);
        }

        // POST: Employesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            foreach (var item in db.BuildingEmployees.Where(x => x.empID == id))
            {
                db.BuildingEmployees.Remove(item);
            }
            Employess employess = db.Employesses.Find(id);
            db.Employesses.Remove(employess);
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
