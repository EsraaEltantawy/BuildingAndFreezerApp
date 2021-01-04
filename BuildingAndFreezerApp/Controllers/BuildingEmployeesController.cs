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

    public class BuildingEmployeesController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();

        // GET: BuildingEmployees
        public ActionResult Index()
        {
            var buildingEmployees = db.BuildingEmployees.Include(b => b.Building).Include(b => b.Employess).OrderByDescending(x => x.id);
            return View(buildingEmployees.ToList());
        }

        // GET: BuildingEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingEmployee buildingEmployee = db.BuildingEmployees.Find(id);
            if (buildingEmployee == null)
            {
                return HttpNotFound();
            }
            return View(buildingEmployee);
        }

        // GET: BuildingEmployees/Create
        public ActionResult Create()
        {
            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName");
            ViewBag.empID = new SelectList(db.Employesses, "id", "empName");
            return View();
        }

        // POST: BuildingEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BuildingEmployee buildingEmployee)
        {
            var index = db.BankBuildings.Max(x => x.id);
            var bankbuilding = db.BankBuildings.Find(index);
            if (ModelState.IsValid)
            {
                if (bankbuilding.CurrentPrices != null)
                {
                    if (buildingEmployee.Price > bankbuilding.CurrentPrices)
                    {
                        ViewBag.message = "السعر الذى ادخلته اكبر من الرصيد فى البنك";
                        ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", buildingEmployee.BuildID);
                        ViewBag.empID = new SelectList(db.Employesses, "id", "empName", buildingEmployee.empID);
                        return View(buildingEmployee);
                    }
                }
                bankbuilding.CurrentPrices = bankbuilding.CurrentPrices - buildingEmployee.Price;
                buildingEmployee.Date = DateTime.Now;
                db.Entry(bankbuilding).State = EntityState.Modified;
                db.BuildingEmployees.Add(buildingEmployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", buildingEmployee.BuildID);
            ViewBag.empID = new SelectList(db.Employesses, "id", "empName", buildingEmployee.empID);
            return View(buildingEmployee);
        }

        // GET: BuildingEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingEmployee buildingEmployee = db.BuildingEmployees.Find(id);
            if (buildingEmployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", buildingEmployee.BuildID);
            ViewBag.empID = new SelectList(db.Employesses, "id", "empName", buildingEmployee.empID);
            Session["price"] = buildingEmployee.Price;
            return View(buildingEmployee);
        }

        // POST: BuildingEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BuildingEmployee buildingEmployee)
        {
            if (ModelState.IsValid)
            {
                decimal? price = (decimal)Session["price"];
                if (buildingEmployee.Price != price)
                {
                    var index = db.BankBuildings.Max(x => x.id);
                    var bankbuilding = db.BankBuildings.Find(index);
                    bankbuilding.CurrentPrices += price - buildingEmployee.Price;
                    if (bankbuilding.CurrentPrices < buildingEmployee.Price)
                    {
                        ViewBag.message = "السعر الذى ادخلته اكبر من الرصيد فى البنك";
                        ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", buildingEmployee.BuildID);
                        ViewBag.empID = new SelectList(db.Employesses, "id", "empName", buildingEmployee.empID);
                        return View(buildingEmployee);
                    }
                }
                db.Entry(buildingEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", buildingEmployee.BuildID);
            ViewBag.empID = new SelectList(db.Employesses, "id", "empName", buildingEmployee.empID);
            return View(buildingEmployee);
        }

        // GET: BuildingEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingEmployee buildingEmployee = db.BuildingEmployees.Find(id);
            if (buildingEmployee == null)
            {
                return HttpNotFound();
            }
            return View(buildingEmployee);
        }

        // POST: BuildingEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BuildingEmployee buildingEmployee = db.BuildingEmployees.Find(id);
            db.BuildingEmployees.Remove(buildingEmployee);
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
