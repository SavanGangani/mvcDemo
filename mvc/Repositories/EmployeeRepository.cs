using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;
using Npgsql;

namespace mvc.Repositories
{
    public class EmployeeRepository : CommonRepository, IEmployeeRepository
    {
        public void AddEmployee(Employee employee)
        {
            try
            {
                // Console.WriteLine("SDFG " + employee.c_hiredate);
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO t_employee115 (c_empname, c_hiredate, c_grosssalary, c_designationid, c_gender) VALUES (@name, @hiredate, @salaray, @designation, @gender)", conn))
                {
                    cmd.Parameters.AddWithValue("@name", employee.c_empname);
                    cmd.Parameters.AddWithValue("@hiredate", employee.c_hiredate.Date);
                    cmd.Parameters.AddWithValue("@salaray", employee.c_grosssalary);
                    cmd.Parameters.AddWithValue("@designation", employee.c_designation);
                    cmd.Parameters.AddWithValue("@gender", employee.c_gender);
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

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT c_empid, c_empname, c_hiredate, c_grosssalary,c_designation, c_gender, c_designationid,c_designation, c_basic, c_da, c_hra, c_taxable_salary, c_tax, c_takehomepay FROM t_designation115 d, t_employee115 e WHERE d.c_id = e.c_designationid ORDER BY d.c_id", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var emp = new Employee
                            {
                                c_empid = Convert.ToInt32(reader["c_empid"]),
                                c_empname = reader["c_empname"].ToString(),
                                c_hiredate = Convert.ToDateTime(reader["c_hiredate"]),
                                c_grosssalary = Convert.ToInt32(reader["c_grosssalary"]),
                                c_designationname = reader["c_designation"].ToString(),
                                c_gender = reader["c_gender"].ToString(),
                            };
                            employees.Add(emp);
                        };
                    }
                }
            }

            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return employees;
        }

        public void DeleteEmployee(Employee employee)
        {
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM t_employee115 WHERE c_empid = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", employee.c_empid);
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


        public void UpdateEmployee(Employee employee)
        {
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE t_employee115 SET c_empname = @name, c_hiredate= @date,c_grosssalary = @salary, c_designationid = @designation, c_gender = @gender  WHERE c_empid = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@name", employee.c_empname);
                    cmd.Parameters.AddWithValue("@date", employee.c_hiredate);
                    cmd.Parameters.AddWithValue("@salary", employee.c_grosssalary);
                    cmd.Parameters.AddWithValue("@designation", employee.c_designation);
                    cmd.Parameters.AddWithValue("@gender", employee.c_gender);
                    cmd.Parameters.AddWithValue("@id", employee.c_empid);
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

        public Employee GetEmployee(int id)
        {
            Employee employee = null;
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT c_empid, c_empname, c_hiredate, c_grosssalary, c_gender, c_designationid, c_basic, c_da, c_hra, c_taxable_salary, c_tax, c_takehomepay FROM t_employee115 WHERE c_empid = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employee = new Employee
                            {
                                c_empid = Convert.ToInt32(reader["c_empid"]),
                                c_hiredate = Convert.ToDateTime(reader["c_hiredate"]),
                                c_empname = reader["c_empname"].ToString(),
                                c_grosssalary = Convert.ToInt32(reader["c_grosssalary"]),
                                c_designation = Convert.ToInt32(reader["c_designationid"]),
                                // c_designationname = reader["c_designation"].ToString(),
                                c_gender = reader["c_gender"].ToString(),
                                c_basic = reader["c_basic"] != DBNull.Value ? Convert.ToDecimal(reader["c_basic"]) : 0,
                                c_da = reader["c_da"] != DBNull.Value ? Convert.ToDecimal(reader["c_da"]) : 0,
                                c_hra = reader["c_hra"] != DBNull.Value ? Convert.ToDecimal(reader["c_hra"]) : 0,
                                c_tax = reader["c_tax"] != DBNull.Value ? Convert.ToDecimal(reader["c_tax"]) : 0,
                                c_taxablesalary = reader["c_taxable_salary"] != DBNull.Value ? Convert.ToDecimal(reader["c_taxable_salary"]) : 0,
                                c_takehomepay = reader["c_takehomepay"] != DBNull.Value ? Convert.ToDecimal(reader["c_takehomepay"]) : 0
                            };
                        };
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
            return employee;
        }

        public int GetDesignationID(string designation)
        {
            int designationId = 0;
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT  c_id FROM t_designation115 WHERE c_designation = @designation", conn))
                {
                    cmd.Parameters.AddWithValue("@designation", designation);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            designationId = reader.GetInt32(0);
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
            return designationId;
        }

        public void UpdateEmployeePayroll(int id)
        {
            try
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE t_employee115 SET c_basic = c_grosssalary * 0.6, c_da = c_grosssalary * 0.25, c_hra = c_grosssalary * 0.15, c_taxable_salary = CASE WHEN c_grosssalary > 25000 THEN c_grosssalary - 25000 ELSE c_grosssalary END, c_tax = CASE WHEN c_grosssalary > 25000 THEN (c_grosssalary - 25000) * 0.1 ELSE 0 END, c_takehomepay = (c_grosssalary * 0.6) + (c_grosssalary * 0.25) + (c_grosssalary * 0.15) - (CASE WHEN c_grosssalary > 25000 THEN (c_grosssalary - 25000) * 0.1 ELSE 0 END) WHERE c_empid = @EmployeeId;";

                    cmd.Parameters.AddWithValue("@EmployeeId", id);

                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
