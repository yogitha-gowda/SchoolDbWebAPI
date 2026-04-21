using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDBCodeFirstApp.Models
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }

        //[StringLength(25)]
        //[Column("grade_name",TypeName ="VarChar")]
        //public string? GradeName { get; set; } //Removed using update-database "ISV1" to migrate back to this <--

        public string Section { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<Student>? Students { get; set; }

    }
}
