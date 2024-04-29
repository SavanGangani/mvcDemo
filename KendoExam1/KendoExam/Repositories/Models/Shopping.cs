using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Models
{
    public class Shopping
    {
        public int c_shoppingid {get; set;}

        [ForeignKey("Album")]
        public int c_albumid { get; set; }
        public virtual Album album {get; set;}

        public int c_userid {get; set;}
        public string c_username { get; set; }

        public int c_quantity {get; set;}

        public double c_total_price { get; set; }

        public double c_price {get; set;}

        public string c_title {get; set;}
    }
}