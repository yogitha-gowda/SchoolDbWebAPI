using SchoolDBCodeFirstApp.Models;

using SchoolDBCoreWebAPI.Models;

namespace SchoolDBCoreWebAPI.Services

{

    public class GradeDAL

    {

        public GradeDAL() { }

        public GradeDAL(SchoolDBContext dBContext)

        {

            Context = dBContext;

        }

        public SchoolDBContext Context;

        public List<Grade> GetAllGrades()

        {

            try

            {

                return Context.Grades.OrderBy(g => g.Section).ToList();

            }

            catch (Exception ex)

            {

                throw ex;

            }

        }

        public Grade GetGradeById(int gradeId)

        {

            try

            {

                return Context.Grades.FirstOrDefault(g => g.GradeId == gradeId);

            }

            catch (Exception ex)

            {

                throw ex;

            }

        }

        public int AddGrade(Grade grade)

        {

            Context.Grades.Add(grade);

            return Context.SaveChanges();

        }

        public void UpdateGrade(Grade grade)

        {

            Context.Entry(grade).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            Context.SaveChanges();

        }

        public void DeleteGrade(int grdId)

        {

            var grade = Context.Grades.Find(grdId);

            if (grade != null)

            {

                Context.Grades.Remove(grade);

                Context.SaveChanges();

            }

        }

    }

}

