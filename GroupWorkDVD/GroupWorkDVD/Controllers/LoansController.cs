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
    public class LoansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Loans
        public ActionResult Index()
        {
            var loans = db.Loans.Include(l => l.DvdDetails).Include(l => l.LoanType).Include(l => l.Member);
            return View(loans.ToList());
        }
        [HttpPost]
        public ActionResult Index(int DvdId)
        {
            
            Loan loan = (from l in db.Loans
                         where l.DvdId == DvdId
                         select l).ToList().FirstOrDefault(); 
            DateTime date = DateTime.Now;
            if (loan.ReturnDate < DateTime.Now) {
                int days = (int)(date - loan.DueDate).TotalDays;
                int fine = 8*days;
                TempData["FineMessage"] = "You have exceeded the due date by " + days + " Your fine is " + fine;
                return View(db.Loans.ToList());
            }
            loan.ReturnDate = DateTime.Now;
            db.Entry(loan).State = EntityState.Modified;
            db.SaveChanges();
            return View(db.Loans.ToList());
        }

        // GET: Loans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: Loans/Create
        public ActionResult Create()
        {
            ViewBag.DvdId = new SelectList(db.DvdDetails, "DvdId", "DvdTitle");
            ViewBag.LoanTypeId = new SelectList(db.LoanTypes, "LoanTypeId", "LoanCategory");
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "FirstName");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoanId,MemberId,LoanTypeId,DvdId,TakenDate,DueDate,ReturnDate,StandardCharge")] Loan loan)
        {
            int age = (int)(DateTime.Now - db.Members.Find(loan.MemberId).DateOfBirth).TotalDays;
            if (ModelState.IsValid)
            {
                ViewBag.DvdId = new SelectList(db.DvdDetails, "DvdId", "DvdTitle");
                ViewBag.LoanTypeId = new SelectList(db.LoanTypes, "LoanTypeId", "LoanCategory");
                ViewBag.MemberId = new SelectList(db.Members, "MemberId", "FirstName");
                if (db.DvdDetails.Find(loan.DvdId).AgeRestiriction==true) {
                    if (age < 18 * 365)
                    {
                        TempData["AgeError"] = "Sorry You are Small.";
                        return View();
                    }
                }
                
                
                db.Loans.Add(loan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DvdId = new SelectList(db.DvdDetails, "DvdId", "DvdTitle", loan.DvdId);
            ViewBag.LoanTypeId = new SelectList(db.LoanTypes, "LoanTypeId", "LoanCategory", loan.LoanTypeId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "FirstName", loan.MemberId);
            return View(loan);
        }

        // GET: Loans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            ViewBag.DvdId = new SelectList(db.DvdDetails, "DvdId", "DvdTitle", loan.DvdId);
            ViewBag.LoanTypeId = new SelectList(db.LoanTypes, "LoanTypeId", "LoanCategory", loan.LoanTypeId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "FirstName", loan.MemberId);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoanId,MemberId,LoanTypeId,DvdId,TakenDate,DueDate,ReturnDate,StandardCharge")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DvdId = new SelectList(db.DvdDetails, "DvdId", "DvdTitle", loan.DvdId);
            ViewBag.LoanTypeId = new SelectList(db.LoanTypes, "LoanTypeId", "LoanCategory", loan.LoanTypeId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "FirstName", loan.MemberId);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = db.Loans.Find(id);
            db.Loans.Remove(loan);
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
