using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolDBCodeFirstApp.Models;
using SchoolDBCoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBCoreWebAPI.Services
{
    public class SchoolDAL
    {
        //public string _ConStr;
        public SchoolDBContext Context;

        public SchoolDAL( SchoolDBContext dBContext)
        {
          /*  IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
                .Build();

            _ConStr = config.GetConnectionString("SchoolCon");
            Context = new SchoolDBContext(_ConStr);
          */
          Context = dBContext;
        }




        /* public void DisplayStudents()
         {
             foreach (var std in Context.Students)
                 Console.WriteLine($"Student ID: {std.Studentid}, Name: {std.Name}");
         }
        */



        public StdWithNameAndRoll GetStudentNameAndRollById(int stdId)
        {
            Student std = Context.Students.FirstOrDefault(s => s.Studentid == stdId);
            return new StdWithNameAndRoll { StudentName = std.Name, RollNumber = std.RollNumber };
        }

        public List<Student> GetAllStudentsWithGrade()
        {
            List<Student> Allstudents;
            try
            {
                Allstudents = Context.Students.OrderBy(s => s.Name).Include(s => s.grade).ToList();
                //Allstudents =[..Context.Students.Orderby(s=> s.name).Include(s=>s.grade)]
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Allstudents;
        }

        public List<Grade> GetAllGradeWithStudent()
        {
            List<Grade> Allgrades;
            try
            {
                Allgrades = Context.Grades.OrderBy(s => s.Section).Include(s => s.Students).ToList();
                //Allstudents =[..Context.Students.Orderby(s=> s.name).Include(s=>s.grade)]
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Allgrades;
        }

        public List<StdWithGradeDTO> GetAllStudentsDTO()
        {
            List<StdWithGradeDTO> Allstudents;
            Allstudents = Context.Students.OrderBy(s => s.Name).Include(s => s.grade)
                .Select(s => new StdWithGradeDTO()
                {
                    StudentName = s.Name,
                    StudentID = s.Studentid,

                    GrdDescription = s.grade.Description,
                    GrdSection = s.grade.Section,
                }).ToList();

            /*    var std=Context.Students.Include(s=>s.grade)
                    .Select(s=> new StdWithGradeDTO()
                    {
                        StudentName = s.Name,
                        StudentID = s.Studentid,
                        GrdDescription = s.grade.Description,
                        GrdSection = s.grade.Section,
                    }).SingleOrDefault(s=>s.StudentID == 0);
                */
            return Allstudents;
        }
    }
}
