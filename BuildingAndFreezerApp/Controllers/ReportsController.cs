using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildingAndFreezerApp.Models;

namespace BuildingAndFreezerApp.Controllers
{
    [Authorize(Roles = "Admins", Users = "hossam50@gmail.com, ayman5383@gmail.com")]
    public class ReportsController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();
        // GET: Reports
        public ActionResult Index(string name)
        {
            FreezerRepositry free = new FreezerRepositry();
            free.Customer = db.Customers.Where(x => x.Section.ToLower() == name.ToLower());
            free.Trader = db.Traders.Where(x => x.Section.ToLower() == name.ToLower());
            return View(free);
        }
        public ActionResult FinalReport()
        {
            ViewBag.one = db.Traders.Where(x => x.Section.ToLower() == "عنبر امامى فوق").Sum(x => x.Number);
            ViewBag.two = db.Traders.Where(x => x.Section.ToLower() == "عنبر امامى تحت").Sum(x => x.Number);
            ViewBag.three = db.Traders.Where(x => x.Section.ToLower() == "عنبر اوسط فوق").Sum(x => x.Number);
            ViewBag.four = db.Traders.Where(x => x.Section.ToLower() == "عنبر اوسط تحت").Sum(x => x.Number);
            ViewBag.five = db.Traders.Where(x => x.Section.ToLower() == "عنبر خلفى فوق").Sum(x => x.Number);
            ViewBag.six = db.Traders.Where(x => x.Section.ToLower() == "عنبر خلفى تحت").Sum(x => x.Number);

            return View();
        }
    }
}