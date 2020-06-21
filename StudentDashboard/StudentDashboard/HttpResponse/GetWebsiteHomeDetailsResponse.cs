using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetWebsiteHomeDetailsResponse:APIDefaultResponse
    {
        [JsonProperty("no_of_instructors")]
        public long m_llNoOfInstructorsJoined;
        [JsonProperty("no_of_courses_created")]
        public long m_llNoOfCoursesCreated;
        [JsonProperty("no_of_assignments_created")]
        public long m_llNoOfAssignmentscreated;
        [JsonProperty("no_of_tests_created")]
        public long m_llNoOfTestCreated;
        [JsonProperty("no_of_students_joined")]
        public long m_llNoOfStudentsJoined;
        public GetWebsiteHomeDetailsResponse()
        {

        }
        public GetWebsiteHomeDetailsResponse(long NoOfInstructors,long NoOfCourses,long NoOfTests,long NoOfAssignments,long NoOfStudents)
        {
            this.m_llNoOfAssignmentscreated = NoOfAssignments;
            this.m_llNoOfCoursesCreated = NoOfCourses;
            this.m_llNoOfStudentsJoined = NoOfStudents;
            this.m_llNoOfInstructorsJoined = NoOfInstructors;
            this.m_llNoOfTestCreated = NoOfTests;

        }
    }
}