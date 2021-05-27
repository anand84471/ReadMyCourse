using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Notification
{
    public class NotificationBase
    {
        public long Id;
        public long Message;
        public string NotificationDate;
        public int Type;
        public string NotificatioUrl;
    }
}