using DIWebApiTutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIWebApiTutorial.EmployeeService
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployee(Employee employee);

        Task<List<Employee>> GetEmployees();

        Task<Employee> UpdateEmployee(Employee employee);

        Task<Employee> DeleteEmployee(int Id);

        Task<Employee> GetEmployee(int Id);
    }
}
