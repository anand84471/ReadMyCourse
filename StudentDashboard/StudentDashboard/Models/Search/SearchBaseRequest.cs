using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Search
{
    public class SearchBaseRequest
    {
        [JsonProperty("key")]
        public string m_strSearchString;
        [JsonProperty("rows_fetched")]
        public int m_iNoOfRowsFetched;
        [JsonProperty("no_of_rows_to_fetch")]
        public int m_iNoOfRowsToFetch;
    }
}