using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.Master
{
    public class MasterResponseBase
    {
        [JsonProperty("response_message")]
        public string m_strResponseMessage;
        [JsonProperty("response_code")]
        public int m_iReponseCode;
        [JsonProperty("error")]
        public MasterError error;
        public MasterResponseBase()
        {
            m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
            m_iReponseCode = Constants.API_RESPONSE_CODE_FAIL;
        }
        public void SetSuccessReponse()
        {
            m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
            m_iReponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
        }
    }
}