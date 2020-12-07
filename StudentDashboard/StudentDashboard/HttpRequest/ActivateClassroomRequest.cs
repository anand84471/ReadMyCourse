﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class ActivateClassroomRequest
    {
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("classroom_joining_fee")]
        public int m_iClassroomJoiningFee;
        [JsonProperty("classroom_start_date")]
        public string m_strClassroomDate;
        public DateTime m_dtClassroomStartDate;
        [JsonProperty("classroom_start_time")]
        public string m_strClassroomStartTime;
        public TimeSpan m_tClassroomStartTime;
        [JsonProperty("classroom_weekday_schedule")]
        public string m_strClassroomBitwiseSchedule;
        [JsonProperty("public_type")]
        public int m_bPublicType;
        [JsonIgnore]
        public string m_strClassroomShareCode;
        [JsonIgnore]
        public string m_strTinyUrl;
    }
}