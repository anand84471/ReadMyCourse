using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Event
{
    public class StudentEventDetails
    {
        [JsonProperty("event_id")]
        public long EventId;
        [JsonProperty("student_name")]
        public string StudentName;
        [JsonProperty("email")]
        public string StudentEmail;
    }
}