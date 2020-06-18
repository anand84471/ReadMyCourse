using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class BasicCourseDetails
    {
        [JsonProperty("course_name")]
        public string m_strIndexName { get; set; }
        [JsonProperty("course_id")]
        public long m_iIndexId { get; set; }
    }
}