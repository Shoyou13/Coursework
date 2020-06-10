using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DvdStore.Models
{
    public class CastDetails
    {
        [Key]
        public int CastId { get; set; }
        [Display(Name = "First Name")]
        public string ActorFname { get; set; }
        [Display(Name = "Last Name")]
        public string ActorLname { get; set; }
    }
}