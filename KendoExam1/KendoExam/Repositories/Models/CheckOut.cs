using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Models
{
    public class CheckOut
    {
           public int c_id { get; set; }
        public Int64 c_orderid { get; set; } // Common orderid for multiple orders

        public int c_userid { get; set; }

        public string c_fname { get; set; }

        public string c_lname { get; set; }

        public string c_address { get; set; }

        public string c_city { get; set; }

        public int c_postalcode { get; set; }

        public string c_state { get; set; }

        public string c_country { get; set; }

        public long c_phone { get; set; }

        public string c_email { get; set; }

        public string c_priority { get; set; }

        public DateTime c_shipdate { get; set; }

        public DateTime c_orderdate { get; set; }

        public int c_albumid { get; set; }

        public int c_qauntity { get; set; }

        public double c_total_price {get; set;}

        public int c_shoppingid { get; set; }
    }
}