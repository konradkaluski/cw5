using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using cw3.Models;
using cw3.Services;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private IDBService _dBService;
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s19391;Integrated Security=True";

        public StudentsController(IDBService service)
        {
            _dBService = service;
        }

        //2.QueryString
        [HttpGet]
        public IActionResult GetStudents()//string orderBy)
        {

            var list = new List<Student>();

            using(var con = new SqlConnection(ConString))
            using(var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student;";

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.Birthdate = (DateTime) dr["BirthDate"];
                    list.Add(st);
                }

                return Ok(list);

            }

          
        }


        [HttpGet("{indexNumber}")]
        public IActionResult GetEnrollment(string indexNumber)
        {

            var list2 = new List<Enrollment>();

            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Enrollment inner join Student on Enrollment.IdEnrollment = Student.IdEnrollment where Student.IndexNumber = @index";
                com.Parameters.AddWithValue("index", indexNumber);

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {

                    var en = new Enrollment();
                    en.IndexNumber = dr["IndexNumber"].ToString();
                    en.IdEnrollment = (int) dr["IdEnrollment"];
                    en.Semester = (int) dr["Semester"];
                    en.IdStudy = (int)dr["IdStudy"];
                    en.StartDate = dr["StartDate"].ToString();
                    list2.Add(en);
                }

                return Ok(list2);

            }
        }

        
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {

           
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }
        

        [HttpPut("{id}")]
        public IActionResult ModifyStudent(int id)
        {
            return Ok("Aktualizacja dokonczona");
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie zakonczone");
        }
    }
}