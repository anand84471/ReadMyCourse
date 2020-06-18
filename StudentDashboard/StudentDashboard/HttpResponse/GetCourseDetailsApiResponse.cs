using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetCourseDetailsApiResponse:APIDefaultResponse
    {
        [JsonProperty("course_name")]
        public string m_strCourseName { get; set; }
        [JsonProperty("course_description")]
        public string m_strCourseDescription { get; set; }
        [JsonProperty("course_creation_date")]
        public string m_strCourseCreationDate { get; set; }
        [JsonProperty("course_updation_date")]
        public string m_strCourseUpdationDate { get; set; }
        [JsonProperty("indexes")]
        public List<CourseIndexDetails> m_lsIndexes { get; set; }
        public GetCourseDetailsApiResponse(string CourseName, string CourseDescription, string CourseCreationDate,string CourseUpdationDate)
        {
            this.m_strCourseName = CourseName;
            this.m_strCourseDescription = CourseDescription;
            this.m_strCourseCreationDate = CourseCreationDate;
            this.m_strCourseUpdationDate = CourseUpdationDate;
        }
        public GetCourseDetailsApiResponse()
        {

        }
    }
}