using DIWebApiTutorial.EmployeeService;
using DIWebApiTutorial.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIWebApiTutorial.CacheManager
{
    public sealed class CacheManager
    {
        private CacheManager() { }
        public static async Task<List<Employee>> GetEmployees(RequestQuery requestQuery, IEmployeeService _employeeService, IMemoryCache _memCacheProvider)
        {
            List<Employee> Employees = new List<Employee>();
            if (_memCacheProvider.TryGetValue(CacheKeys.GetAllEmployeesKey, out Employees))
            {
                return await Task.FromResult(Employees);
            }
            else
            {
                Employees = _employeeService.GetEmployees().Result.Where(s => s.Salary > requestQuery.MinSal && s.Salary < requestQuery.MaxSal)
                    .OrderBy(s => s.EmployeeID)
                    .ToList();

                Employees = Employees.Skip(requestQuery.PageSize * (requestQuery.Page - 1))
                            .Take(requestQuery.PageSize).ToList();


                //TakeWhile - will take till condition specifies
                //SequenceEqual - Should match the sequence and location

                InsertToCache(CacheKeys.GetAllEmployeesKey, Employees,_memCacheProvider);
                return await Task.FromResult(Employees);
            }
        }


        public static async Task<Employee> GetEmployee(int id,IEmployeeService _employeeService, IMemoryCache _memCacheProvider)
        {
            Employee employee = new Employee();
            if (!_memCacheProvider.TryGetValue(CacheKeys.GetAllEmployeesKey, out employee))
            {
                employee = await _employeeService.GetEmployee(id);
                InsertToCache(CacheKeys.GetAllEmployeeKey, employee, _memCacheProvider);
                //return employee;
            }
            return employee;
            //else
            //{
            //    employee = await _employeeService.GetEmployee(id);
            //    InsertToCache(CacheKeys.GetAllEmployeeKey, employee, _memCacheProvider);
            //    return employee;
            //}
        }

        public static void InsertToCache(string cacheKey, object value,IMemoryCache memoryCache)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromSeconds(120));

            memoryCache.Set(CacheKeys.GetAllEmployeesKey, value, cacheOptions);

        }
    }
}
