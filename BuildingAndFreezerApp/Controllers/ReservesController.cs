using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BuildingAndFreezerApp.Models;

namespace BuildingAndFreezerApp.Controllers
{
    [Authorize(Roles = "Admins , Building", Users = "hossam50@gmail.com, ayman5383@gmail.com, MangerBuilding@gamil.com")]

    public class ReservesController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();
        BuildingRepositry repo = new BuildingRepositry();

        // GET: Reserves
        public ActionResult Index(string searching)
        {
            var reserves = db.Reserves.Include(r => r.Building).Where(h => h.ClientName.StartsWith(searching) || h.Building.buildName.Contains(searching) || searching == null).OrderByDescending(x => x.id);
            return View(reserves.ToList());
        }

        // عرض تفاصيل العملاء
        public ActionResult IndexReserved()
        {
            List<ClientReserve> lstreserves = new List<ClientReserve>();
            foreach (var item in repo.Reserves)
            {
                var client = db.ClientReserves.Where(x => x.ClientID == item.id && x.Prices == null).FirstOrDefault();
                if (client != null)
                    lstreserves.Add(client);
            }
            repo.ClientReserve = lstreserves.OrderByDescending(x => x.id).ToList();
            repo.Reserves = db.Reserves.Include(r => r.Building).Where(x => x.ChangePrices != 0).OrderByDescending(x => x.id).ToList();
            return View(repo);
        }
        // سداد فواتير العملاء
        [HttpGet]
        public ActionResult RegisterReserve(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientReserve reserve = db.ClientReserves.Find(id);
            if (reserve == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", reserve.BuildID);
            ViewBag.ClientID = new SelectList(db.Reserves, "id", "ClientName", reserve.ClientID);
            return View(reserve);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterReserve(ClientReserve clientReserve)
        {
            if (clientReserve.Prices == null)
            {
                ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", clientReserve.BuildID);
                ViewBag.ClientID = new SelectList(db.Reserves, "id", "ClientName", clientReserve.ClientID);
                return View(clientReserve);
            }

            var clientData = db.Reserves.Find(clientReserve.ClientID);
            clientData.ChangePrices -= clientReserve.Prices;
            clientData.CashPrices += clientReserve.Prices;
            db.Entry(clientData).State = EntityState.Modified;
            db.Entry(clientReserve).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("IndexReserved");
        }
        //GET: Reserves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserve reserve = db.Reserves.Find(id);
            if (reserve == null)
            {
                return HttpNotFound();
            }
            repo.oneClient = reserve;
            repo.ClientReserve = repo.ClientReserve.Where(x => x.ClientID == id);
            return View(repo);
        }

        // GET: Reserves/Create
        public ActionResult Create()
        {
            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName");
            return View();
        }

        // POST: Reserves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reserve reserve, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                int count = countFlat(reserve.BuildID, reserve.FlatNumber);
                if (count > 0)
                {
                    ViewBag.error = "هذه الشقة محجوزة";
                    ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName");
                    return View(reserve);
                }
                // صورة البطاقة
                if (upload != null)
                {
                    string stringPathPhotoClient = Path.Combine(Server.MapPath("~/Images/"), upload.FileName);
                    upload.SaveAs(stringPathPhotoClient);
                    reserve.ClientPhoto = upload.FileName;
                }

                //if (uploadPhoto != null)
                //{
                //    string stringPathPhotoContract = Path.Combine(Server.MapPath("~/Images/"), uploadPhoto.FileName);
                //    uploadPhoto.SaveAs(stringPathPhotoContract);
                //    reserve.Photo = uploadPhoto.FileName;
                //}
                // صورة العقد


                reserve.ChangePrices = reserve.RePrices - reserve.CashPrices;
                reserve.DateFirstPrem = DateTime.Now;
                db.Reserves.Add(reserve);
                int j = 0;
                for (int i = 0; i < reserve.NumPrem; i++)
                {

                    ClientReserve clientReserve = new ClientReserve()
                    {
                        BuildID = reserve.BuildID,
                        FiatNum = reserve.FlatNumber,
                        Date = reserve.DateFirstPrem.Value.AddMonths((int)reserve.Period + j)
                    };
                    db.ClientReserves.Add(clientReserve);
                    j += (int)reserve.Period;
                }
                db.SaveChanges();

                var returnClientData = db.ClientReserves.Where(x => x.ClientID == null);
                foreach (var item in returnClientData)
                {
                    item.ClientID = reserve.id;
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Reserves");
            }


            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", reserve.BuildID);
            return View(reserve);
        }

        // GET: Reserves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserve reserve = db.Reserves.Find(id);
            if (reserve == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", reserve.BuildID);
            return View(reserve);
        }

        // POST: Reserves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reserve reserve, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldPathClientPhoto = null;
                string oldPathPhotoContract = null;
                //صورة البطاقة
                if (reserve.ClientPhoto != null)
                {
                    oldPathClientPhoto = Path.Combine(Server.MapPath("~/Images/") + reserve.ClientPhoto);
                }
                if (reserve.Photo != null)
                {
                    // صورة العقد
                    oldPathPhotoContract = Path.Combine(Server.MapPath("~/Images/") + reserve.Photo);
                }


                var clientdata = db.Reserves.Find(reserve.id);
                if (clientdata.FlatNumber != reserve.FlatNumber)
                {
                    int count = countFlat(reserve.BuildID, reserve.FlatNumber);
                    if (count > 0)
                    {
                        ViewBag.error = "هذه الشقة محجوزة";
                        ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName");
                        return View(reserve);
                    }
                }
                if (upload != null)
                {
                    if (oldPathClientPhoto != null)
                    {
                        System.IO.File.Delete(oldPathClientPhoto);
                    }
                    string newPathClientPhoto = Path.Combine(Server.MapPath("~/Images/") + upload.FileName);
                    upload.SaveAs(newPathClientPhoto);
                    reserve.ClientPhoto = upload.FileName;
                }
                //if (uploadPhoto != null)
                //{
                //    if (oldPathPhotoContract != null)
                //    {
                //        System.IO.File.Delete(oldPathPhotoContract);
                //    }
                //    string newPathPhoto = Path.Combine(Server.MapPath("~/Images/") + uploadPhoto.FileName);
                //    uploadPhoto.SaveAs(newPathPhoto);
                //    reserve.Photo = uploadPhoto.FileName;
                //}


                if (reserve.Period != clientdata.Period || reserve.NumPrem != clientdata.NumPrem)
                {
                    var returnClientData = db.ClientReserves.Where(x => x.ClientID == reserve.id);
                    List<decimal?> lstprices = new List<decimal?>();
                    foreach (var item in returnClientData)
                    {
                        if (item.Prices != null)
                        {
                            lstprices.Add(item.Prices);
                        }

                        db.ClientReserves.Remove(item);
                    }
                    if (reserve.NumPrem <= lstprices.Count() && reserve.ChangePrices != 0)
                    {
                        ViewBag.errorPrice = "لقد قمت بادخال عدد الاقساط مساوى او اقل من عدد الاقساط المدفوعه ولا يمكنك القيام بهذه العمليه";
                        ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", reserve.BuildID);
                        return View(reserve);
                    }
                    int j = 0;
                    for (int i = 0; i < reserve.NumPrem; i++)
                    {
                        if (i >= lstprices.Count())
                            lstprices.Add(null);
                        ClientReserve clientReserve = new ClientReserve()
                        {
                            BuildID = reserve.BuildID,
                            FiatNum = reserve.FlatNumber,
                            ClientID = reserve.id,
                            Prices = lstprices[i],
                            Date = reserve.DateFirstPrem.Value.AddMonths((int)reserve.Period + j)
                        };
                        db.ClientReserves.Add(clientReserve);
                        j += (int)reserve.Period;
                    }
                    db.SaveChanges();


                }
                reserve.ChangePrices = reserve.RePrices - reserve.CashPrices;
                db.Entry(clientdata).CurrentValues.SetValues(reserve);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildID = new SelectList(db.Buildings, "id", "buildName", reserve.BuildID);
            return View(reserve);
        }
        //
        //public ActionResult DetailsOneClient(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    repo.ClientReserve = repo.ClientReserve.Where(x => x.ClientID == id);
        //    return View(repo);
        //}
        // GET: Reserves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserve reserve = db.Reserves.Find(id);
            if (reserve == null)
            {
                return HttpNotFound();
            }
            return View(reserve);
        }

        // POST: Reserves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            foreach (var item in db.ClientReserves.Where(x => x.ClientID == id))
            {
                db.ClientReserves.Remove(item);
            }
            Reserve reserve = db.Reserves.Find(id);
            db.Reserves.Remove(reserve);
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
        public int countFlat(int? buildingId, int? flatnum) => db.Reserves.Where(x => x.BuildID == buildingId && x.FlatNumber == flatnum).Count();


    }
}
