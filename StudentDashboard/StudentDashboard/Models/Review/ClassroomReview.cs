using Newtonsoft.Json;
using StudentDashboard.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Review
{
    public class ClassroomReview:ReviewBase
    {
        [JsonProperty("student_details")]
        StudentBase student;
    }
}