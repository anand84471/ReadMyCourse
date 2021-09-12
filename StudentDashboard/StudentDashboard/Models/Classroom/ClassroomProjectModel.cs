using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Classroom
{
    public class ClassroomProjectModel
    {
        [JsonProperty("project_name")]
        public string m_strProjectName;
        [JsonProperty("project_description")]
        public string m_strProjectDescription;
    }
}