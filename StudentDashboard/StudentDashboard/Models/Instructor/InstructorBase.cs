using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class InstructorBase
    {
        [JsonProperty("instructor_id")]
        public int m_iInstructorId;
        [JsonProperty("instructor_name")]
        public string m_strName;
        [JsonProperty("profile_url_small")]
        public string m_strProfileUrlSmall;
        [JsonProperty("profile_url_medium")]
        public string m_strProfileUrlMedium;
        [JsonProperty("joining_date")]
        public string m_strJoiningDate;
    }
}