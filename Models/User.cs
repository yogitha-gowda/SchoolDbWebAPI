using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SchoolDBCodeFirstApp.Models
{
    public class User
    {
        public int UsertId { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 100")]
        public string FullName { get; set; } = null!;
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Email format is invalid")]
        public string Email { get; set; } = null;
        [Required(ErrorMessage = "Password hash is required")]
        public string PassWordHash { get; set; } = null!;
        [Required(ErrorMessage = "Role is required")]
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 character.")]
        public string Role { get; set; } = null!;
    }
}
