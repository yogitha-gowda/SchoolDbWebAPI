using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;

using SchoolDBCodeFirstApp.Models;

using SchoolDBCoreWebAPI.Models;

using SchoolDBCoreWebAPI.Services;

namespace SchoolDBCoreWebAPI.Controllers

{

    [Route("api/[controller]/[action]")]

    [ApiController]

    public class GradeController : ControllerBase

    {

        public GradeDAL grDAL;

        public SchoolDBContext context;


        public GradeController(SchoolDBContext context)

        {

            this.context = context;

        }

        [HttpGet]

        public ActionResult<List<Grade>> GetAllGrades()

        {

            grDAL = new GradeDAL(context);

            return grDAL.GetAllGrades();

        }

        [HttpGet("{gradeId}")]

        public ActionResult<Grade> GetGradeById(int gradeId)

        {

            grDAL = new GradeDAL(context);
            Grade grd = null;
            try
            {
                grd = grDAL.GetGradeById(gradeId);
            }
            catch (Exception ex) 
            {

            }
            finally
            {

            }
            if (grd == null)
                return NotFound();

            return Ok(grd);
        }

        [HttpPost]

        public IActionResult AddGrade(Grade grade)

        {

            grDAL = new GradeDAL(context);

            int result=grDAL.AddGrade(grade);

            return Ok(result);

        }

        [HttpPut]

        public IActionResult UpdateGrade(Grade grade)

        {

            grDAL.UpdateGrade(grade);

            return Ok(new { message = "Successfully Updated" });

        }

        [HttpDelete("{id}")]

        public IActionResult DeleteGrade(int id)

        {

            grDAL.DeleteGrade(id);

            return Ok(new { message = "Successfully Deleted" });

        }

    }

}

