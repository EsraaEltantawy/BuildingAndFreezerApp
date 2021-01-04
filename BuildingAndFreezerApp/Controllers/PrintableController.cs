using BuildingAndFreezerApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BuildingAndFreezerApp.Controllers
{
    [Authorize(Roles = "Admins", Users = "hossam50@gmail.com, ayman5383@gmail.com")]
    public class PrintableController : Controller
    {
        BuildingandFreezerEntities db = new BuildingandFreezerEntities();
        BuildingRepositry repositry = new BuildingRepositry();
        // GET: ReportsBuilding
        public ActionResult Index()
        {
            return View(repositry.Buildings);
        }
        public ActionResult DetailsBuilding(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repositry.Reserves = db.Reserves.Where(x => x.BuildID == id);
            repositry.buildId = (int)id;
            return View(repositry);
        }
        public ActionResult DetailsOneClient(int? id, int buildId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repositry.ClientReserve = repositry.ClientReserve.Where(x => x.BuildID == buildId && x.ClientID == id);
            return View(repositry);
        }
        public ActionResult DetailsMatrial(int? id)
        {
            List<decimal?> sumMatrial = new List<decimal?>();
            List<decimal?> NumMatrial = new List<decimal?>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (var item in repositry.Matriels)
            {
                sumMatrial.Add(repositry.BuildingMatrial.Where(x => x.MatID == item.id && x.BuildID == id).Sum(x => x.Prices));
            }
            foreach (var item in repositry.Matriels)
            {
                NumMatrial.Add(repositry.BuildingMatrial.Where(x => x.MatID == item.id && x.BuildID == id).Sum(x => x.MumOfMat));
            }
            ViewBag.sumMatrial = sumMatrial;
            ViewBag.sum = sumMatrial.Sum();
            ViewBag.NumMatrial = NumMatrial;
            repositry.buildId = (int)id;

            return View(repositry);
        }
        public ActionResult DetailsEmployees(int? id)
        {
            List<decimal?> sumEmployee = new List<decimal?>();
            List<BuildingEmployee> Employees = new List<BuildingEmployee>();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (var item in repositry.Employess)
            {
                var emp = repositry.BuildingEmployee.Where(x => x.BuildID == id && x.empID == item.id);
                sumEmployee.Add(emp.Sum(x => x.Price));
                Employees.Add(emp.FirstOrDefault());
            }
            repositry.BuildingEmployee = Employees;
            repositry.sumEmployee = sumEmployee;
            ViewBag.sum = sumEmployee.Sum();
            repositry.buildId = (int)id;
            return View(repositry);
        }

        public ActionResult DetailsNonWorks(int? id)
        {
            List<decimal?> sumNonWorks = new List<decimal?>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (var item in repositry.NonWorksTables)
            {
                sumNonWorks.Add(repositry.NonWorks.Where(x => x.NonId == item.Id && x.BuildID == id).Sum(x => x.Prices));
            }
            ViewBag.sumNonWorks = sumNonWorks;
            ViewBag.sum = sumNonWorks.Sum();
            repositry.buildId = (int)id;
            return View(repositry);
        }

        public ActionResult Profit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sum = db.BuildingEmployees.Where(x => x.BuildID == id).Sum(x => x.Price) +
                                   db.NonWorks.Where(x => x.BuildID == id).Sum(x => x.Prices) +
                                   db.BuildingMatrials.Where(x => x.BuildID == id).Sum(x => x.Prices);
            ViewBag.SumoutPrices = sum == null ? 0 : sum;
            ViewBag.sumClientReserve = db.Reserves.Where(x => x.BuildID == id).Sum(x => x.RePrices - x.Comission) == null ? 0 : db.Reserves.Where(x => x.BuildID == id).Sum(x => x.RePrices - x.Comission);
            return View();

        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientReserve client = db.ClientReserves.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientReserve client = db.ClientReserves.Find(id);
            ClientReserve clientReserves = db.ClientReserves.Where(x => x.ClientID == client.ClientID && x.Prices == null).FirstOrDefault();
            db.ClientReserves.Remove(clientReserves);

            Reserve reserve = db.Reserves.Find(client.ClientID);
            reserve.CashPrices -= client.Prices;
            reserve.ChangePrices += client.Prices;
            db.Entry(reserve).State = EntityState.Modified;
            client.Prices = null;
            db.Entry(client).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AllOutPrices()
        {

            return View(repositry);
        }

    }
}