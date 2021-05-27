using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Session
{
    public class StudentSession : SessionBase
    {
        [JsonIgnore]
        public long StudentId;
        public string EmailId;
        public StudentSession()
        {

        }
        public StudentSession(string token, DateTime expiryTime,
            long studentId, bool isLoggedOut) : base(token, expiryTime, isLoggedOut)
        {
            Token = token;
            ExpiryTime = expiryTime;
            StudentId = studentId;
        }
    }
}