using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DvdStore.Models
{
    public class DvdDetails
    {
        [Key]
        public int DvdId { get; set; }
        [Display(Name = "Movie Name")]
        public String DvdTitle { get; set; }
        [Display(Name = "Movie Description")]
        public String DvdDescription { get; set; }
        [Display(Name = "Total Copies")]
        public int TotalDvdCopies { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; }
        [Display(Name = "Age Restriction")]
        public Boolean AgeRestiriction { get; set; } 

        public String Studio { get; set; }

        public String Producer { get; set; }
        public string Cast { get; set; }

    }
}