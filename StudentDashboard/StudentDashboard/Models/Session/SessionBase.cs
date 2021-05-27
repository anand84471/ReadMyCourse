using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Session
{
    public class SessionBase
    {
        [JsonProperty("token")]
        public string Token;
        [JsonProperty("expiry_time")]
        public DateTime ExpiryTime;
        public bool IsLoggedOut;
        public SessionBase()
        {

        }
        public SessionBase(string token,DateTime expiryTime,bool isLoggedOut)
        {
            Token = token;
            ExpiryTime = expiryTime;
            IsLoggedOut = isLoggedOut;
        }
        public bool GetIsExpired()
        {
            return ExpiryTime - DateTime.Now < TimeSpan.Zero;
        }

    }
}