using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Classroom
{
    public class ClassroomBase
    {
        [JsonProperty("classroom_name")]
        public string m_strClassroomName;
        [JsonProperty("classroom_description")]
        public string m_strClassroomDescription;
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("classroom_charge_in_paise")]
        public int ClassroomJoiningInPaise;
    }
}