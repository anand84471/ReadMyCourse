using Newtonsoft.Json;
using StudentDashboard.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class RecentStudentLiveCourseJoin: StudentBase
    {
        [JsonProperty("classroom_name")]
        public string classroomName { get; set; }

        [JsonProperty("joining_date")]
        public String dateOfJoining { get; set; }
        [JsonProperty("classroom_id")]
        public long classroomId { get; set; }
    }
}