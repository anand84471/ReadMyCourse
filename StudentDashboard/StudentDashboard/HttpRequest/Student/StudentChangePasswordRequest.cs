using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest.Student
{
    public class StudentChangePasswordRequest
    {
        [Display(Name ="password")]
        public string Password;
        [Display(Name="confirm_password")]
        public string ConfirmPassword;
        public long StudentId;
        public string HashedPassword;
    }
}