using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Models
{
    public class ShoppingCart
    {
         public int c_shoppingid { get; set; }
    public int c_albumid { get; set; }
    public int c_userid { get; set; }
    public string c_username { get; set; }
    public int c_quantity { get; set; }
    public int c_total_price { get; set; }

    public string c_status{ get; set; }
    }
}