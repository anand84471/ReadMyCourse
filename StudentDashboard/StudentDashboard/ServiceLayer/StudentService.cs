using Newtonsoft.Json;
using StudentDashboard.AlertManager;
using StudentDashboard.BusinessLayer;
using StudentDashboard.DTO;
using StudentDashboard.HttpRequest;
using StudentDashboard.HttpResponse;
using StudentDashboard.HttpResponse.ClassRoom;
using StudentDashboard.JsonSerializableObject;
using StudentDashboard.Models.Course;
using StudentDashboard.Models.Instructor;
using StudentDashboard.Models.Student;
using StudentDashboard.Utilities;
using StudentDashboard.Views.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace StudentDashboard.ServiceLayer
{
    public class StudentService
    {
        StudentDTO objStudentDTO;
        StringBuilder m_strLogMessage;
        InstructorAlertManager objInstructorAlertManager;
        InstructorBusinessLayer objInstructorBusinessLayer;
        SMSServiceManager objSMSServiceManager;
        public StudentService()
        {
            objStudentDTO = new StudentDTO();
            m_strLogMessage = new StringBuilder();
            objInstructorAlertManager = new InstructorAlertManager();
            objInstructorBusinessLayer = new InstructorBusinessLayer();
            objSMSServiceManager = new SMSServiceManager();
        }
        public StudentDetailsModel GetStudentDetails(int StudentId)
        {
            return new StudentDetailsModel();
        }
        public async Task<bool> RegisterNewStudent(StudentRegisterModal objStudentRegisterModal)
        {
            bool result = false;
            try
            {
                string EncryptedPassword = SHA256Encryption.ComputeSha256Hash(objStudentRegisterModal.m_strPassword);
                objStudentRegisterModal.m_strHashedPassword = EncryptedPassword;
                result = await objStudentDTO.RegisterNewStudent(objStudentRegisterModal);
                objStudentRegisterModal.m_strPhoneNoVarificationGuid = objInstructorBusinessLayer.GetSmsVerificationString();
                objStudentRegisterModal.m_strEmailVarificationGuid = objInstructorBusinessLayer.GetEmailVerficationString();
                if (await objStudentDTO.RegisterNewStudent(objStudentRegisterModal))
                {
                    result = true;
                    var SmsVarificationLink = objInstructorBusinessLayer.GetLinkForSmsVarification(objStudentRegisterModal.m_strEmailVarificationGuid,
                        objStudentRegisterModal.m_strEmail, Constants.SMS_VERIFICATION_LINK_TYPE_ID_FOR_STUDENT);
                    await objSMSServiceManager.SendInstructorPhoneNoVarification(SmsVarificationLink, "+91" + objStudentRegisterModal.m_strPhoneNo);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewUser", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;

        }
        public async Task<string> InsertPasswordRecovery(string StudentUserId)
        {
            string AuthToken=null;
            try
            {
                var StudentId = objStudentDTO.GetStudentIdFromUserId(StudentUserId);
                if (StudentId != -1)
                {
                    StudentRegisterModal objStudentRegisterModal = await objStudentDTO.GetStudentDetails(StudentId);
                    if(objStudentRegisterModal!=null)
                    {
                        string OTP = objInstructorBusinessLayer.GenerateOtp();
                        AuthToken = objInstructorBusinessLayer.GeneratePasswordVeryficationToken();
                        await objSMSServiceManager.SendStudentPasswordRecoveryOTP(OTP, "+91" + objStudentRegisterModal.m_strPhoneNo);
                        await objStudentDTO.InsertPasswordRecovery(StudentUserId, AuthToken, OTP);
                    }
                }
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertPasswordRecovery", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return AuthToken;
        }
        
        public bool ValidateLogin(StudentRegisterModal objStudentRegisterModal)
        {
            bool result = false;
            try
            {
                string EncryptedPassword = SHA256Encryption.ComputeSha256Hash(objStudentRegisterModal.m_strPassword);
                objStudentRegisterModal.m_strHashedPassword = EncryptedPassword;
                long StudentId = -1;
                result = objStudentDTO.ValidateLogin(objStudentRegisterModal.m_strUserId,objStudentRegisterModal.m_strHashedPassword,ref StudentId);
                if(result&&StudentId!=-1)
                {
                    objStudentRegisterModal.m_llStudentId = StudentId;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ValidateLogin", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;

        }
        public async Task<bool> ChangePasswordAfterAuth(StudentUpdatePasswordRequestModal objStudentUpdatePasswordRequestModal)
        {

            bool result = false;
            try
            {
                if(objStudentUpdatePasswordRequestModal.m_strPassword.Equals(objStudentUpdatePasswordRequestModal.m_strMatchPassword))
                {
                    objStudentUpdatePasswordRequestModal.m_strHashedPassword = SHA256Encryption.ComputeSha256Hash(objStudentUpdatePasswordRequestModal.m_strPassword);
                    result = await objStudentDTO.UpdateStudentPasswordAfterAuth(objStudentUpdatePasswordRequestModal.m_strUserName,
                        objStudentUpdatePasswordRequestModal.m_strToken, objStudentUpdatePasswordRequestModal.m_strHashedPassword);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> ValidatePasswordRecodevrtOtp(StudentUpdatePasswordRequestModal objStudentUpdatePasswordRequestModal)
        {

            bool result = false;
            try
            {
                if(await objStudentDTO.ValidatePasswordRecodevrtOtp(objStudentUpdatePasswordRequestModal.m_strUserName,
                    objStudentUpdatePasswordRequestModal.m_strToken, objStudentUpdatePasswordRequestModal.m_strOtp))
                {
                    result = await objStudentDTO.MarkOtpVerifiedForPasswordRecodevry(objStudentUpdatePasswordRequestModal.m_strUserName,
                        objStudentUpdatePasswordRequestModal.m_strToken);
                }
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> AddQuestionAskForCourse(StudentCourseQuestionModal objStudentCourseQuestionModal)
        {
            return await objStudentDTO.AddQuestionAskForCourse(objStudentCourseQuestionModal);
        }
        public async Task<List<CourseDetailsModel>> SearchForCourse(string SearchString, int MaxRowToReturn,int NoOfRowseFetched,int SortingId)
        {
            List<CourseDetailsModel> lsCourseDetailsModel = null;
            try
            {
                lsCourseDetailsModel = await objStudentDTO.SearchForCourse(SearchString, MaxRowToReturn, NoOfRowseFetched, SortingId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsCourseDetailsModel;
        }
        public async Task<List<StudentCourseQuestionModal>> GetAllQuestionOfStudentCourse(long StudentId, long CourseId)
        {

            List<StudentCourseQuestionModal> lsStudentCourseQuestionModal = null;
            try
            {
                lsStudentCourseQuestionModal = await objStudentDTO.GetAllQuestionOfStudentCourse(StudentId, CourseId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsStudentCourseQuestionModal;
        }
        public async Task<List<CourseDetailsModel>> SearchForCourseForStudent(string SearchString, int MaxRowToReturn, int NoOfRowseFetched, int SortingId,long Studentid)
        {
            List<CourseDetailsModel> lsCourseDetailsModel = null;
            try
            {
                lsCourseDetailsModel =await objStudentDTO.SearchForCourse(SearchString, MaxRowToReturn, NoOfRowseFetched, SortingId, Studentid);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForCourseForStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsCourseDetailsModel;
        }
        public async Task<StudentRegisterModal> GetStudentDetails(long StudentId)
        {
            StudentRegisterModal objStudentRegisterModal = null;
            try
            {
                objStudentRegisterModal =await objStudentDTO.GetStudentDetails(StudentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetStudentDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objStudentRegisterModal;
        }
        public async Task<bool> UpdateStudentDetails(StudentRegisterModal objStudentRegisterModal)
        {
            bool result = false;
            try
            {
                result = await objStudentDTO.UpdateStudentDetails(objStudentRegisterModal);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateStudentDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> JoinStudentToCourse(long CourseId, long StudentId)
        {
            bool result = false;
            try
            {
                result =await objStudentDTO.JoinStudentToCourse(CourseId, StudentId);
                if(result)
                {
                    objInstructorAlertManager.AddCourseJoinAlert(StudentId, CourseId);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "JoinStudentToCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertReadTopicByStudent(long StudentId, long TopicId)
        {
            bool result = false;
            try
            {
                result = await objStudentDTO.InsertReadTopicByStudent(StudentId, TopicId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertReadTopicByStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> JoinStudentToInstructor(long StudentId, int InstructorId)
        {
            bool result = false;
            try
            {
                result = await objStudentDTO.JoinStudentToInstructor(StudentId, InstructorId);
                if(result)
                {
                    objInstructorAlertManager.AddStudentJoinAlert(StudentId, InstructorId);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "JoinStudentToInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }

        public async Task<List<StudentJoinedCoursesResponseModal>> SerachForJoinedCourses(long StudentId, string SearchString, int MaxRowToReturn)
        {
            List<StudentJoinedCoursesResponseModal> lsStudentJoinedCoursesResponseModal = null;
            try
            {
                lsStudentJoinedCoursesResponseModal =await objStudentDTO.SerachForJoinedCourses(StudentId, SearchString, MaxRowToReturn);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SerachForJoinedCourses", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsStudentJoinedCoursesResponseModal;
        }
        public async Task<List<SearchInstructorResponseModal>> SearchForInstructor(string SearchString, int MaxRowToReturn)
        {
            List<SearchInstructorResponseModal> lsSearchInstructorResponseModal = null;
            try
            {
                lsSearchInstructorResponseModal = await objStudentDTO.SearchForInstructor(SearchString, MaxRowToReturn);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsSearchInstructorResponseModal;
        }
        public async Task<List<SearchInstructorResponseModal>> SearchForJoinedInstructor(long StudentId,string SearchString, int MaxRowToReturn)
        {
            List<SearchInstructorResponseModal> lsSearchInstructorResponseModal = null;
            try
            {
                lsSearchInstructorResponseModal =await objStudentDTO.GetAllInstructorJoinedForStudent(StudentId,SearchString, MaxRowToReturn);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForJoinedInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsSearchInstructorResponseModal;
        }
        public async Task<List<AssignmentDetailsModel>> SearchForAssignment(string SearchString, int MaxRowToReturn)
        {
            List<AssignmentDetailsModel> lsAssignmentDetailsModel = null;
            try
            {
                lsAssignmentDetailsModel = await objStudentDTO.SearchForAssignment(SearchString, MaxRowToReturn);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentDetailsModel;
        }
        public async Task<List<TestDetailsModel>> SearchForTest(string SearchString, int MaxRowToReturn)
        {
            List<TestDetailsModel> lsAssignmentDetailsModel = null;
            try
            {
                lsAssignmentDetailsModel = await objStudentDTO.SearchForTest(SearchString, MaxRowToReturn);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentDetailsModel;
        }
        public bool InserAssignmentResponse(AssignmentSubmissionRequest objAssignmentSubmissionRequest)
        {
            bool result = false;
            try
            {
                objAssignmentSubmissionRequest.ProcessRequest();
                result = objStudentDTO.InserAssignmentResponse(objAssignmentSubmissionRequest);
                if(result)
                {
                    objInstructorAlertManager.AddAssignmentSubmissionAlert(objAssignmentSubmissionRequest.m_llStudentId, objAssignmentSubmissionRequest.m_llAssignmentId);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InserAssignmentResponse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertTestResponse(TestSubmissionRequest objTestSubmissionRequest)
        {
            bool result = false;
            try
            {
                objTestSubmissionRequest.ProcessRequest();
                result = objStudentDTO.InsertTestResponse(objTestSubmissionRequest);
                if(result)
                {
                    objInstructorAlertManager.AddTestSubmissionAlert(objTestSubmissionRequest.m_llStudentId, objTestSubmissionRequest.m_llTestId);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InserAssignmentResponse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<AssignmentsSubmissionModal>> GetAllAssignmentSubmissions(long StudentId)
        {
            List<AssignmentsSubmissionModal> lsAssignmentsSubmissionOfStudentResponse = null;
            try
            {
                lsAssignmentsSubmissionOfStudentResponse = await objStudentDTO.GetAllAssignmentSubmissions(StudentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentsSubmissionOfStudentResponse;
        }
        public async Task<List<TestSubmissionModal>> GetAllTestSubmissions(long StudentId)
        {
            List<TestSubmissionModal> lsTestSubmissionModal = null;
            try
            {
                lsTestSubmissionModal = await objStudentDTO.GetAllTestSubmissions(StudentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllTestSubmissions", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsTestSubmissionModal;
        }
        private List<AssignmentQuestionResponse> ConvertFromJsonObjectToAssignmentResponse(List<AssignmentSubmissionResponseJsonSerializable> lsAssignmentSubmissionResponseJsonSerializable)
        {
            List<AssignmentQuestionResponse> lsAssignmentQuestionResponse = new List<AssignmentQuestionResponse>();
            AssignmentQuestionResponse objAssignmentQuestionResponse;
            foreach (var obj in lsAssignmentSubmissionResponseJsonSerializable)
            {
                objAssignmentQuestionResponse = new AssignmentQuestionResponse();
                objAssignmentQuestionResponse.m_CorrectOption = obj.m_CorrectOption;
                objAssignmentQuestionResponse.m_iOptionSelected = obj.m_iOptionSelected;
                objAssignmentQuestionResponse.m_strOption1 = obj.m_strOption1;
                objAssignmentQuestionResponse.m_strOption2 = obj.m_strOption2;
                objAssignmentQuestionResponse.m_strOption3 = obj.m_strOption3;
                objAssignmentQuestionResponse.m_strOption4 = obj.m_strOption4;
                objAssignmentQuestionResponse.m_strQuestionStatement = obj.m_strQuestionStatement;
                objAssignmentQuestionResponse.m_llQuestionId = obj.m_llQuestionId;
                lsAssignmentQuestionResponse.Add(objAssignmentQuestionResponse);
            }
            return lsAssignmentQuestionResponse;
        }
        private List<TestQuestionResponse> ConvertFromJsonObjectToTestResponse(List<TestSubmissionResponseJsonSerializable> lsAssignmentSubmissionResponseJsonSerializable)
        {
            List<TestQuestionResponse> lsTestQuestionResponse = new List<TestQuestionResponse>();
            TestQuestionResponse objTestQuestionResponse;
            foreach (var obj in lsAssignmentSubmissionResponseJsonSerializable)
            {
                objTestQuestionResponse = new TestQuestionResponse();
                objTestQuestionResponse.m_CorrectOption = obj.m_CorrectOption;
                objTestQuestionResponse.m_iOptionSelected = obj.m_iOptionSelected;
                objTestQuestionResponse.m_strOption1 = obj.m_strOption1;
                objTestQuestionResponse.m_strOption2 = obj.m_strOption2;
                objTestQuestionResponse.m_strOption3 = obj.m_strOption3;
                objTestQuestionResponse.m_strOption4 = obj.m_strOption4;
                objTestQuestionResponse.m_strQuestionStatement = obj.m_strQuestionStatement;
                objTestQuestionResponse.m_llQuestionId = obj.m_llQuestionId;
                objTestQuestionResponse.m_iMarks = obj.m_iMarks;
                objTestQuestionResponse.m_iTimeInSeconds = obj.m_iTimeInSeconds;
                lsTestQuestionResponse.Add(objTestQuestionResponse);
            }
            return lsTestQuestionResponse;
        }
        public async Task<GetAssignmentSubssionDetials> GetAssignmentResponse(long SubmissionId,long StudentId)
        {
            GetAssignmentSubssionDetials objGetAssignmentSubssionDetials = null;
            try
            {
                objGetAssignmentSubssionDetials=await objStudentDTO.GetAssignmentResponse(SubmissionId, StudentId);
                if(objGetAssignmentSubssionDetials!=null)
                {
                    List<AssignmentSubmissionResponseJsonSerializable> lsAssignmentSubmissionResponseJsonSerializable= JsonConvert.DeserializeObject<List<AssignmentSubmissionResponseJsonSerializable>>(objGetAssignmentSubssionDetials.m_strResponse);

                    objGetAssignmentSubssionDetials .m_lsAssignmentQuestionResponse= ConvertFromJsonObjectToAssignmentResponse(lsAssignmentSubmissionResponseJsonSerializable);
                }
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetStudentDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objGetAssignmentSubssionDetials;
        }
        public async Task<GetTestSubmissionDetailsResponse> GetTestResponse(long SubmissionId,long StudentId)
        {
            GetTestSubmissionDetailsResponse objGetTestSubmissionDetailsResponse = null;
            try
            {
                objGetTestSubmissionDetailsResponse =await  objStudentDTO.GetTestResponse(SubmissionId,StudentId);
                if (objGetTestSubmissionDetailsResponse != null)
                {
                    List<TestSubmissionResponseJsonSerializable> lsTestSubmissionResponseJsonSerializable = JsonConvert.DeserializeObject<List<TestSubmissionResponseJsonSerializable>>(objGetTestSubmissionDetailsResponse.m_strResponse);

                    objGetTestSubmissionDetailsResponse.m_lsTestQuestionResponse = ConvertFromJsonObjectToTestResponse(lsTestSubmissionResponseJsonSerializable);
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetStudentDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objGetTestSubmissionDetailsResponse;
        }
        public async Task<StudentHomeModal> GetStudentHomeDetails(long StudentId)
        {
            StudentHomeModal objStudentHomeModal = null;
            try
            {
                objStudentHomeModal = await objStudentDTO.GetStudentHomeDetails(StudentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetStudentDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objStudentHomeModal;
        }
        public async Task<bool> CheckIsStudentHasJoinedTheCourse(long StudentId, long CourseId)
        {
            return await objStudentDTO.CheckIsStudentHasJoinedTheCourse(StudentId, CourseId);
        }
        public async Task<bool> CheckIsStudentHasSubmittedTheTest(long StudentId, long TestId)
        {
            return await objStudentDTO.CheckIsStudentHasSubmittedTheTest(StudentId,TestId);
        }
        public async Task<bool> CheckIsStudentHasSubmittedTheAssignment(long StudentId, long AssignmentId)
        {
            return await objStudentDTO.CheckIsStudentHasSubmittedTheAssignment(StudentId, AssignmentId);
        }
        public async Task<bool> CheckIsTestSubmissionIdExsitsForStudent(long StudentId, long SubmissionId)
        {
            return await objStudentDTO.CheckIsTestSubmissionIdExsitsForStudent(StudentId, SubmissionId);
        }
        public async Task<bool> CheckIsAssignmentSubmissionIdExsitsForStudent(long StudentId, long SubmissionId)
        {
            return await objStudentDTO.CheckIsAssignmentSubmissionIdExsitsForStudent(StudentId, SubmissionId);
        }
        public async Task<InstructorProfileDetailsModal> GetInstructorProfileDetails(int InstructorId)
        {
            return await objStudentDTO.GetInstructorProfileDetails(InstructorId);
        }
        public async Task<List<CourseDetailsModel>> GetAllCourseDetailsForInstructor(int InstructorId)
        {
            return await objStudentDTO.GetAllCourseDetailsForInstructor(InstructorId);
        }
        public async Task<ClassRoomModal> GetClassroomDetailsForStudent(long ClassroomId)
        {
            return await objStudentDTO.GetClassroomDetailsForStudent(ClassroomId);
        }
        public async Task<bool> JoinClassroom(long ClassroomId, long StudentId)
        {
            return await objStudentDTO.JoinClassroom(ClassroomId, StudentId);
        }
        public async Task<bool> JoinClassroomMeeting(long MeetingId, long StudentId)
        {
            return await objStudentDTO.JoinClassroomMeeting(MeetingId, StudentId);
        }
        public async Task<List<StudentClassroomModal>> GetJoinedClassroom(long StudentId)
        {
            return await objStudentDTO.GetJoinedClassroom( StudentId);
        }
        public async Task<JitsiMeetingModal> GetClassroomMeetingDetails(long ClassroomId)
        {
            return await objStudentDTO.GetClassroomMeetingDetails(ClassroomId);
        }
        public async Task<bool> InsertNewMessageToClassroomByStudent(InsertStudentMessageToClassroom insertStudentMessageToClassroom)
        {
            return await objStudentDTO.InsertNewMessageToClassroomByStudent(insertStudentMessageToClassroom);
        }
        public async Task<bool> CheckStudentAccessToClassroom(long StudentId, long ClassroomId)
        {
            return await objStudentDTO.CheckStudentAccessToClassroom(StudentId, ClassroomId);
        }
        public async Task<List<ClassroomStudentMessageModal>> GetAllClassroomMessageForStudent(long ClassroomId, long StudentId)
        {
            return await objStudentDTO.GetAllClassroomMessageForInstructor(ClassroomId, StudentId);
        }
    }
}