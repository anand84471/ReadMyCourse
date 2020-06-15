using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class GetJoinedCourseRequest
    {
        [JsonProperty("key")]
        public string m_strSearchString;
        [JsonIgnore]
        [JsonProperty("id")]
        public long m_llStudentGid;
    }
}