using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.AlertManager
{
    public class InstructorAlertManager
    {
        public int m_iAlertType;
        public string m_strAlertMessage;
        public HomeService objHomeService;
        public InstructorAlertManager()
        {
            objHomeService = new HomeService();
        }
        private string GetAlertMessage(int m_iAlertType)
        {
            string AlertMessage = "";
            switch(m_iAlertType)
            {
                case (int)Constants.InstructorAlertType.STUDNET_JOINED:
                    {
                        AlertMessage = Constants.ALERT_INSTRUCTOR_NEW_JOIN;
                        break;
                    }
                case (int)Constants.InstructorAlertType.COURSE_JOINED:
                    {
                        AlertMessage = Constants.ALERT_INSTRUCTOR_NEW_COURSE_JOIN;
                        break;
                    }
                case (int)Constants.InstructorAlertType.ASSIGNMENT_SUBMISSION:
                    {
                        AlertMessage = Constants.ALERT_INSTRUCTOR_NEW_ASSIGNMENT_SUBMISSION;
                        break;
                    }
                case (int)Constants.InstructorAlertType.TEST_SUBMISSION:
                    {
                        AlertMessage = Constants.ALERT_INSTRUCTOR_NEW_TEST_SUBMISSION;
                        break;
                    }
            }
            return AlertMessage;
           
        }
        public void AddStudentJoinAlert(long StudentId,int InstructorId)
        {
            objHomeService.InsertNewAlertForInstructor(InstructorId, GetAlertMessage((int)Constants.InstructorAlertType.STUDNET_JOINED), (int)Constants.InstructorAlertType.STUDNET_JOINED,
                 StudentId,null);
        }
        public async Task<bool> AddCourseJoinAlert(long StudentId, long CourseId)
        {
            bool result = false;
            try
            {
                int InstructorId = -1;
                objHomeService.GetInstructorIdByCourseId(ref InstructorId, CourseId);
                result=await objHomeService.InsertNewAlertForInstructor(InstructorId, GetAlertMessage((int)Constants.InstructorAlertType.COURSE_JOINED), (int)Constants.InstructorAlertType.COURSE_JOINED,
                     StudentId, CourseId);
            }
            catch
            {

            }
            return result;
        }
        public async Task<bool> AddTestSubmissionAlert(long StudentId,long TestId)
        {
            bool result = false;
            try
            {
                int InstructorId = -1;
                objHomeService.GetInstructorIdByTestId(ref InstructorId,TestId);
                result=await objHomeService.InsertNewAlertForInstructor(InstructorId, GetAlertMessage((int)Constants.InstructorAlertType.TEST_SUBMISSION), (int)Constants.InstructorAlertType.TEST_SUBMISSION,
                     StudentId, TestId);
            }
            catch
            {

            }
            return result;
        }
        public void AddAssignmentSubmissionAlert(long StudentId, long AssignmentId)
        {
            try
            {
                int InstructorId = -1;
                objHomeService.GetInstructorIdByAssignmentId(ref InstructorId, AssignmentId);
                objHomeService.InsertNewAlertForInstructor(InstructorId, GetAlertMessage((int)Constants.InstructorAlertType.ASSIGNMENT_SUBMISSION), (int)Constants.InstructorAlertType.ASSIGNMENT_SUBMISSION,
                     StudentId, AssignmentId);
            }
            catch
            {

            }
        }
    }
}