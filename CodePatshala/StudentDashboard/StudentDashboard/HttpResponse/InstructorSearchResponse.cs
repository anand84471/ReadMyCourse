using Newtonsoft.Json;
using StudentDashboard.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class InstructorSearchResponse:APIDefaultResponse
    {
        [JsonProperty("courses")]
        public List<CourseDetailsModel> m_lsCourses;
        [JsonProperty("assignments")]
        public List<AssignmentDetailsModel> m_lsAssignments;
        [JsonProperty("tests")]
        public List<TestDetailsModel> m_lsTestDetails;
    }
}