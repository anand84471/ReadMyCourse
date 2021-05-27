using Newtonsoft.Json;
using StudentDashboard.Models.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Classroom
{
    public class ClassroomPublicDetails:ClassroomShowcaseDetails
    {
        [JsonProperty("instructor_details")]
        public InstructorPublicProfileDetails Instructor;
        [JsonProperty("syllabus")]
        public List<ClassroomWeekWiseSyallabus> m_lsClassroomWeekWiseSyallabus;
    }
}