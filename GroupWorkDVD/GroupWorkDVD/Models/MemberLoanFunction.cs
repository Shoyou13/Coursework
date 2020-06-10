using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DvdStore.Models
{
    public class MemberLoanFunction
    {
        public string MemberName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MemberCategory { get; set; }
        public int TotalLoans { get; set; }
        public string dvdTitle { get; set; }
    }
}