using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdStore.Models
{
    public class LoanOrderByDate
    {
        public DateTime LoanDate { get; set; }
        public IList<Function11> functions { get; set; }
        public int totalLoan { get; set; }
    }
}