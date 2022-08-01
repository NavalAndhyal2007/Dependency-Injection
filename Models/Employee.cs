using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIWebApiTutorial.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string EmpDOB { get; set; }
        public string EmpJoiningDate { get; set; }
        public int PrevExperience { get; set; }
        public int Salary { get; set; }
        public string Address { get; set; }
    }
}
