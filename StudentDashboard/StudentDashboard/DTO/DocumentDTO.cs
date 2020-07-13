using StudentDashboard.HttpResponse;
using StudentDashboard.Models.Course;
using StudentDashboard.Models.Instructor;
using StudentDashboard.Models.Utils;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.DTO
{
    public class DocumentDTO
    {
        CPDataService.CpDataServiceClient objCPDataService;
        StringBuilder m_strLogMessage = new StringBuilder();
        public DocumentDTO()
        {
            objCPDataService = new CPDataService.CpDataServiceClient();
        }
        //public async Task<TestModalForAnonymousAccess> GetTestDetails(long TestId,string AccessCode)
        //{
        //    TestModalForAnonymousAccess objTestModel = null;
        //    try { 
        //        DataSet ds = await objCPDataService.GetTestDetailsWithAccessCodeAsync(TestId, AccessCode);
        //        if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            objTestModel = ds.Tables[0].AsEnumerable().Select(
        //             dataRow => new TestModalForAnonymousAccess(
        //                 dataRow.Field<long>("TEST_ID"),
        //                 dataRow.Field<string>("TEST_NAME"),
        //                 dataRow.Field<string>("TEST_DESCRIPTION"),
        //                 dataRow.Field<DateTime>("TEST_ACTIVATION_DATETIME").ToString("d MMM yyyy"),
        //                 dataRow.Field<byte>("TEST_TYPE"),
        //                 dataRow.Field<String>("INSTRUCTOR_NAME"),
        //                 dataRow.Field<int>("TOTAL_ALLOWED_TIME"),
        //                 dataRow.Field<int>("TOTAL_MARKS"),
        //                 dataRow.Field<int>("TOTAL_NO_OF_QUESTIONS"),
        //                 dataRow.Field<int>("NO_OF_SUBMISSIONS"),
        //                 dataRow.Field<DateTime?>("TEST_START_TIME")
        //                 )).ToList()[0];
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
        //        m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTestDetails", Ex.ToString());
        //        m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
        //        MainLogger.Error(m_strLogMessage);
        //    }
        //    return objTestModel;
        //}

        public async Task<bool> CheckTestAccess(long TestId, string AccessCode)
        {
            bool result = false;
            try
            {
                DataSet ds = await objCPDataService.CheckTestAccessAsync(TestId, AccessCode);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckTestAccess", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckCourseAccess(long CourseId, string AccessCode)
        {
            bool result = false;
            try
            {
                DataSet ds = await objCPDataService.CheckCourseAccessAsync(CourseId, AccessCode);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckCourseAccess", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckAssignmentAccess(long AssignmentId, string AccessCode)
        {
            bool result = false;
            try
            {
                DataSet ds = await objCPDataService.CheckAssignmentAccessAsync(AssignmentId, AccessCode);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckAssignmentAccess", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckClassroomAccess(long ClassroomId, string AccessCode)
        {
            bool result = false;
            try
            {
                DataSet ds = await objCPDataService.CheckClassroomAccessAsync(ClassroomId, AccessCode);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckAssignmentAccess", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<GetTestDetailsResponseWithAccessCode> GetTestDetails(long TestId, string AccessCode)
        {
            GetTestDetailsResponseWithAccessCode objTestModel = null;
            try
            {
                DataSet ds = await objCPDataService.GetTestDetailsWithAccessCodeAsync(TestId, AccessCode);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objTestModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new GetTestDetailsResponseWithAccessCode(
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<string>("TEST_DESCRIPTION"),
                         dataRow.Field<DateTime>("TEST_ACTIVATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<byte>("TEST_TYPE"),
                         dataRow.Field<string>("INSTRUCTOR_NAME"),
                         dataRow.Field<int>("TOTAL_ALLOWED_TIME"),
                         dataRow.Field<int>("TOTAL_MARKS"),
                         dataRow.Field<int>("TOTAL_NO_OF_QUESTIONS"),
                         dataRow.Field<int>("NO_OF_SUBMISSIONS"),
                         dataRow.Field<DateTime?>("TEST_START_TIME")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objTestModel;
        }
        public async Task<GetWebsiteHomeDetailsResponse> GetHomeDetails()
        {
            GetWebsiteHomeDetailsResponse objTestModel = null;
            try
            {
                DataSet ds = await objCPDataService.GetWebsiteAboutDetailsAsync();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objTestModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new GetWebsiteHomeDetailsResponse(
                         dataRow.Field<int>("NO_OF_INSTRUCTORS"),
                         dataRow.Field<int>("NO_OF_COURSES"),
                         dataRow.Field<int>("NO_OF_TESTS_CREATED"),
                         dataRow.Field<int>("NO_OF_ASSIGNMENTS_CREATED"),
                         dataRow.Field<int>("NO_OF_STUDENTS_JOINED")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objTestModel;
        }
        public  List<SmsNotificationModal> GetAllSmsNotification(int RetryCount)
        {
            List<SmsNotificationModal> lsSmsNotificationModal = null;
            try
            {
                DataSet ds =  objCPDataService.GetAllNotificationToPrecess(RetryCount);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsSmsNotificationModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new SmsNotificationModal(
                         dataRow.Field<string>("SMS_BODY"),
                         dataRow.Field<string>("RECEIVER_PHONE_NO"),
                         dataRow.Field<long>("ID")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsSmsNotificationModal;
        }
        public bool ChangeSmsNotificationStatus(long NotificationId, bool Status)
        {
            bool result = false;
            try
            {
                result =  objCPDataService.UpdateNotificationStatus(Status, NotificationId);
               
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ChangeSmsNotificationStatus", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertSMSNotification(string Message, string SmsReceiver,int SmsNotificationTypeId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertSmsNotificationAsync(SmsNotificationTypeId,Message,SmsReceiver);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ChangeSmsNotificationStatus", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> VialidateStudentPhoneNoVarificationLink(string UserId,string Guid)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.ValidatePhoneNoVarificationLinkForStudentAsync(UserId, Guid);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "VialidateStudentPhoneNoVarificationLink", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> VialidateInstructorPhoneNoVarificationLink(string UserId, string Guid)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.ValidatePhoneNoVarificationLinkForInstructorAsync(UserId, Guid);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "VialidateInstructorPhoneNoVarificationLink", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<ClassRoomModal> GetClassroomDetailsForStudent(long ClassroomId)
        {
            ClassRoomModal objClassRoomModal = new ClassRoomModal();
            try
            {
                DataSet ds = await objCPDataService.GetClasroomDetailsAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objClassRoomModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassRoomModal(
                         dataRow.Field<long>("CLASSROOM_ID"),
                         dataRow.Field<string>("CLASSROOM_NAME"),
                          dataRow.Field<string>("CLASSROOM_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DETATIME").ToString("d MMM yyyy"),
                         dataRow.Field<bool>("IS_MEETING_ACTIVE")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objClassRoomModal;
        }
    }
}