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

    public class MatrielsController : Controller
    {

        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();

        // GET: Matriels
        public ActionResult Index()
        {
            return View(db.Matriels.OrderByDescending(x => x.id).ToList());
        }

        // GET: Matriels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriel matriel = db.Matriels.Find(id);
            if (matriel == null)
            {
                return HttpNotFound();
            }
            return View(matriel);
        }

        // GET: Matriels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Matriels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,matName")] Matriel matriel)
        {
            if (ModelState.IsValid)
            {
                db.Matriels.Add(matriel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(matriel);
        }

        // GET: Matriels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriel matriel = db.Matriels.Find(id);
            if (matriel == null)
            {
                return HttpNotFound();
            }
            return View(matriel);
        }

        // POST: Matriels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Matriel matriel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matriel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(matriel);
        }

        // GET: Matriels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriel matriel = db.Matriels.Find(id);
            if (matriel == null)
            {
                return HttpNotFound();
            }
            return View(matriel);
        }

        // POST: Matriels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            foreach (var item in db.BuildingMatrials.Where(x => x.MatID == id))
            {
                db.BuildingMatrials.Remove(item);
            }
            Matriel matriel = db.Matriels.Find(id);
            db.Matriels.Remove(matriel);
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
