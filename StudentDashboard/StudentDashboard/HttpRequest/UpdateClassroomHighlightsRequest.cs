using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class UpdateClassroomHighlightsRequest
    {
        [JsonProperty("classroom_id")]
        public long classroomId;
        [JsonProperty("highlights")]
        public string highlights;
    }
}