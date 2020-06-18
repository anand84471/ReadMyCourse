using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class CoursesJoinedResponseModal
    {
        [JsonProperty("student_name")]
        public string m_strStudentName;
        [JsonProperty("date_of_joining")]
        public string m_strDateOfJoining;
        [JsonProperty("student_id")]
        public long m_llStudentId;
        public CoursesJoinedResponseModal(string StudentName,string DateOfJoining,long StudentId)
        {
            this.m_strStudentName = StudentName;
            this.m_strDateOfJoining = DateOfJoining;
            this.m_llStudentId = StudentId;
        }
    }
}