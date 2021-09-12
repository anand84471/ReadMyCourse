using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Payment
{
    public class TransactionBase
    {
        public int TransactionAmount { get; set; }
        public string DateOfTransactions { get; set; }
    }
}