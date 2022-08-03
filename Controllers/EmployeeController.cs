using DIWebApiTutorial.EmployeeService;
using DIWebApiTutorial.Models;
using DIWebApiTutorial.CacheManager;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DIWebApiTutorial.Controllers
{
    [ApiVersion("1.0")]
    [Route("employee")]
    [EnableCors]
    //[Route("v{v:apiVersion}/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private IMemoryCache _memCacheProvider;
        public EmployeeController(IEmployeeService employeeService,IMemoryCache memoryCache)
        {
            _employeeService = employeeService;
            _memCacheProvider = memoryCache;
        }

        //[HttpGet("GetEmployees")]
        [HttpGet("GetEmployees")]
        [Route("[action]")]
        //[Route("api/{Employee}/")]
        //public IEnumerable<Employee> GetEmployees([FromQuery] RequestQuery requestQuery)
        public async Task<List<Employee>> GetEmployees([FromQuery] RequestQuery requestQuery)
        {
            return await CacheManager.CacheManager.GetEmployees(requestQuery, _employeeService, _memCacheProvider);//CacheManager.GetEmployees(requestQuery,_employeeService,_memCacheProvider);
            //return await CacheManager.GetEmployees(requestQuery, _employeeService, _memCacheProvider);//CacheManager.GetEmployees(requestQuery,_employeeService,_memCacheProvider);
            //List<Employee> Employees = new List<Employee>();
            //if (_memCacheProvider.TryGetValue(CacheKeys.GetAllEmployeesKey, out Employees))
            //{
            //    return await Task.FromResult(Employees);
            //}
            //else
            //{
            //    Employees = _employeeService.GetEmployees().Result.Where(s => s.Salary > requestQuery.MinSal && s.Salary < requestQuery.MaxSal)
            //        .OrderBy(s => s.EmployeeID)                       
            //        .ToList();

            //    Employees = Employees.Skip(requestQuery.PageSize * (requestQuery.Page - 1))
            //                .Take(requestQuery.PageSize).ToList();


            //    //TakeWhile - will take till condition specifies
            //    //SequenceEqual - Should match the sequence and location
            //    var cacheOptions = new MemoryCacheEntryOptions()
            //        .SetSlidingExpiration(TimeSpan.FromSeconds(30));

            //    //Set object in cache
            //    _memCacheProvider.Set(CacheKeys.GetAllEmployeesKey, Employees, cacheOptions);
            //    return await Task.FromResult(Employees);
            //}

            //return Ok(Employees);

        }

        [HttpPost("AddEmployee")]
        [Route("[action]")]
        //[Route("Employee/AddEmployee")]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            await _employeeService.AddEmployee(employee);
            return await Task.FromResult(Ok("Employee with ID : " + employee.EmpID + " is Added Successfully."));
        }

        [HttpPost("UpdateEmployee")]
        [Route("[action]")]
        //[Route("employee")]
        //[Route("api/Employee/UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            await _employeeService.UpdateEmployee(employee);
            return Ok("Employee with ID : "+ employee.EmpID + "is Updated Successfully.");
        }

        [HttpDelete("DeleteEmployee")]
        [Route("[action]")]
        //[Route("api/Employee/DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var existingEmployee = _employeeService.GetEmployee(id).Result;
            if (existingEmployee != null)
            {
                await _employeeService.DeleteEmployee(existingEmployee.EmpID);
                return Ok("Employee with ID : " + existingEmployee.EmpID + " is Deleted Successfully.");
            }
            return NotFound($"Employee Not Found with ID : {id}");
        }

        [HttpGet("GetEmployee")]
        [Route("[action]")]
        public async Task<Employee> GetEmployee(int id)
        {
            return await Task.FromResult(_employeeService.GetEmployee(id)).Result;
        }
    }


    //[ApiVersion("2.0")]
    //[Route("employeev2")]
    ////[Route("v{v:apiVersion}/employee")]
    //[ApiController]
    //public class EmployeeV2Controller : ControllerBase
    //{
    //    private readonly IEmployeeService _employeeService;
    //    public EmployeeV2Controller(IEmployeeService employeeService)
    //    {
    //        _employeeService = employeeService;
    //    }

    //    //[HttpGet("GetEmployees")]
    //    [HttpGet]
    //    //[Route("[action]")]
    //    //[Route("api/{Employee}/")]
    //    public IEnumerable<Employee> Get([FromQuery] RequestQuery requestQuery)
    //    {
    //        return null;
    //    //    List<Employee> Employees = _employeeService.GetEmployees().Where(s => s.EmpAge > requestQuery.MinAge)
    //    //                                .Where(s => s.EmpAge < requestQuery.MaxAge).ToList();

    //    //    Employees = Employees.Skip(requestQuery.PageSize * (requestQuery.Page - 1))
    //    //                .Take(requestQuery.PageSize).ToList();

    //    //    return Employees;
    //    }

    //    [HttpPost]
    //    [Route("[action]")]
    //    [Route("api/Employee/AddEmployee")]
    //    public IActionResult AddEmployee(Employee employee)
    //    {
    //        _employeeService.AddEmployee(employee);
    //        return Ok();
    //    }

    //    [HttpPost]
    //    [Route("[action]")]
    //    [Route("api/Employee/UpdateEmployee")]
    //    public IActionResult UpdateEmployee(Employee employee)
    //    {
    //        _employeeService.UpdateEmployee(employee);
    //        return Ok();
    //    }

    //    [HttpDelete]
    //    [Route("[action]")]
    //    [Route("api/Employee/DeleteEmployee")]
    //    public IActionResult DeleteEmployee(int id)
    //    {
    //        var existingEmployee = _employeeService.GetEmployee(id);
    //        if (existingEmployee != null)
    //        {
    //            _employeeService.DeleteEmployee(existingEmployee.ID);
    //            return Ok();
    //        }
    //        return NotFound($"Employee Not Found with ID : {id}");
    //    }

    //    [HttpGet]
    //    [Route("GetEmployee")]
    //    public Employee GetEmployee(int id)
    //    {
    //        return _employeeService.GetEmployee(id);
    //    }
    //}
}
