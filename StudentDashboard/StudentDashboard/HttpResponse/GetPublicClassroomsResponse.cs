using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetPublicClassroomsResponse
    {
        [JsonProperty("classroom_name")]
        public string m_strClasssroomName;
        [JsonProperty("classroom_id")]
        public long m_llClasssroomId;
        [JsonProperty("classroom_creation_date")]
        public string m_strCreationDate;
        [JsonProperty("no_of_enrollments")]
        public int m_iNoOfEnrollments;
        [JsonProperty("is_joined")]
        public bool m_IsJoined;
        [JsonProperty("joining_date")]
        public string m_strJoiningDate;
        public GetPublicClassroomsResponse(string classroomName,
            long classroomId,string creationDate,int noOfEnrollments,long? StudentJoinId,
            DateTime? JoiningDate)
        {
            this.m_strClasssroomName = classroomName;
            this.m_llClasssroomId = classroomId;
            this.m_strCreationDate = creationDate;
            this.m_iNoOfEnrollments = noOfEnrollments;
            if(StudentJoinId!=null)
            {
                m_IsJoined = true;
            }

        }
    }
}