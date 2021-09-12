using Newtonsoft.Json;
using StudentDashboard.Models.Payment;
using StudentDashboard.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Classroom
{
    public class ClassroomTransactionDetails
    {
        [JsonProperty("student")]
        public StudentBase studentDetails { get; set; }
        [JsonProperty("transaction_details")]
        public TransactionBase transactionDetails { get; set; }
        [JsonProperty("classroom_details")]
        public ClassroomBase classroomBase { get; set; }
    }
}