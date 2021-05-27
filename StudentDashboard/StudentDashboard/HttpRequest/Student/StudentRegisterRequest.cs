using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest.Student
{
    public class StudentRegisterRequest: RegisterBase
    {

        [Required]
        [StringLength(maximumLength: 50)]
        [Display(Name = "password")]
        public string Password { get; set; }
        public string EmailVarificationUid { get; set; }
    }
}