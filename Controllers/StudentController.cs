using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolDBCodeFirstApp.Models;
using SchoolDBCoreWebAPI.Models;
using SchoolDBCoreWebAPI.Services;

namespace SchoolDBCoreWebAPI.Controllers
{

    [Route("api/[controller]/[Action]")]
    [ApiController]
    [EnableCors]
    public class StudentController : ControllerBase
    {
        public StudentDAL stDAL;

        public SchoolDBContext context;

        public StudentController(SchoolDBContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Student>> GetAllStudents()
        {
            stDAL = new StudentDAL(context);
            return stDAL.GetAllStudents();
        }

        [HttpGet("{stdId}")]
        public ActionResult<Student> GetStudentbyId(int stdId)
        {
            stDAL = new StudentDAL(context);
            return stDAL.GetStudentById(stdId);
        }

        [HttpPost]
        public ActionResult<int> AddStudents(Student std)
        {
            stDAL = new StudentDAL(context);
            return Ok(stDAL.AddStudent(std));
        }

        [HttpPut("{stdId}")]
        public async Task<ActionResult> UpdateStudent(int stdId, Student std)
        {
            stDAL =new StudentDAL(context);
            Student s1 = stDAL.GetStudentById(stdId);
            if (s1 == null)
                return NotFound();
            int result=stDAL.UpdateStudent(std);

            return NoContent();
        }
    }
}
