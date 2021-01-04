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

    public class BuildingMatrialsController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();

        // GET: BuildingMatrials
        public ActionResult Index()
        {
            var buildingMatrials = db.BuildingMatrials.Include(b => b.Building).Include(b => b.Matriel).OrderByDescending(x => x.id);
            return View(buildingMatrials.ToList());
        }

        // GET: BuildingMatrials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingMatrial buildingMatrial = db.BuildingMatrials.Find(id);
            if (buildingMatrial == null)
            {
                return HttpNotFound();
            }
            return View(buildingMatrial);
        }

        // GET: BuildingMatrials/Create
        public ActionResult Create()
        {
            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName");
            ViewBag.MatID = new SelectList(db.Matriels, "id", "matName");
            return View();
        }

        // POST: BuildingMatrials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BuildingMatrial buildingMatrial)
        {
            var index = db.BankBuildings.Max(x => x.id);
            var bankbuilding = db.BankBuildings.Find(index);
            if (ModelState.IsValid)
            {
                if (bankbuilding.CurrentPrices != null)
                {
                    if (buildingMatrial.Prices > bankbuilding.CurrentPrices)
                    {
                        ViewBag.message = "السعر الذى ادخلته اكبر من الرصيد فى البنك";
                        ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", buildingMatrial.BuildID);
                        ViewBag.MatID = new SelectList(db.Matriels, "id", "matName", buildingMatrial.MatID);
                        return View(buildingMatrial);
                    }
                }
                buildingMatrial.Prices = buildingMatrial.PricePerOne * buildingMatrial.MumOfMat;

                bankbuilding.CurrentPrices -= buildingMatrial.Prices;
                db.Entry(bankbuilding).State = EntityState.Modified;
                buildingMatrial.Date = DateTime.Now;

                db.BuildingMatrials.Add(buildingMatrial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", buildingMatrial.BuildID);
            ViewBag.MatID = new SelectList(db.Matriels, "id", "matName", buildingMatrial.MatID);
            return View(buildingMatrial);
        }

        // GET: BuildingMatrials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingMatrial buildingMatrial = db.BuildingMatrials.Find(id);
            if (buildingMatrial == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", buildingMatrial.BuildID);
            ViewBag.MatID = new SelectList(db.Matriels, "id", "matName", buildingMatrial.MatID);
            return View(buildingMatrial);
        }

        // POST: BuildingMatrials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BuildingMatrial buildingMatrial)
        {
            var index = db.BankBuildings.Max(x => x.id);
            var bankbuilding = db.BankBuildings.Find(index);
            if (ModelState.IsValid)
            {
                var newprice = buildingMatrial.PricePerOne * buildingMatrial.MumOfMat;


                if (buildingMatrial.Prices != newprice)
                {
                    bankbuilding.CurrentPrices += buildingMatrial.Prices;

                    if (newprice > bankbuilding.CurrentPrices)
                    {
                        ViewBag.message = "السعر الذى ادخلته اكبر من الرصيد فى البنك";
                        ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", buildingMatrial.BuildID);
                        ViewBag.MatID = new SelectList(db.Matriels, "id", "matName", buildingMatrial.MatID);
                        return View(buildingMatrial);
                    }
                    bankbuilding.CurrentPrices -= newprice;
                    db.Entry(bankbuilding).State = EntityState.Modified;
                }
                buildingMatrial.Prices = newprice;
                db.Entry(buildingMatrial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", buildingMatrial.BuildID);
            ViewBag.MatID = new SelectList(db.Matriels, "id", "matName", buildingMatrial.MatID);
            return View(buildingMatrial);
        }

        // GET: BuildingMatrials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingMatrial buildingMatrial = db.BuildingMatrials.Find(id);
            if (buildingMatrial == null)
            {
                return HttpNotFound();
            }
            return View(buildingMatrial);
        }

        // POST: BuildingMatrials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            BuildingMatrial buildingMatrial = db.BuildingMatrials.Find(id);
            db.BuildingMatrials.Remove(buildingMatrial);
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
