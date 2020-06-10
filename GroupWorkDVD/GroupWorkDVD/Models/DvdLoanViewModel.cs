using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdStore.Models
{
    public class DvdLoanViewModel
    {
        public string DvdTitle { get; set; }
        public DateTime dateout { get; set; }
        public DateTime datedue { get; set; }
        public string MemberName { get; set;}
        public DateTime? returnDate { get; set; }
        public IList<DvdDetails> dvdDetails { get; set; }
    }
}