using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Search
{
    public class SearchRequest<T>:SearchBaseRequest
    {
        [JsonProperty("id")]
        public T Id;
    }
}