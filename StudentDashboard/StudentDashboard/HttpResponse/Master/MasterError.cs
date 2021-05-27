using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.Master
{
    public class MasterError
    {
        [JsonProperty("error_message")]
        public string m_strErrorMessage;
        [JsonProperty("error_code")]
        public int m_iErrorCode;
    }
}