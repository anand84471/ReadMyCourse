using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Event
{
    public class EventModel
    {
        public long EventId;
        public string EventName;
        public string EventDescription;
        public DateTime dtEventStartTime;
        public int InstructorId;
        public string EventsHighlights;
        public string MeetingLink;
    }
}