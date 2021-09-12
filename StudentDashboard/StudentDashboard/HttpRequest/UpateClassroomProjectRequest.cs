using Newtonsoft.Json;
using StudentDashboard.Models.Classroom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class UpateClassroomProjectRequest
    {
        [JsonProperty("classroom_id")]
        public long classroomId { get; set; }
        public List<ClassroomProjectModel> projects;
    }
}