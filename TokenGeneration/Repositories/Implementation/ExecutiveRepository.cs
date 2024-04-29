using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
// using Repositories.Interface;
using Repositories.Models;
using Repositories.Repository;

namespace Repositories.Repository
{
    public class ExecutiveRepository : CommonRepository, IExecutiveRepository
    {
        public ExecutiveRepository(IConfiguration myConfiguration) : base(myConfiguration)
        {
        }

        //  private readonly IHttpContextAccessor _httpContextAccessor;

        // public ExecutiveRepository(IHttpContextAccessor httpContextAccessor)
        // {
        //     _httpContextAccessor = httpContextAccessor;
        // }
        public Executive Login(Executive login)
        {
            Executive loggedInUser = null;
            try
            {
                conn.Open();
                var cmd = new NpgsqlCommand("SELECT c_userid, c_userpassword, c_usertype FROM t_executiveuser WHERE c_userid=@c_userid AND c_userpassword=@c_userpassword", conn);
                cmd.Parameters.AddWithValue("@c_userid", login.t_userid);
                cmd.Parameters.AddWithValue("@c_userpassword", login.t_userpassword);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        loggedInUser = new Executive
                        {
                            t_userid = reader.GetString(0),
                            t_userpassword = reader.GetString(1),
                            t_usertype = reader.GetString(2)
                        };
                    }
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
            return loggedInUser;
        }

        public List<Customer> GetAllForUser(string username)
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT c_tokenid,c_tokentype, c_tokenstatus, c_username, c_usermobile FROM t_token115 WHERE c_tokentype = @type AND c_tokenstatus = 'Pending' ORDER BY c_tokenid", conn))
                {
                    cmd.Parameters.AddWithValue("@type", username);
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



        public Customer GetDataFromId(string id)
        {
            Customer customer = null;
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT c_tokenid,c_tokentype, c_tokenstatus, c_username, c_usermobile FROM t_token115 WHERE c_tokenid = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customer = new Customer();
                            customer.c_tokenid = reader.GetString(0);
                            customer.c_tokentype = reader.GetString(1);
                            customer.c_tokenstatus = reader.GetString(2);
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
            return customer;
        }

        public void UpdateTask(Customer customer)
        {
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE t_token115 SET c_tokenstatus = @c_tokenstatus, c_username = @username, c_usermobile = @mobile  WHERE c_tokenid = @c_tokenid", conn))
                {
                    cmd.Parameters.AddWithValue("@c_tokenid", customer.c_tokenid);
                    cmd.Parameters.AddWithValue("@c_tokenstatus", customer.c_tokenstatus);
                    cmd.Parameters.AddWithValue("@username", customer.c_username);
                    cmd.Parameters.AddWithValue("@mobile", customer.c_usermobile);
                    cmd.ExecuteNonQuery();
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
        }
    }
}