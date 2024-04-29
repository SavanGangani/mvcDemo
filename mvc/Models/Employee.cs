using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class Employee
    {

        public int c_empid { get; set; }
        
        [Required(ErrorMessage = "Employee name is required.")]
        [StringLength(100, ErrorMessage = "Employee name must be between 1 and 100 characters.", MinimumLength = 1)]
        public string c_empname { get; set; }

        [Required(ErrorMessage = "Hire date is required.")]
        [DataType(DataType.Date)]
        public DateTime c_hiredate { get; set; }

        [Required(ErrorMessage = "Gross salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Gross salary must be a positive value.")]
        public decimal c_grosssalary { get; set; }

        [Required(ErrorMessage = "Designation ID is required.")]
        public int c_designation { get; set; }

        [Required(ErrorMessage = "Designation name is required.")]
        public string c_designationname { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string c_gender { get; set; }
        public decimal c_basic { get; set; }
        public decimal c_da { get; set; }
        public decimal c_hra { get; set; }
        public decimal c_tax { get; set; }
        public decimal c_taxablesalary { get; set; }
        public decimal c_takehomepay { get; set; }
    }
}