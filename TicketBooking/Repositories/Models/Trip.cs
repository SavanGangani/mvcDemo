using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Models
{
    public class Trip
    {
        public int c_tripid { get; set; }
        public string c_trip { get; set; }
        public int c_tripprice { get; set; }
        public int c_triptotalticket { get; set; }
        public int c_tripcurrentticket { get; set; }
        public int c_tripqty { get; set; }
        public DateTime c_tripdate { get; set; }
        public int c_totalcost { get; set; }
        public int userid { get; set; }
        public int c_tripcustid { get; set; }
        public string c_tripstatus { get; set; }
    }
}