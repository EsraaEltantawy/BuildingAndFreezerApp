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

    public class BuildingsController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();

        // GET: Buildings
        public ActionResult Index()
        {
            return View(db.Buildings.OrderByDescending(x => x.id).ToList());
        }

        // GET: Buildings/Details/5
        public ActionResult Details(int? id)
        {

            BuildingRepositry br = new BuildingRepositry();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            br.Buildings = db.Buildings.Where(x => x.id == id);
            br.Reserves = db.Reserves.Where(x => x.BuildID == id);
            br.BuildingMatrial = db.BuildingMatrials.Where(x => x.BuildID == id);
            br.BuildingEmployee = db.BuildingEmployees.Where(x => x.BuildID == id);
            br.NonWorks = db.NonWorks.Where(x => x.BuildID == id);

            return View(br);
        }

        // GET: Buildings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,buildName,NamberHouses")] Building building)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Check = true;
                int Count = DuplicateName(building);
                if (Count > 0)
                {
                    ViewBag.titleError = $"This {building.buildName} is Already Exists  ";
                    ViewBag.Check = false;
                    return View(building);
                }
                db.Buildings.Add(building);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(building);
        }

        // GET: Buildings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,buildName,NamberHouses")] Building building)
        {
            if (ModelState.IsValid)
            {
                db.Entry(building).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(building);
        }

        // GET: Buildings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Building building = db.Buildings.Find(id);
            foreach (var item in db.BuildingEmployees.Where(x => x.BuildID == id))
            {
                db.BuildingEmployees.Remove(item);
            }
            foreach (var item in db.BuildingMatrials.Where(x => x.BuildID == id))
            {
                db.BuildingMatrials.Remove(item);
            }
            foreach (var item in db.NonWorks.Where(x => x.BuildID == id))
            {
                db.NonWorks.Remove(item);
            }
            foreach (var item in db.Reserves.Where(x => x.BuildID == id))
            {
                db.Reserves.Remove(item);
            }
            foreach (var item in db.ClientReserves.Where(x => x.BuildID == id))
            {
                db.ClientReserves.Remove(item);
            }

            db.Buildings.Remove(building);
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
        public int DuplicateName(Building build)
        {
            List<Building> checkOfName = (from d in db.Buildings where d.buildName == build.buildName select d).ToList();
            return checkOfName.Count;
        }
    }
}
