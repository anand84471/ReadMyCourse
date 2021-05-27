using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest.Student
{
    public class StudentLoginRequest
    {
        [Required]
        [StringLength(maximumLength: 50)]
        [Display(Name = "password")]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "email_id")]
        public string EmailId { get; set; }
    }
}