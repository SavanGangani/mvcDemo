using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Models
{
    public class Album
    {
         public int c_albumid { get; set; }

        public string c_image { get; set; }

        [ForeignKey("AlbumType")]
        public string c_type { get; set; }
        public virtual AlbumType albumType {get; set;}
        public string c_artist { get; set; }

        public string c_title { get; set; }

        public decimal c_price { get; set; }




         public int c_shoppingid { get; set; }
    // public int c_albumid { get; set; }
    public int c_userid { get; set; }
    public string c_username { get; set; }
    public int c_quantity { get; set; }
    public int c_total_price { get; set; }

    public string c_status{ get; set; }
    }
}