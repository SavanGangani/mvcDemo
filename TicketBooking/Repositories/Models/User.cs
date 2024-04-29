using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Models
{
    public class User
    {
        public int c_userid { set; get; }

        public string c_useremail { set; get; }

        public string c_userpassword { set; get; }

        public string c_userrole { set; get; }
    }
}