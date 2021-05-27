using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest.Student
{
    public class StudentRegisterGoogleRequest: RegisterBase
    {
        [Url]
        [Display(Name = "image_url_small")]
        public string ImageUrlSmall { get; set; }
        [Required]
        public string GoogleId { get; set; }
        [Required]
        public string GoogleAccessToken { get; set; }
    }
}