using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentHomeModal
    {
        public int m_iCoursesJoined;
        public int m_iCoursesCompleted;
        public int m_iAssignmentsSubmitted;
        public int m_iTestSubmitted;
        public StudentHomeModal(int CoursesJoined, int CourseCompleted, int AssignmentsSubmitted, int TestsSubmitted)
        {
            this.m_iCoursesJoined = CoursesJoined;
            this.m_iCoursesCompleted = CourseCompleted;
            this.m_iAssignmentsSubmitted = AssignmentsSubmitted;
            this.m_iTestSubmitted = TestsSubmitted;
        }
    }
   
}