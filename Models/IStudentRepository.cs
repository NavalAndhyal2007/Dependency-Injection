using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIWebApiTutorial.Models
{
    public interface IStudentRepository
    {
        Student GetStudentById(int StudentId);
        List<Student> GetAllStudent();
    }
}
