using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;
using Npgsql;

namespace mvc.Repositories
{
    public class DesignationRepository : CommonRepository, IDesignationRepository
    {
        public List<Designation> GetDesignations()
        {
            List<Designation> designations = new List<Designation>();
            using (conn)
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM t_designation115", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var designation = new Designation
                            {
                                c_id = Convert.ToInt32(reader["c_id"]),
                                c_designation = reader["c_designation"].ToString(),
                            };
                            designations.Add(designation);
                        }
                    }
                }
            }
            return designations;
        }
    }
}