using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentClassroomHomeDetails
    {
        public long m_llClassroomId;
        public long m_iNoOfAssignmentSubmitted;
        public long m_iNoOfTestSubmitted;
        public long m_iNoOfMeetingJoined;
        public StudentClassroomHomeDetails(long ClassroomId,int NoOfMeetingsJoined,int NoOfTestSubmissions,int NoOfAssignmentSubmissions)
        {
            this.m_llClassroomId = ClassroomId;
            this.m_iNoOfMeetingJoined = NoOfMeetingsJoined;
            this.m_iNoOfTestSubmitted = NoOfTestSubmissions;
            this.m_iNoOfAssignmentSubmitted = NoOfAssignmentSubmissions;
        }
    }
}