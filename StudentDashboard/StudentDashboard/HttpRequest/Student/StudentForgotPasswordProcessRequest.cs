using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest.Student
{
    public class StudentForgotPasswordProcessRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "email_id")]
        public string EmailId { get; set; }
        [Required]
        [Display(Name = "password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "password_reset_token")]
        public string PasswordRecoveryToken { get; set; }
    }
}