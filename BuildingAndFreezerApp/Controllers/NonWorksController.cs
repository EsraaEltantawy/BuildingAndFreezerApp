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

    public class NonWorksController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();

        // GET: NonWorks
        public ActionResult Index()
        {
            var nonWorks = db.NonWorks.Include(n => n.Building).Include(n => n.NonWorksTable).OrderByDescending(x => x.id);
            return View(nonWorks.ToList());
        }

        // GET: NonWorks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonWork nonWork = db.NonWorks.Find(id);
            if (nonWork == null)
            {
                return HttpNotFound();
            }
            return View(nonWork);
        }

        // GET: NonWorks/Create
        public ActionResult Create()
        {
            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName");
            ViewBag.NonId = new SelectList(db.NonWorksTables, "Id", "nonName");
            return View();
        }

        // POST: NonWorks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NonWork nonWork)
        {
            var index = db.BankBuildings.Max(x => x.id);
            var bankbuilding = db.BankBuildings.Find(index);
            if (ModelState.IsValid)
            {
                if (bankbuilding.CurrentPrices != null)
                {
                    if (nonWork.Prices > bankbuilding.CurrentPrices)
                    {
                        ViewBag.message = "السعر الذى ادخلته اكبر من الرصيد فى البنك";
                        ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", nonWork.BuildID);
                        ViewBag.NonId = new SelectList(db.NonWorksTables, "Id", "nonName", nonWork.NonId);
                        return View(nonWork);
                    }
                }
                bankbuilding.CurrentPrices -= nonWork.Prices;
                db.Entry(bankbuilding).State = EntityState.Modified;
                nonWork.Data = DateTime.Now;
                db.NonWorks.Add(nonWork);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", nonWork.BuildID);
            ViewBag.NonId = new SelectList(db.NonWorksTables, "Id", "nonName", nonWork.NonId);
            return View(nonWork);
        }

        // GET: NonWorks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonWork nonWork = db.NonWorks.Find(id);
            if (nonWork == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", nonWork.BuildID);
            ViewBag.NonId = new SelectList(db.NonWorksTables, "Id", "nonName", nonWork.NonId);
            Session["price"] = nonWork.Prices;

            return View(nonWork);
        }

        // POST: NonWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NonWork nonWork)
        {
            if (ModelState.IsValid)
            {
                decimal? price = (decimal)Session["price"];
                if (nonWork.Prices != price)
                {
                    var index = db.BankBuildings.Max(x => x.id);
                    var bankbuilding = db.BankBuildings.Find(index);
                    bankbuilding.CurrentPrices += price - nonWork.Prices;
                    if (bankbuilding.CurrentPrices < nonWork.Prices)
                    {
                        ViewBag.message = "السعر الذى ادخلته اكبر من الرصيد فى البنك";
                        ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", nonWork.BuildID);
                        ViewBag.NonId = new SelectList(db.NonWorksTables, "Id", "nonName", nonWork.NonId);
                        return View(nonWork);
                    }
                }
                db.Entry(nonWork).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", nonWork.BuildID);
            ViewBag.NonId = new SelectList(db.NonWorksTables, "Id", "nonName", nonWork.NonId);

            return View(nonWork);
        }

        // GET: NonWorks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonWork nonWork = db.NonWorks.Find(id);
            if (nonWork == null)
            {
                return HttpNotFound();
            }
            return View(nonWork);
        }

        // POST: NonWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NonWork nonWork = db.NonWorks.Find(id);
            db.NonWorks.Remove(nonWork);
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
