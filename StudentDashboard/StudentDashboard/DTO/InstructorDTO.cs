using StudentDashboard.HttpRequest;
using StudentDashboard.Models;
using StudentDashboard.Models.Classroom;
using StudentDashboard.Models.Course;
using StudentDashboard.Models.Instructor;
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
    public class InstructorDTO
    {
        CPDataService.CpDataServiceClient objCPDataService;
        StringBuilder m_strLogMessage = new StringBuilder();
        public InstructorDTO()
        {
            objCPDataService = new CPDataService.CpDataServiceClient();
        }
        public async Task<bool> RegisterNewInstructor(InstructorRegisterModel objInstructorRegisterModal)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.RegisterNewInstructorAsync(objInstructorRegisterModal.m_strFirstName, objInstructorRegisterModal.m_strLastName, objInstructorRegisterModal.m_strPhoneNo, objInstructorRegisterModal.m_strEmail, objInstructorRegisterModal.m_strPassword,
                    objInstructorRegisterModal.m_strPhoneNoVarificationGuid,objInstructorRegisterModal.m_strEmailVarificationGuid);

            }
            catch (Exception Ex)
            {

            }
            return result;
        }

        public async Task<bool> ValidateInstructorLoginDetails(InstructorRegisterModel objInstructorRegisterModel)
        {
            bool result = false;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.ValidateInstructorLoginDetailsAsync(objInstructorRegisterModel.m_strEmail, objInstructorRegisterModel.m_strPassword);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<InstructorRegisterModel> lsRegisterModel = ds.Tables[0].AsEnumerable().Select(
                      dataRow => new InstructorRegisterModel(
                          dataRow.Field<string>("INSTRUCTOR_FIRST_NAME"),
                          dataRow.Field<string>("INSTRUCTOR_LAST_NAME"),
                          dataRow.Field<int>("ID"),
                          dataRow.Field<string>("PROFILE_URL")
                          )).ToList();
                    if (lsRegisterModel.Count == 1)
                    {
                        objInstructorRegisterModel.m_iInstructorId = lsRegisterModel[0].m_iInstructorId;
                        objInstructorRegisterModel.m_strProfilePictureUrl= lsRegisterModel[0].m_strProfilePictureUrl;
                        result = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<InstructorRegisterModel> GetInstructorPostLoginDetails(int Id)
        {
            InstructorRegisterModel objResult = null;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetInstructorPostLoginDetailsAsync(Id);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<InstructorRegisterModel> lsRegisterModel = ds.Tables[0].AsEnumerable().Select(
                      dataRow => new InstructorRegisterModel(
                          dataRow.Field<int>("TOTAL_CLASSROOMS"),
                          dataRow.Field<int>("TOAL_COURSES_SELLS"),
                          dataRow.Field<int?>("TOTAL_EARNINGS"),
                          dataRow.Field<int>("ACTIVE_CLASSROOMS"),
                          dataRow.Field<int>("NO_OF_FOLLOWERS")
                          )).ToList();
                    if (lsRegisterModel.Count == 1)
                    {
                        objResult = new InstructorRegisterModel();
                        objResult = lsRegisterModel[0];
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResult;
        }
        public async Task<InstructorRegisterModel> GetInstructorDetails(int Id)
        {
            InstructorRegisterModel objInstructorRegisterModel = null;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetInstructorDetailsAsync( Id);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<InstructorRegisterModel> lsRegisterModel = ds.Tables[0].AsEnumerable().Select(
                      dataRow => new InstructorRegisterModel(
                          dataRow.Field<string>("INSTRUCTOR_FIRST_NAME"),
                          dataRow.Field<string>("INSTRUCTOR_LAST_NAME"),
                          dataRow.Field<string>("EMAIL"),
                          dataRow.Field<string>("PHONE_NO"),
                          dataRow.Field<string>("ADDRESS_LINE_1"),
                          dataRow.Field<string>("ADDRESS_LINE_2"),
                          dataRow.Field<string>("CITY_NAME"),
                          dataRow.Field<string>("STATE_NAME"),
                          dataRow.Field<string>("PIN_CODE"),
                          dataRow.Field<string>("GENDER"),
                          dataRow.Field<string>("SCHOOL_NAME"),
                          dataRow.Field<DateTime?>("LAST_UPDATED"),
                          dataRow.Field<DateTime>("ROW_INSERTION_DATETIME"),
                          dataRow.Field<int?>("CITY_ID"),
                          dataRow.Field<int?>("STATE_ID"),
                          dataRow.Field<string>("SCHOOL_DETAILS"),
                          dataRow.Field<string>("LINKED_IN_PROFILE_ID"),
                          dataRow.Field<string>("GOOGLE_SCHOLAR_ID"),
                          dataRow.Field<string>("SHORT_BIO")
                          )).ToList();
                    if (lsRegisterModel.Count == 1)
                    {
                        objInstructorRegisterModel = lsRegisterModel[0];
                        
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objInstructorRegisterModel;
        }
        public async Task<bool> UpdateInstructorDetails(InstructorRegisterModel objInstructorRegisterModal)
        {
            bool result = false;
            try
            {
                result =await objCPDataService.UpdateInstructorDetailsAsync(objInstructorRegisterModal.m_strFirstName, objInstructorRegisterModal.m_strLastName,
                                                      objInstructorRegisterModal.m_strPhoneNo,objInstructorRegisterModal.m_strGender,objInstructorRegisterModal.m_strAddressLine1,
                                                      objInstructorRegisterModal.m_strAddressLine2,objInstructorRegisterModal.m_iCityid,
                                                      objInstructorRegisterModal.m_iStateId,objInstructorRegisterModal.m_strPinCode
                                                      ,objInstructorRegisterModal.m_iInstructorId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdatePassword(InstructorRegisterModel objInstructorRegisterModel)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateInstructorPasswordAsync(objInstructorRegisterModel.m_strPassword, objInstructorRegisterModel.m_iInstructorId);
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> AddMcqTestQuestion(McqQuestion objMcqQuestion)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertNewMcqTestQuestionAsync(objMcqQuestion.m_llTestId, objMcqQuestion.m_strQuestionStatement, objMcqQuestion.m_strOption1, objMcqQuestion.m_strOption2,
                    objMcqQuestion.m_strOption3, objMcqQuestion.m_strOption4, objMcqQuestion.m_iCorrectOption, objMcqQuestion.m_iTimeInSeconds, objMcqQuestion.m_iMarks);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AddMcqTestQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertPasswordRecoveryForInstructor(string UserId, string Token, string OTP)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertInstructorPasswordRecoveryRequestAsync(UserId, Token, OTP);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertPasswordRecovery", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> ValidatePasswordRecoveryOtpForInstructor(string UserId, string Token, string OTP)
        {
            bool result = false;
            DateTime? TokenExpiryTime = null;
            try
            {
                DataSet ds = await objCPDataService.ValidateInstructorPasswordRecoveryRequestAsync(UserId, Token, OTP);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    TokenExpiryTime = ds.Tables[0].AsEnumerable().Select(
                     dataRow => (dataRow.Field<DateTime?>("LAST_PASSWORD_RECOVERY_REQUEST_TIME"))).ToList()[0];
                }
                if (TokenExpiryTime != null && DateTime.Now - TokenExpiryTime > TimeSpan.FromSeconds(MvcApplication._forgotPasswordExpiryTimeInMinutes))
                {
                    result = true;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ValidatePasswordRecoveryOtpForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> MarkOtpVerifiedForPasswordRecoveryForInstructor(string UserId, string Token)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.MarkPassowordVarificationOtpVarifiedForInstructorAsync(UserId, Token);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "MarkOtpVerifiedForPasswordRecoveryForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public int GetInstructorIdFromUserId(string UserId)
        {
            int InstructorId = -1;
            try
            {
                objCPDataService.GetInstructorIdFromUserId(UserId, ref InstructorId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertActivityForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return InstructorId;
        }
        public async Task<bool> UpdateInstructorPasswordAfterAuth(string UserId, string Token, string HashedPasword)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.ChangePasswordAfterAuthenticationForInstructorAsync(UserId, Token, HashedPasword);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InertNewMeetingToClassroom(JitsiMeetingModal objJitsiMeetingModal)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InertNewMeetingToClassroomAsync(objJitsiMeetingModal.m_llClassroomId,
                    objJitsiMeetingModal.m_strMeetingName,
                    objJitsiMeetingModal.m_strMeetingPassword);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        
        public async Task<ClassRoomModal> GetClassroomDetailsForInstructor(long ClassroomId, int InstructorId)
        {
            ClassRoomModal objClassRoomModal = new ClassRoomModal();
            try
            {
                DataSet ds = await objCPDataService.GetClassRoomDetailsForInstructorAsync(InstructorId, ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objClassRoomModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassRoomModal(
                         dataRow.Field<long>("CLASSROOM_ID"),
                         dataRow.Field<string>("CLASSROOM_NAME"),
                         dataRow.Field<string>("CLASSROOM_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DETATIME").ToString("d MMM yyyy"),
                         dataRow.Field<string>("CLASSROOM_STATUS"),
                         dataRow.Field<int>("NO_OF_POSTS"),
                         dataRow.Field<string>("SHARE_URL"),
                         dataRow.Field<string>("SHARE_CODE"),
                         dataRow.Field<int>("NO_OF_ASSIGNMENTS"),
                         dataRow.Field<int>("NO_OF_TESTS"),
                         dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                         dataRow.Field<int>("NO_OF_MEETINGS"),
                         dataRow.Field<string>("BACK_GROUND_IMAGE_PATH"),
                         dataRow.Field<string>("CLASSROOM_MEETING_NAME"),
                         dataRow.Field<DateTime?>("CLASS_START_DATE"),
                         dataRow.Field<DateTime?>("REGISTRATION_CLOSE_DATE"),
                         dataRow.Field<int>("NO_OF_DEMO_CLASSES"),
                         dataRow.Field<bool?>("IS_VARIFIED_BY_ADMIN"),
                         dataRow.Field<int?>("ADMIN_VARIFICATION_CODE"),
                         dataRow.Field<string>("ADMIN_VARIFICATION_MESSAGE"),
                         dataRow.Field<bool>("SHOULD_USE_EXTERNAL_MEETING_LINK"),
                         dataRow.Field<string>("EXTERNAL_MEETING_LINK"),
                            dataRow.Field<string>("HIGHLIGHTS")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetClassroomDetailsForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objClassRoomModal;
        }
        public async Task<JitsiMeetingModal> GetClassroomMeetingDetails(long ClassroomId, int InstructorId)
        {
            JitsiMeetingModal objJitsiMeetingModal = new JitsiMeetingModal();
            try
            {
                DataSet ds = await objCPDataService.GetMeetingDetailsOfClassroomAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objJitsiMeetingModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new JitsiMeetingModal(
                         dataRow.Field<long>("MEETING_ID"),
                         dataRow.Field<string>("MEETING_NAME"),
                          dataRow.Field<string>("MEETING_PASSWORD"),
                         dataRow.Field<string>("CLASSROOM_NAME"),
                         dataRow.Field<bool>("SHOULD_USE_EXTERNAL_MEETING_LINK"),
                         dataRow.Field<string>("EXTERNAL_MEETING_LINK")
                         )).ToList()[0];
                }
                if(objJitsiMeetingModal!=null)
                {
                    objJitsiMeetingModal.m_llClassroomId = ClassroomId;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objJitsiMeetingModal;
        }
        public async Task<GetLiveClassDetailsForInstructor> GetLiveClassDetailsForInstructor(long MeetingId)
        {
            GetLiveClassDetailsForInstructor objGetLiveClassDetailsForInstructor = null;
            try
            {
                DataSet ds = await objCPDataService.GetLiveClassMeetingDetailsAsync(MeetingId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objGetLiveClassDetailsForInstructor = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new GetLiveClassDetailsForInstructor(
                         dataRow.Field<string>("MEETING_TOPIC"),
                         dataRow.Field<string>("MEETING_DESCRIPTION"),
                          dataRow.Field<string>("VIDEO_URL"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_PARTICIPANTS")
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
            return objGetLiveClassDetailsForInstructor;
        }
        public async Task<bool> CheckClassroomAccess(long ClassroomId, int InstructorId)
        {
            bool result = false;
            try
            {
                DataSet ds = await objCPDataService.CheckInstructorClassroomAccessAsync(ClassroomId, InstructorId);
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
        public async Task<bool> DeleteClassroomAssignment(long ClassroomID,long AssignmentId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.DeleteClassroomAssignmentAsync(ClassroomID,AssignmentId);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteClassroomAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<InstructorRegisterModel> GetInstructorBasicDetails(int Id)
        {
            InstructorRegisterModel objInstructorRegisterModel = null;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetInstructorDetailsAsync(Id);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<InstructorRegisterModel> lsRegisterModel = ds.Tables[0].AsEnumerable().Select(
                      dataRow => new InstructorRegisterModel(
                          dataRow.Field<string>("INSTRUCTOR_FIRST_NAME"),
                          dataRow.Field<string>("INSTRUCTOR_LAST_NAME"),
                          Id,
                          dataRow.Field<string>("PROFILE_URL")
                      
                          )).ToList();
                    if (lsRegisterModel.Count == 1)
                    {
                        objInstructorRegisterModel = lsRegisterModel[0];

                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objInstructorRegisterModel;
        }
        public async Task<bool> UpdateInstructorAcademicRecord(InstructorAcademicRecordDTO instructorAcademicRecordDTO)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateInstructorAcademicRecordAsync(
                    instructorAcademicRecordDTO.m_strLinkedIn,
                    instructorAcademicRecordDTO.m_strGoogleScholarId,instructorAcademicRecordDTO.m_iInstructorId,
                    instructorAcademicRecordDTO.m_strSchoolDetails);

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
        public async Task<bool> UpdateInstructorBio(int InstructorId,string IntructorBio)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateInstructorBioAsync(InstructorId, IntructorBio);

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
        public async Task<InstrucorEarningDetailsModal> GetInstructorEarningDetails(int InstructorId)
        {
            InstrucorEarningDetailsModal objResponse = null;
            try
            {
                DataSet ds = await objCPDataService.GetInstructorEarningsAsync(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new InstrucorEarningDetailsModal(
                         dataRow.Field<int>("TOTAL_EARNINGS"),
                         dataRow.Field<int>("TOAL_COURSES_SELLS"),
                          dataRow.Field<int>("TOTAL_UNPAID_AMOUNT"),
                         dataRow.Field<int>("ACTIVE_CLASSROOMS")
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
            return objResponse;
        }
        public async Task<List<InstructorClassroomEarningModal>> GetInstructorClassroomEarning(int InstructorId)
        {
            List<InstructorClassroomEarningModal> objResponse = null;
            try
            {
                DataSet ds = await objCPDataService.GetMonthwiseInstructorClassroomEarningAsync(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new InstructorClassroomEarningModal(
                         dataRow.Field<string>("MONTH_NAME"),
                         dataRow.Field<int>("EARNING_IN_PAISE")
                         )).ToList();
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        public async Task<List<InstructorCourseEarningDetailsModal>> GetInstructorCourseEarning(int InstructorId)
        {
            List<InstructorCourseEarningDetailsModal> objResponse = null;
            try
            {
                DataSet ds = await objCPDataService.GetMonthwiseInstructorCourseEarningAsync(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new InstructorCourseEarningDetailsModal(
                         dataRow.Field<string>("MONTH_NAME"),
                         dataRow.Field<int>("EARNING_IN_PAISE")
                         )).ToList();
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        public  long AddNewTestSeries(InsertTestSeriesRequest insertTestSeriesRequest)
        {
            long result = -1;
            try
            {
                objCPDataService.InsertNewTestSeries(insertTestSeriesRequest.m_iInstructorId,
                    insertTestSeriesRequest.m_strTestSeriesImage, insertTestSeriesRequest.m_strTestSeriesDescription,
                    insertTestSeriesRequest.m_strTestSeriesImage,ref result);
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
        public async Task<ClassRoomModal> GetClassroomDetailsFor(long ClassroomId, int InstructorId)
        {
            ClassRoomModal objClassRoomModal = new ClassRoomModal();
            try
            {
                DataSet ds = await objCPDataService.GetClassRoomDetailsForInstructorAsync(InstructorId, ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objClassRoomModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassRoomModal(
                         dataRow.Field<long>("CLASSROOM_ID"),
                         dataRow.Field<string>("CLASSROOM_NAME"),
                         dataRow.Field<string>("CLASSROOM_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DETATIME").ToString("d MMM yyyy"),
                         dataRow.Field<string>("CLASSROOM_STATUS"),
                         dataRow.Field<int>("NO_OF_POSTS"),
                         dataRow.Field<string>("SHARE_URL"),
                         dataRow.Field<string>("SHARE_CODE"),
                         dataRow.Field<int>("NO_OF_ASSIGNMENTS"),
                         dataRow.Field<int>("NO_OF_TESTS"),
                         dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                         dataRow.Field<int>("NO_OF_MEETINGS"),
                         dataRow.Field<string>("BACK_GROUND_IMAGE_PATH"),
                         dataRow.Field<string>("CLASSROOM_MEETING_NAME"),
                         dataRow.Field<DateTime?>("REGISTRATION_CLOSE_DATE"),
                         dataRow.Field<DateTime?>("CLASS_START_DATE"),
                         dataRow.Field<int>("NO_OF_DEMO_CLASSES")
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
        public async Task<bool> UpdateClassroomVideoUrl(UpdateClassroomVideoUrlRequest updateClassroomVideoUrlRequest)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateClassroomsingleMeetingDetailsAsync(updateClassroomVideoUrlRequest.m_llMeetingId, updateClassroomVideoUrlRequest.m_strVideoLink,
                    updateClassroomVideoUrlRequest.m_strTopicName,
                    updateClassroomVideoUrlRequest.m_strTopicDescription
                    );

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateClassroomVideoUrl", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateClassroomSyllabus(long ClassroomId,string ClassroomSyllabus)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateClassroomSyllabusAsync(ClassroomId, ClassroomSyllabus);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateClassroomSyllabus", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<ClassroomSyllabusDetailsModal> GetClassroomSyllabus(long ClassroomId)
        {
            ClassroomSyllabusDetailsModal classroomSyllabusDetailsModal = null;
            try
            {
                DataSet ds = await objCPDataService.GetClassroomSyllabusAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    classroomSyllabusDetailsModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomSyllabusDetailsModal(
                         dataRow.Field<string>("CLASSROOM_SYLLABUS")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateClassroomSyllabus", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return classroomSyllabusDetailsModal;
        }
        public async Task<List<MasterInstructorDetails>> SearchInstrucrByUserId(SearchInstructorByUserIdRequest searchInstructorByUserIdRequest)
        {
            List<MasterInstructorDetails> lsMasterInstructorDetails = null;
            try
            {
                DataSet ds = await objCPDataService.SearchInstructorByUserIdAsync(searchInstructorByUserIdRequest.m_strInstructorSearchId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsMasterInstructorDetails = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new MasterInstructorDetails(
                         dataRow.Field<string>("INSTRUCTOR_NAME"),
                         dataRow.Field<string>("PROFILE_URL"),
                         dataRow.Field<int>("INSTRUCTOR_ID")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateClassroomSyllabus", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsMasterInstructorDetails;
        }
        public async Task<bool> SendClassroomNotification(long ClassroomId, string ClassroomSyllabus)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.SendClassroomNotificationAsync(ClassroomId, ClassroomSyllabus);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateClassroomSyllabus", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckIsClassroomAlreadyTakenoday(long ClassroomId)
        {
            bool result = false;
            try
            {
                DataSet ds = await objCPDataService.CheckClassroomMeetingOccuredTodayAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsClassroomAlreadyTakenoday", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        //public async Task<List<ClassroomTransactionDetails>> GetInstructorRecentTransactions(int Id)
        //{
        //    List<ClassroomTransactionDetails> transactions = null;
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        ds = await objCPDataService.GetInstructorDetailsAsync(Id);
        //        if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            transactions = ds.Tables[0].AsEnumerable().Select(
        //              dataRow => new ClassroomTransactionDetails(
        //                  dataRow.Field<string>("INSTRUCTOR_FIRST_NAME"),
        //                  dataRow.Field<string>("INSTRUCTOR_LAST_NAME"),
        //                  Id,
        //                  dataRow.Field<string>("PROFILE_URL")

        //                  )
        //              {
        //                  classroomBase=new ClassroomBase()
        //                  {

        //                  },
        //                  transactionDetails=new Transcat
        //              }).ToList();
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
        //        m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorRecentTransactions", Ex.ToString());
        //        m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
        //        MainLogger.Error(m_strLogMessage);
        //    }
        //    return transactions;
        //}
        public async Task<List<RecentStudentLiveCourseJoin>> GetInstructorRecentClassroomJoin(int InstructorId,MasterSearchRequest request)
        {
            List<RecentStudentLiveCourseJoin> responseData = null;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetInstructorRecentLiveClassesJoinedAsync(InstructorId,
                    request.m_iNoOfRowsFetched, request.m_iMaxRowsToFetch);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                 responseData = ds.Tables[0].AsEnumerable().Select(
                      dataRow => new RecentStudentLiveCourseJoin()
                      {
                          Name= dataRow.Field<string>("FIRST_NAME"),
                          Id= dataRow.Field<long>("STUDENT_ID"),
                          ProfileUrl= dataRow.Field<string>("PROFILE_URL"),
                          classroomName= dataRow.Field<string>("CLASSROOM_NAME"),
                          dateOfJoining = dataRow.Field<DateTime>("DATE_OF_JOINING").ToString("d MMM yyyy"),
                          classroomId = dataRow.Field<long>("CLASSROOM_ID")
                      }).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorRecentClassroomJoin", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return responseData;
        }
        //
        public async Task<List<InstructorClassroomsEarningDetails>> InstructorClassroomsEarning(int InstructorId, MasterSearchRequest request)
        {
            List<InstructorClassroomsEarningDetails> responseData = null;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetInstructorClassroomwisePurchasesAsync(InstructorId,
                    request.m_iNoOfRowsFetched, request.m_iMaxRowsToFetch);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    responseData = ds.Tables[0].AsEnumerable().Select(
                         dataRow => new InstructorClassroomsEarningDetails()
                         {
                             m_strClassroomName = dataRow.Field<string>("CLASSROOM_NAME"),
                             m_llClassroomId = dataRow.Field<long>("CLASSROOM_ID"),
                             ClassroomJoiningInPaise = dataRow.Field<int>("CLASSROOM_CHARGE_IN_PAISE"),
                             TotalEarning = dataRow.Field<int>("TOTAL_AMOUNT"),
                             TotalSells = dataRow.Field<int>("NO_OF_COURSES_SELLS")
                         }).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorRecentClassroomJoin", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return responseData;
        }
        public async Task<List<ClassroomTransactions>> GetClassroomsTransactions(int InstructorId, MasterSearchRequest request)
        {
            List<ClassroomTransactions> responseData = null;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetInstructorClassroomPurchasesAsync(InstructorId,
                    request.m_iNoOfRowsFetched, request.m_iMaxRowsToFetch);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    responseData = ds.Tables[0].AsEnumerable().Select(
                         dataRow => new ClassroomTransactions()
                         {
                             m_strClassroomName = dataRow.Field<string>("CLASSROOM_NAME"),
                             m_llClassroomId = dataRow.Field<long>("CLASSROOM_ID"),
                             ClassroomJoiningInPaise = dataRow.Field<int>("CLASSROOM_CHARGE_IN_PAISE"),
                             TransactionDate = dataRow.Field<DateTime>("DATE_OF_PAYMENT").ToString("d MMM yyyy")
                         }).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorRecentClassroomJoin", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return responseData;
        }
        public async Task<StudentPublicProfileResponse> GetStudentPublicProfileResponse(long StudentId)
        {
            StudentPublicProfileResponse studentPublicProfileResponse = null;
            try
            {
                DataSet ds = await objCPDataService.GetStudentPublicProfileDetailsAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    studentPublicProfileResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentPublicProfileResponse(
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<DateTime>("JOINING_DATE").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_STUDENT_FOLLOWERS"),
                         dataRow.Field<int>("NO_OF_COURSES_JOINED"),
                         dataRow.Field<int>("LIVE_COURSE_JOINED"),
                         dataRow.Field<string>("PROFILE_URL"),
                         dataRow.Field<int>("NO_OF_LIVE_CLASSESS_ATTENDED"),
                         dataRow.Field<int>("NO_OF_INSTRUCTOR_FOLLOWING")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return studentPublicProfileResponse;
        }
        public async Task<bool> UpdateClassroomMeetingLink(long ClassroomId,string MeetingLink)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateClassroomMeetingLinkAsync(ClassroomId,MeetingLink);
             
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsClassroomAlreadyTakenoday", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateClassroomProjects(long ClassroomId, string classroomProjects)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateClassroomProjectsAsync(ClassroomId, classroomProjects);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateClassroomRequest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateClassroomHihlights(long ClassroomId, string highlight)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateClassroomHighlightsAsync(ClassroomId, highlight);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateClassroomRequest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
    }
}