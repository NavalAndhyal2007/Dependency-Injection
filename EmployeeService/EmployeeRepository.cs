using DIWebApiTutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIWebApiTutorial.EmployeeService
{
    public class EmployeeRepository : IEmployeeService
    {
        public EmployeeContext _employeeDbContext;
        public EmployeeRepository(EmployeeContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            _employeeDbContext.EmployeeTbl.Add(employee);
            _employeeDbContext.SaveChanges();
            return await Task.FromResult(employee);
        }
        public async Task<List<Employee>> GetEmployees()
        {
            return await Task.Run(() => _employeeDbContext.EmployeeTbl.ToList<Employee>());//(List<Employee>)await _employeeDbContext.Employees.ToList();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            _employeeDbContext.EmployeeTbl.Update(employee);
            _employeeDbContext.SaveChanges();
            return await Task.FromResult(employee);
        }

        public async Task<Employee> DeleteEmployee(int Id)
        {
            var employee = _employeeDbContext.EmployeeTbl.FirstOrDefault(x => x.EmpID == Id);
            if (employee != null)
            {
                _employeeDbContext.Remove(employee);
                _employeeDbContext.SaveChanges();
                return await Task.FromResult(employee);
            }
            return null;
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            return await Task.FromResult(_employeeDbContext.EmployeeTbl.FirstOrDefault(x => x.EmpID == Id));
        }
    }
}
