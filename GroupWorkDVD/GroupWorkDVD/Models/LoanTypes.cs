using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DvdStore.Models
{
    public class LoanType
    {
        [Key]
        public int LoanTypeId { get; set; }
        public string LoanCategory { get; set; }
    }
}