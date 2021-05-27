using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.Student
{
    public class PasswordResetResponse
    {
        [JsonProperty("token")]
        public string PasswordRecoveryToken { get; set; }
        [JsonProperty("email_id")]
        public string EmailId { get; set; }
    }
}