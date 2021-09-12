using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class ClassroomMeetingLinkUpdateRequest
    {
        [JsonProperty("meeting_link")]
        public string classroomMeetingLink;
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        public int m_iInstructorId;
    }
}