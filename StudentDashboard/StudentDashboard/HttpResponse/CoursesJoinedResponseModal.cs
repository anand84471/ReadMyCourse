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
        [JsonProperty("profile_url")]
        public string m_strProfileUrl;
        [JsonProperty("no_of_followers")]
        public int m_iNoOfFollowers;
        public CoursesJoinedResponseModal(string StudentName,string DateOfJoining,long StudentId,string ProfileUrl,int NoOfFollowers)
        {
            this.m_strStudentName = StudentName;
            this.m_strDateOfJoining = DateOfJoining;
            this.m_llStudentId = StudentId;
            this.m_strProfileUrl = ProfileUrl;
            this.m_iNoOfFollowers = NoOfFollowers;

        }
    }
}