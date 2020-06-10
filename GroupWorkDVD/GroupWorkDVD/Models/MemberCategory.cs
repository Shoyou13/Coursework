using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DvdStore.Models
{
    public class MemberCategory
    {
        [Key]
        public int MemberCatID { get; set; }
        public string MemberType{ get; set; }
        public int TotalLoan{ get; set; }
    }
}