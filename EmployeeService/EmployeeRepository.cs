using DIWebApiTutorial.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIWebApiTutorial.EmployeeService
{
    //Added Solid Principles
    public class EmployeeRepository : IEmployeeService
    {
        public EmployeeContext _employeeDbContext;
        private readonly object employeeLock = new object();

        public EmployeeRepository(EmployeeContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            lock (employeeLock)
            {
                _employeeDbContext.EmployeeTbl.AddAsync(employee);
                _employeeDbContext.SaveChangesAsync();                
            }
            return await Task.FromResult(employee);
        }
        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeDbContext.EmployeeTbl.ToListAsync();//(List<Employee>)await _employeeDbContext.Employees.ToList();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            lock (employeeLock)
            {
                _employeeDbContext.EmployeeTbl.Update(employee);
                _employeeDbContext.SaveChangesAsync();
            }
            return await Task.FromResult(employee);
        }

        public async Task<Employee> DeleteEmployee(int Id)
        {
            Employee employee = null;
            employee = await _employeeDbContext.EmployeeTbl.FirstOrDefaultAsync(x => x.EmpID == Id);

            //lock (employeeLock)
            //{
            //    employee = await _employeeDbContext.EmployeeTbl.FirstOrDefaultAsync(x => x.EmpID == Id);
            //}
            if (employee != null)
            {
                lock (employeeLock)
                {
                    _employeeDbContext.Remove(employee);
                    _employeeDbContext.SaveChangesAsync();
                }
                return await Task.FromResult(employee);
            }
            return null;
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            return await _employeeDbContext.EmployeeTbl.FirstOrDefaultAsync(x => x.EmpID == Id);
        }
    }
}
