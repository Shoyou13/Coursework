using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdStore.Models
{
    public class SearchLoan
    {
        public int SearchLoanId { get; set; }
        public DvdDetails dvd { get; set; }
        public Member member { get; set; }
    }
}