using Newtonsoft.Json;
using StudentDashboard.Models.Classroom;
using StudentDashboard.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class ClassroomTransactions:ClassroomBase
    {
        [JsonProperty("transaction_date")]
        public string TransactionDate;
    }
}