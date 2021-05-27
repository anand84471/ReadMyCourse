using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest.Student
{
    public class StudentLoginGoogleRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "email_id")]
        public string EmailId { get; set; }
        [Required]
        public string GoogleId { get; set; }
        [Required]
        public string GoogleAccessToken { get; set; }
    }
}