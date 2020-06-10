using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DvdStore.Models;

namespace DvdStore.Controllers
{
    public class DvdDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DvdDetails
        public ActionResult Index()
        {
            return View(db.DvdDetails.ToList());
        }

        // GET: DvdDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DvdDetails dvdDetails = db.DvdDetails.Find(id);
            if (dvdDetails == null)
            {
                return HttpNotFound();
            }
            return View(dvdDetails);
        }

        // GET: DvdDetails/Create
        public ActionResult Create()
        {
            IList<Producer> producer = db.Producers.ToList();
            IList<CastDetails> cast = db.CastDetails.ToList();
            DvdViewModel dvdviewmodel = new DvdViewModel()
            {
                Producer = producer,
                Cast = cast
            };
            return View(dvdviewmodel);
        }

        // POST: DvdDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DvdId,DvdTitle,DvdDescription,TotalDvdCopies,ReleaseDate,DateAdded,AgeRestiriction,Studio,Producer,Cast")] DvdDetails dvdDetails)
        {
            if (ModelState.IsValid)
            {

                db.DvdDetails.Add(dvdDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dvdDetails);
        }

        // GET: DvdDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DvdDetails dvdDetails = db.DvdDetails.Find(id);
            if (dvdDetails == null)
            {
                return HttpNotFound();
            }
            return View(dvdDetails);
        }

        // POST: DvdDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DvdId,DvdTitle,DvdDescription,TotalDvdCopies,ReleaseDate,DateAdded,AgeRestiriction,Studio,Producer,Cast")] DvdDetails dvdDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dvdDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dvdDetails);
        }

        // GET: DvdDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DvdDetails dvdDetails = db.DvdDetails.Find(id);
            if (dvdDetails == null)
            {
                return HttpNotFound();
            }
            return View(dvdDetails);
        }

        // POST: DvdDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DvdDetails dvdDetails = db.DvdDetails.Find(id);
            db.DvdDetails.Remove(dvdDetails);
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
