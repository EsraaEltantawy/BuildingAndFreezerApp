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
    [Authorize(Roles = "Admins , Freezer", Users = "hossam50@gmail.com, ayman5383@gmail.com, MangerFreezer@gamil.com")]
    public class PaymentsController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();

        // GET: Payments
        public ActionResult Index()
        {
            decimal? totalprice = 0;
            decimal? totalprice2 = 0;
            decimal? totalprice3 = 0;
            decimal? totalprice4 = 0;
            var payment = db.Payments.ToList();
            foreach (var item in payment)
            {
                if (item.Type == "كهرباء")
                {
                    totalprice += item.Price;
                }
                else if (item.Type == "صيانة")
                {
                    totalprice2 += item.Price;

                }
                else if (item.Type == "نقل")
                {
                    totalprice3 += item.Price;

                }
                else if (item.Type == "مرتبات")
                {
                    totalprice4 += item.Price;

                }

            }
            ViewBag.totalprice = totalprice;
            ViewBag.totalprice2 = totalprice2;
            ViewBag.totalprice3 = totalprice3;
            ViewBag.totalprice4 = totalprice4;
            return View(db.Payments.OrderByDescending(x => x.id).ToList());
        }
        public ActionResult Getall()
        {
            var getListPayment = from t in db.Payments
                                 select new
                                 {
                                     t.Date,
                                     t.Type,
                                     t.Details,
                                     t.Price,
                                     t.id
                                 };
            return Json(new { data = getListPayment }, behavior: JsonRequestBehavior.AllowGet);
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Date,Type,Details,Price")] Payment payment)
        {
            var index = db.BankFreezers.Max(x => x.id);
            var bankfreezer = db.BankFreezers.Find(index);


            if (ModelState.IsValid)
            {

                if (bankfreezer.CurrentPrices != null)
                {
                    if (payment.Price > bankfreezer.CurrentPrices)
                    {
                        ViewBag.message = "Invaild Payment";
                        return View(payment);
                    }
                    bankfreezer.CurrentPrices = bankfreezer.CurrentPrices - payment.Price;
                    payment.Date = DateTime.Now;
                    db.Entry(bankfreezer).State = EntityState.Modified;
                    db.Payments.Add(payment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Date,Type,Details,Price")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
