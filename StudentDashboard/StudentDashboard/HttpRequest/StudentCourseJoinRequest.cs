﻿using Newtonsoft.Json;
using StudentDashboard.Models.RazorPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class StudentCourseJoinRequest
    {
        [JsonProperty("course_id")]
        public long m_llCourseId;
        [JsonProperty("student_id")]
        public long m_llStudentId;
        [JsonProperty("payment_response")]
        public RazorPayPaymentResponseModal razorPayPaymentResponseModal;
        [JsonProperty("order_id")]
        public string m_strOrderId;
    }
}