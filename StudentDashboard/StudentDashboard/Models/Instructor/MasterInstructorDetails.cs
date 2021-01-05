using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class MasterInstructorDetails
    {
        [JsonProperty()]
        public string m_strInstructorName;
        public string m_strName;
        public long m_iInstructorId;
    }
}