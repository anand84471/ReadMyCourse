using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class InstrucorEarningDetailsModal
    {
        [JsonProperty("instructor_id")]
        public string m_iInstructorId;
        [JsonProperty("classroom_earning")]
        public int m_iClassroomEarning;
        [JsonProperty("course_earning")]
        public int m_iCourseEarning;
        [JsonProperty("total_earning")]
        public int m_iTotalEarning;
        [JsonProperty("total_courses_sells")]
        public int m_iTotalCourseSells;
        [JsonProperty("total_unpaid_amount")]
        public int m_iTotalUnpaidAmount;
        [JsonProperty("active_clasrooms")]
        public int m_iActiveClassrooms;
        [JsonProperty("classroom_earning_details")]
        public List<InstructorClassroomEarningModal> m_lsInstructorClassroomEarningModal;
        [JsonProperty("course_earning_details")]
        public List<InstructorCourseEarningDetailsModal> m_lsInstructorCourseEarningDetailsModal;
        public InstrucorEarningDetailsModal(int TotalEarnings,int TotalCoursesSells,int TotalUnpaidAmount,int ActiveClassrooms)
        {
            this.m_iTotalEarning = TotalEarnings/100;
            this.m_iTotalUnpaidAmount = TotalUnpaidAmount / 100;
            this.m_iTotalCourseSells = TotalCoursesSells;
            this.m_iActiveClassrooms = ActiveClassrooms;
        }
        public InstrucorEarningDetailsModal()
        {

        }

    }
}