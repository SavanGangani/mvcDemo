using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
// using mvc.Models;
using Npgsql;
using Repositories.Models;
using Repositories.Repository;

namespace Repositories.Repository
{
    public class UserRepository : CommonRepository, IUserRepository
    {
        public UserRepository(IConfiguration myConfiguration) : base(myConfiguration)
        {
        }
        public int Insert(Customer customer)
        {
            int tokenid = -1;
            try
            {
                conn.Open();
                var cmd = new NpgsqlCommand("INSERT INTO t_token115(c_tokentype,c_tokenstatus) VALUES (@c_tokentype,@c_tokenstatus) RETURNING c_tokenid", conn);
                cmd.Parameters.AddWithValue("@c_tokentype", customer.c_tokentype);
                cmd.Parameters.AddWithValue("@c_tokenstatus", "Pending");
                // cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    tokenid = Convert.ToInt32(reader["c_tokenid"]);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return tokenid;
        }

        public List<Customer> GetAllTask()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT c_tokenid,c_tokentype, c_tokenstatus, c_username, c_usermobile FROM t_token115 ORDER BY c_tokenid", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer();
                            customer.c_tokenid = reader.GetString(0);
                            customer.c_tokentype = reader.GetString(1);
                            customer.c_tokenstatus = reader.GetString(2);
                            customers.Add(customer);
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
            return customers;
        }

    }
}