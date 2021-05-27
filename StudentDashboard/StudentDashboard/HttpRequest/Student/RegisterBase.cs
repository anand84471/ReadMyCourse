using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest.Student
{
    public class RegisterBase
    {
        [Required]
        [Display(Name = "first_name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "last_name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "email_id")]
        public string EmailId { get; set; }
        [Phone]
        [Display(Name = "phone_no")]
        public string PhoneNo { get; set; }
        [Required]
        [StringLength(maximumLength: 4)]
        [Display(Name = "phone_code")]
        public string PhoneCode { get; set; }
    }
}