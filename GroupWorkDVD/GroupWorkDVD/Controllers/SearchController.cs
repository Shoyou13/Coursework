using DvdStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DvdStore.Controllers
{
    public class SearchController : Controller

    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Search Fnction 1
        public ActionResult Index()
        {

            return View(db.DvdDetails.ToList());
        }
        [HttpPost]
        public ActionResult Index(string lastName = "None", string lastNameFilter = "None")
        {
            IList<DvdDetails> dvdDetails = (from d in db.DvdDetails
                                            where d.Cast.Contains(lastName)
                                            select d).ToList();
            return View(dvdDetails);
        }
        // GET: Search Fnction 2
        public ActionResult SearchLoan()
        {

            return View(db.DvdDetails.ToList());
        }
        [HttpPost]
        public ActionResult SearchLoan(string lastName = "None", string lastNameFilter = "None")
        {
            IList<DvdDetails> dvdDetails = (from d in db.DvdDetails
                                            join e in db.Loans on d.DvdId equals e.DvdId
                                            where d.Cast.Contains(lastName)
                                            orderby d.DateAdded
                                            select d).ToList();
            return View(dvdDetails);

        }
        // GET: Search Fnction 3
        public ActionResult SearchMember()
        {

            return View(db.Loans.ToList());
        }
        [HttpPost]
        public ActionResult SearchMember(string lastName = "None", string lastNameFilter = "None")
        {
            IList<Loan> loans = (from d in db.Loans
                                 join l in db.Members on d.MemberId equals l.MemberId
                                 where l.LastName.Contains(lastName)
                                 select d).ToList();
            return View(loans);
        }
        // GET: Search Fnction 4
        public ActionResult ListDvd()
        {
            IList<DvdDetails> dvdDetails = (from d in db.DvdDetails
                                            orderby d.ReleaseDate
                                            select d).ToList();
            return View(dvdDetails);
        }
        //Function 5
        public ActionResult DvdLoanFunction()
        {
            DvdLoanViewModel dvdLoanView = new DvdLoanViewModel()
            {
                dvdDetails = db.DvdDetails.ToList()
            };

            return View(dvdLoanView);
        }
        [HttpPost]
        public ActionResult DvdLoanFunction(int dvdId)
        {

            DvdLoanViewModel dvdLoanView = (from d in db.DvdDetails
                                            join l in db.Loans on d.DvdId equals l.DvdId
                                            join m in db.Members on l.MemberId equals m.MemberId
                                            select new DvdLoanViewModel { DvdTitle = d.DvdTitle, dateout = l.TakenDate, datedue = l.DueDate, MemberName = m.FirstName + " " + m.LastName, returnDate = l.ReturnDate, dvdDetails = db.DvdDetails.ToList() }

             ).ToList().Last();
            return View(dvdLoanView);
        }

        //Function 8
        public ActionResult MemberLoanFunction()
        {
            IList<MemberLoanFunction> memberLoanFunction = (from d in db.Loans
                                                     join l in db.Members on d.MemberId equals l.MemberId
                                                     join m in db.MemberCategories on l.MemberCatID equals m.MemberCatID
                                                     join e in db.DvdDetails on d.DvdId equals e.DvdId
                                                     select new MemberLoanFunction { MemberName = l.FirstName + " " + l.LastName, DateOfBirth = l.DateOfBirth, Address = l.Address, Email = l.Email, Phone = l.Phone, MemberCategory = m.MemberType, TotalLoans = m.TotalLoan, dvdTitle = e.DvdTitle }
                                  ).ToList();


            return View(memberLoanFunction);


        }
        //Function 10.
        public ActionResult OldDvdList()
        {
            List<DvdDetails> dvds = new List<DvdDetails>();
            DateTime date = DateTime.Now.AddDays(-365);
            IList<DvdDetails> dvd1 = (from d in db.DvdDetails
                                      where d.DateAdded < date
                                      select d).ToList();
            foreach (var dvd in dvd1) {
                IList<Loan> laons=(from l in db.Loans
                 where l.DvdId == dvd.DvdId
                 select l).ToList();
                if (laons.Count == 0)
                {
                    dvds.Add(dvd);
                }
                else {
                    foreach (var l in laons)
                    {
                        if (l.ReturnDate != null)
                        {
                            dvds.Add(dvd);
                        }

                    }
                }
                

            }
            
            return View(dvds);
        }
        //Function11
        public ActionResult FunctionEleven()
        {
            List<LoanOrderByDate> orderByDates = new List<LoanOrderByDate>();
            IList<DateTime> date = (from l in db.Loans
                                    where l.ReturnDate == null
                                    orderby l.TakenDate
                                    select l.TakenDate).Distinct().ToList();
            foreach (var d in date) {
                IList<Function11> functions=(from dvd in db.DvdDetails
                 join l in db.Loans on dvd.DvdId equals l.DvdId
                 join m in db.Members on l.MemberId equals m.MemberId
                 orderby dvd.DvdTitle
                 where l.TakenDate == d
                 select new Function11 { dvdTitle= dvd.DvdTitle,memberName=m.FirstName+ " "+m.LastName }).ToList();
                LoanOrderByDate loanOrderBy = new LoanOrderByDate()
                {
                    LoanDate = d,
                    totalLoan = functions.Count(),
                    functions = functions

                };
                orderByDates.Add(loanOrderBy);
            }


        return View(orderByDates);


        }
        //function12
        public ActionResult NotBorrowed()
        {
            List<functionTwelve> functionTwelves = new List<functionTwelve>();
            foreach (var member in db.Members.ToList()) {
                IList<Loan> loans = (from m in db.Loans
                              where m.MemberId == member.MemberId
                              select m).ToList();
                if (loans.Count() > 0) {
                    if (loans.Last().TakenDate<DateTime.Today.AddDays(-31)) {
                        int last = loans.Last().DvdId;
                        string title = (from m in db.DvdDetails
                                        where m.DvdId == last
                                        select m.DvdTitle).FirstOrDefault().ToString();
                        functionTwelve functionTwelve = new functionTwelve()
                        {
                            FirstName = member.FirstName,
                            LastName = member.LastName,
                            Days = (int)(DateTime.Today - loans.Last().TakenDate).TotalDays,
                            Title = title
                        };
                        functionTwelves.Add(functionTwelve);
                    }
                }

            }
            
            
            return View(functionTwelves);

        }



    }

}