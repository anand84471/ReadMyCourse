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
        public bool m_bIsPaymentDone;
        public string m_strClassroomClassStartDate;
        public bool m_bShouldBlockClassroomAccess;
        public int m_iClassroomJoiningFee;
        public StudentClassroomHomeDetails(long ClassroomId,int NoOfMeetingsJoined,int NoOfTestSubmissions,int NoOfAssignmentSubmissions,
            bool IsPaymentDone,DateTime? ClassroomStartDate,int JoiningFeeInPaise)
        {
            this.m_llClassroomId = ClassroomId;
            this.m_iNoOfMeetingJoined = NoOfMeetingsJoined;
            this.m_iNoOfTestSubmitted = NoOfTestSubmissions;
            this.m_iNoOfAssignmentSubmitted = NoOfAssignmentSubmissions;
            this.m_bIsPaymentDone = IsPaymentDone;
            if(JoiningFeeInPaise!=0&&!m_bIsPaymentDone&& ClassroomStartDate != null&&((DateTime)ClassroomStartDate).AddDays(3)<DateTime.Now)
            {
                m_bShouldBlockClassroomAccess = true;
            }
        }
    }
}