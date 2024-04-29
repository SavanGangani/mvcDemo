using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;

namespace mvc.Repositories
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee employee);

        List<Employee> GetEmployees();

        Employee GetEmployee(int id);

        void DeleteEmployee(Employee employee);

        void UpdateEmployee(Employee employee);
        void UpdateEmployeePayroll(int id);
    }
}