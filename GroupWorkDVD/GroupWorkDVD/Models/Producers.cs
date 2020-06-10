using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DvdStore.Models
{
    public class Producer
    {
        [Key]
        public int ProducerId { get; set; }
        [Display(Name = "First Name")]
        public string ProducerFname { get; set; }
        [Display(Name = "Last Name")]
        public string ProducerLname { get; set; }
    }
}