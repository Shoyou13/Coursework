using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdStore.Models
{
    public class DvdViewModel
    {
        public int Id { get; set; }
        public DvdDetails Dvd { get; set; }
        public IList<Producer> Producer { get; set; }
        public IList<CastDetails> Cast { get; set; }
    }
}