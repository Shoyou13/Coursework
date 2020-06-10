using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DvdStore.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        public int LoanTypeId { get; set; }
        public virtual LoanType LoanType { get; set; }
        public int DvdId { get; set; }
        public virtual DvdDetails DvdDetails { get; set; }

        [Display(Name = "Taken Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TakenDate { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReturnDate { get; set; }
        [Display(Name = "Standard Charge")]
        public int StandardCharge { get; set; }
    }
}