using DIWebApiTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIWebApiTutorial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _repository = null;
        //Initialize the variable through constructor
        public StudentController(IStudentRepository repository)
        {
            _repository = repository;
        }

        // GET api/<StudentController>
        [HttpGet]
        public JsonResult Get()
        {
            List<Student> allStudentDetails = _repository.GetAllStudent();
            return Json(allStudentDetails);
        }

        // GET api/<StudentController>/101
        [HttpGet("{id}")]
        public JsonResult Get(int Id)
        {
            Student studentDetails = _repository.GetStudentById(Id);
            return Json(studentDetails);
        }
    }
}
