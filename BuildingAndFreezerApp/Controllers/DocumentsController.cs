using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BuildingAndFreezerApp.Models;
using System.IO;

namespace BuildingAndFreezerApp.Controllers
{
    [Authorize(Roles = "Admins , Freezer, Building", Users = "hossam50@gmail.com, ayman5383@gmail.com, MangerFreezer@gamil.com, MangerBuilding@gamil.com")]

    public class DocumentsController : Controller
    {
        private BuildingandFreezerEntities db = new BuildingandFreezerEntities();

        // GET: Documents
        public ActionResult Index()
        {
            return View(db.Documents.OrderByDescending(x => x.id).ToList());
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Document document, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string stringPath = Path.Combine(Server.MapPath("~/Files/"), upload.FileName);
                upload.SaveAs(stringPath);
                document.DocPath = upload.FileName;
                db.Documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index", "Documents");
            }

            return View(document);
        }


        public FileResult Download(int id)
        {
            var doc = db.Documents.Find(id);
            string fullPath = Path.Combine(Server.MapPath("~/Files/"), doc.DocPath);
            return File(fullPath, "application/pdf", Path.GetFileName(fullPath));
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Document document, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldPath = Path.Combine(Server.MapPath("~/Files/") + document.DocPath);

                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string newPath = Path.Combine(Server.MapPath("~/Files/") + upload.FileName);
                    upload.SaveAs(newPath);
                    document.DocPath = upload.FileName;
                }


                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            string oldPath = Path.Combine(Server.MapPath("~/Files/") + document.DocPath);
            System.IO.File.Delete(oldPath);
            db.Documents.Remove(document);
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
