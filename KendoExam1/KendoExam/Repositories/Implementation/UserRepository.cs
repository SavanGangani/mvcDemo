using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Repositories.Models;
using Repositories.Repository;

namespace Repositories.Repository
{

    public class UserRepository : CommonRepository, IUserRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(IConfiguration myConfiguration, IHttpContextAccessor httpContextAccessor) : base(myConfiguration)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        User IUserRepository.GetOne(int id)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM t_kendouser115 WHERE c_userid = @c_userid";
            cmd.Parameters.AddWithValue("c_userid", id);
            NpgsqlDataReader sdr = cmd.ExecuteReader();
            var user = new User();
            while (sdr.Read())
            {
                user.c_userid = Convert.ToInt32(sdr["c_userid"]);
                user.c_username = sdr["c_username"].ToString();
                user.c_email = sdr["c_useremail"].ToString();
                user.c_password = sdr["c_userpassword"].ToString();

            }
            conn.Close();
            sdr.Close();
            return user;
        }


        public User Login(string email, string password)
        {
            User user = null;

            try
            {

                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT c_userid, c_username, c_email, c_password FROM t_kendouser115 WHERE c_email = @email";

                    // Add parameters with NpgsqlDbType
                    cmd.Parameters.AddWithValue("@email", NpgsqlTypes.NpgsqlDbType.Text, email);
                    Console.WriteLine("Email: + "+email + " & "+ password );

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User()
                            {
                                c_userid = Convert.ToInt32(reader["c_userid"]),
                                c_username = reader["c_username"].ToString(),
                                c_password = reader["c_password"].ToString(),
                                c_email = reader["c_email"].ToString(), // Fix the column name here
                            };

                            // Store user information in session
                            var session = _httpContextAccessor.HttpContext.Session;
                            session.SetString("username", user.c_username);
                            session.SetInt32("userid", user.c_userid);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred while logging in: {ex.Message}");
            }finally{
                conn.Close();
            }

            return user;
        }

        bool IUserRepository.Register(User user)
        {
            if (IsEmailExists(user.c_email))
            {
                return false;
            }

            using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO t_kendouser115(c_username, c_email, c_password) VALUES (@name, @email, @password)", conn))
            {
                command.Parameters.AddWithValue("name", user.c_username);
                command.Parameters.AddWithValue("email", user.c_email);
                command.Parameters.AddWithValue("password", user.c_password);
                conn.Open();
                if (command.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return true;
                }
            }
            conn.Close();
            return false;
        }

        private bool IsEmailExists(string c_useremail)
        {
            conn.Open();
            using (NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) FROM t_kendouser115 WHERE c_email = @email", conn))
            {
                command.Parameters.AddWithValue("email", c_useremail);
                int count = Convert.ToInt32(command.ExecuteScalar());
                conn.Close();
                return count > 0;
            }
        }


        bool IUserRepository.IsEmailExists(string c_useremail)
        {
            conn.Open();
            using (NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) FROM t_kendouser115 WHERE c_email = @email", conn))
            {
                command.Parameters.AddWithValue("email", c_useremail);
                int count = Convert.ToInt32(command.ExecuteScalar());
                conn.Close();
                return count > 0;
            }
        }


        void IUserRepository.Update(User user)
        {
            throw new NotImplementedException();
        }


    }
}