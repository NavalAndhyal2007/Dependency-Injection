using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIWebApiTutorial.Models
{
    public class StudentRepository : IStudentRepository
    {
        public List<Student> DataSource()
        {
            return new List<Student>()
            {
                new Student() { StudentId = 101, Name = "Naval", Branch = "CSE", Section = "A", Gender = "Male" },
                new Student() { StudentId = 102, Name = "Amit", Branch = "ETC", Section = "B", Gender = "Male" },
                new Student() { StudentId = 103, Name = "Dharmesh", Branch = "CSE", Section = "A", Gender = "Male" },
                new Student() { StudentId = 104, Name = "Siya", Branch = "CSE", Section = "A", Gender = "Female" },
                new Student() { StudentId = 105, Name = "Pallavi", Branch = "ETC", Section = "B", Gender = "Female" }
            };
        }
        public List<Student> GetAllStudent()
        {
            return DataSource();
        }

        public Student GetStudentById(int StudentId)
        {
            return DataSource().FirstOrDefault(s => s.StudentId == StudentId);
        }
    }
}
