using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Repositories.Models;

namespace Repositories
{
    public class UserRepository : CommonRepository, IUserRepository
    {
        public UserRepository(IConfiguration myConfiguration) : base(myConfiguration)
        {
        }

        public User Login(User user)
        {
            User userdata = new User();
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT c_userid, c_useremail, c_userpassword, c_userrole FROM t_tripuser115 WHERE c_useremail=@email AND c_userpassword = @pass", conn))
                {
                    cmd.Parameters.AddWithValue("@email", user.c_useremail);
                    cmd.Parameters.AddWithValue("@pass", user.c_userpassword);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userdata.c_userid = reader.GetInt32(0);
                            userdata.c_useremail = reader.GetString(1);
                            userdata.c_userpassword = reader.GetString(2);
                            userdata.c_userrole = reader.GetString(3);
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
            // Console.WriteLine(userdata.c_userid);
            return userdata;
        }
    }
}