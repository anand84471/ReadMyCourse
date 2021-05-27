using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentBase
    {
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("id")]
        public long Id;
        [JsonProperty("profile_url")]
        public string ProfileUrl;
    }
}