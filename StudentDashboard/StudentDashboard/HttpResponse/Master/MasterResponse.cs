using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.Master
{
    public class MasterResponse<T>:MasterResponseBase
    {
        [JsonProperty("data")]
        public T data;
    }
}