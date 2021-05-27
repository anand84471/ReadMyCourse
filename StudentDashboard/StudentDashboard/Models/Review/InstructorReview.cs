using Newtonsoft.Json;
using StudentDashboard.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Review
{
    public class InstructorReview:ReviewBase
    {
        [JsonProperty("student_details")]
        public StudentBase student;
        [JsonProperty("classroom_name")]
        public string ClassroomName;
        [JsonProperty("classroom_id")]
        public long ClassroomId;
    }
}