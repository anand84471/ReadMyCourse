using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentBasicClassroomDetails
    {
        public string m_strClassroomName;
        public long m_llClassroomId;
        public string m_strClassroomDescription;
        public string m_strClassroomJoiningDate;
        public string m_strClassroomImage;
        public string m_strClassroomStartDateMsg;
        public StudentBasicClassroomDetails(long ClassroomId,string ClassroomName,string ClassroomDescription,
            string ClassroomJoiningDate)
        {
            this.m_strClassroomDescription = ClassroomDescription;
            this.m_strClassroomName = ClassroomName;
            this.m_strClassroomJoiningDate = ClassroomJoiningDate;
            this.m_llClassroomId = ClassroomId;
        }
        public StudentBasicClassroomDetails(long ClassroomId, string ClassroomName, string ClassroomDescription,
            string ClassroomJoiningDate,string ClassroomImage,DateTime? startDate)
        {
            this.m_strClassroomDescription = ClassroomDescription;
            this.m_strClassroomName = ClassroomName;
            this.m_strClassroomJoiningDate = ClassroomJoiningDate;
            this.m_llClassroomId = ClassroomId;
            this.m_strClassroomImage = ClassroomImage;
            DateTime classStartDate;
            if (startDate == null)
            {
                this.m_strClassroomStartDateMsg = "";
            }
            else
            {
                classStartDate = (DateTime)startDate;
                if(classStartDate-DateTime.Now>TimeSpan.Zero)
                m_strClassroomStartDateMsg = "starting from " + classStartDate.ToString("d MMM yyyy");
                else
                    m_strClassroomStartDateMsg = "started from " + classStartDate.ToString("d MMM yyyy");

            }
        }
    }
}