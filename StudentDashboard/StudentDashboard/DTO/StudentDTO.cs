using StudentDashboard.BusinessLayer;
using StudentDashboard.HttpRequest;
using StudentDashboard.HttpResponse;
using StudentDashboard.HttpResponse.ClassRoom;
using StudentDashboard.Models;
using StudentDashboard.Models.Course;
using StudentDashboard.Models.Instructor;
using StudentDashboard.Models.Student;
using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using StudentDashboard.Views.Student;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.DTO
{
    public class StudentDTO
    {
        CPDataService.CpDataServiceClient objCPDataService;
        StringBuilder m_strLogMessage = new StringBuilder();
        public StudentDTO()
        {
            objCPDataService = new CPDataService.CpDataServiceClient();
            
        }
        public async Task<bool> RegisterNewStudent(StudentRegisterModal objStudentDetailsModal)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.RegisterNewStudentAsync(objStudentDetailsModal.m_strFirstName,objStudentDetailsModal.m_strLastName,
                               objStudentDetailsModal.m_strEmail,objStudentDetailsModal.m_strHashedPassword,objStudentDetailsModal.m_strPhoneNo,
                               objStudentDetailsModal.m_strPhoneNoVarificationGuid,objStudentDetailsModal.m_strEmailVarificationGuid);

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
        public long GetStudentIdFromUserId(string UserId)
        {
            long StudentId = -1;
            try
            {
                objCPDataService.GetStudentIdFromUserId(UserId,ref StudentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertActivityForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return StudentId;
        }
        public async Task<bool> InsertActivityForInstructor(long StudentId, string ActivityMessage)
        {
            bool result = false;
            try
            {
                result =await  objCPDataService.InsertActivityForStudentAsync( ActivityMessage,StudentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertActivityForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool ValidateLogin(string UserId,string HashedPassword,ref long StudentId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.ValidateStudentLogin(UserId,HashedPassword,ref StudentId);

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
        public async  Task<bool> ValidatePasswordRecodevrtOtp(string UserId, string Token,string OTP)
        {
            bool result = false;
            DateTime? TokenExpiryTime=null;
            try
            {
                DataSet ds = await objCPDataService.ValidateStudentPasswordRecoveryRequestAsync(UserId, Token, OTP);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    TokenExpiryTime = ds.Tables[0].AsEnumerable().Select(
                     dataRow => (dataRow.Field<DateTime?>("LAST_PASSWORD_RECOVERY_REQUEST_TIME"))).ToList()[0];
                }
                if(TokenExpiryTime!=null&&DateTime.Now-TokenExpiryTime>TimeSpan.FromSeconds(MvcApplication._forgotPasswordExpiryTimeInMinutes))
                {
                    result = true;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ValidatePasswordRecodevrtOtp", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertPasswordRecovery(string UserId,string Token,string OTP)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertStudentPasswordRecoveryRequestAsync(UserId, Token, OTP);

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
        public async Task<bool> MarkOtpVerifiedForPasswordRecodevry(string UserId, string Token)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.MarkOtpVarifiedAsync(UserId, Token);

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
        public async Task<bool> UpdateStudentPasswordAfterAuth(string UserId, string Token, string HashedPasword)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.ChanegPasswordAfterAuthenticationAsync(UserId, Token, HashedPasword);

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
        public async Task<bool> AddQuestionAskForCourse(StudentCourseQuestionModal objStudentCourseQuestionModal)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertCourseQuestionByStudentAsync(objStudentCourseQuestionModal.m_llCourseId,
                    objStudentCourseQuestionModal.m_llStudentId, objStudentCourseQuestionModal.m_strQuestionStatement
                    );
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AddQuestionAskForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<StudentCourseQuestionModal>> GetAllQuestionOfStudentCourse(long StudentId,long CourseId)
        {
            List<StudentCourseQuestionModal> lsCourseDetailsModel = null;
            try
            {
                DataSet ds = await objCPDataService.GetAllQuestionAskForCourseByStudentAsync(StudentId, CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsCourseDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentCourseQuestionModal(
                         dataRow.Field<string>("QUESTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("dd MMM yyy HH:mm:ss")
                         )).ToList();
                }
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
        public async Task<List<CourseDetailsModel>> SearchForCourse(string SearchString,int MaxRowToReturn,int NoOfRowsFetched,int SortingTypeId)
        {
            List<CourseDetailsModel> lsCourseDetailsModel = null;
            try
            {
                DataSet ds  =await  objCPDataService.SearchForCourseAsync(SearchString,MaxRowToReturn, NoOfRowsFetched, SortingTypeId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsCourseDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CourseDetailsModel(
                         dataRow.Field<long>("COURSE_ID"),
                         dataRow.Field<string>("COURSE_NAME"),
                         dataRow.Field<string>("COURSE_DESCRIPTION"),
                         dataRow.Field<DateTime>("COURSE_ACTIVATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                         dataRow.Field<string>("INSTRUCTOR_NAME")
                         )).ToList();
                }
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
        public async Task<List<CourseDetailsModel>> SearchForCourse(string SearchString, int MaxRowToReturn, int NoOfRowsFetched, int SortingTypeId,long StudentId)
        {
            List<CourseDetailsModel> lsCourseDetailsModel = null;
            try
            {
                DataSet ds = await objCPDataService.SearchForCourseForStudentAsync(SearchString, MaxRowToReturn, NoOfRowsFetched, SortingTypeId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsCourseDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CourseDetailsModel(
                         dataRow.Field<long>("COURSE_ID"),
                         dataRow.Field<string>("COURSE_NAME"),
                         dataRow.Field<string>("COURSE_DESCRIPTION"),
                         dataRow.Field<DateTime>("COURSE_ACTIVATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                         dataRow.Field<string>("INSTRUCTOR_NAME")
                         )).ToList();
                }
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
        public async Task<StudentRegisterModal> GetStudentDetails(long StudentId)
        {
            StudentRegisterModal objStudentRegisterModal = null;
            try
            {
                DataSet ds =await objCPDataService.GetStudentDetailsAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objStudentRegisterModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentRegisterModal(
                         dataRow.Field<string>("FIRST_NAME"),
                         dataRow.Field<string>("LAST_NAME"),
                         dataRow.Field<string>("STUDENT_USER_ID"),
                         dataRow.Field<string>("PHONENO"),
                         dataRow.Field<string>("ADDRESS_LINE_1"),
                         dataRow.Field<string>("ADDRESS_LINE_2"),
                         dataRow.Field<string>("CITY_NAME"),
                         dataRow.Field<string>("STATE_NAME"),
                         dataRow.Field<string>("PIN_CODE"),
                         dataRow.Field<string>("GENDER"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy")
                         )).ToList()[0];
                }
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
                result = await objCPDataService.UpdateStudentDetailsAsync(objStudentRegisterModal.m_strFirstName, objStudentRegisterModal.m_strLastName,
                            objStudentRegisterModal.m_strAddressLine1,objStudentRegisterModal.m_strAddressLine2,objStudentRegisterModal.m_strPinCode,
                            objStudentRegisterModal.m_iStateId,objStudentRegisterModal.m_iStateId,objStudentRegisterModal.m_strGender);

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
        public async Task<bool> JoinStudentToCourse(long CourseId,long StudentId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.JoinStudentToCourseAsync(CourseId,StudentId);
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
        public async Task<List<SearchInstructorResponseModal>> SearchForInstructor(string SearchString,int MaxRowToReturn )
        {
            List<SearchInstructorResponseModal> lsSearchInstructorResponseModal = null;
            try
            {
                DataSet ds =await objCPDataService.SearchForInstructorAsync(SearchString, MaxRowToReturn);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsSearchInstructorResponseModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new SearchInstructorResponseModal(
                         dataRow.Field<int>("ID"),
                         dataRow.Field<string>("INSTRUCTOR_FIRST_NAME"),
                         dataRow.Field<string>("INSTRUCTOR_LAST_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                          dataRow.Field<int>("NO_OF_COURSE_CREATED"),
                          dataRow.Field<int>("NO_OF_STUDENTS_JOINED")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsSearchInstructorResponseModal;
        }
        public async Task<List<StudentJoinedCoursesResponseModal>> SerachForJoinedCourses(long StudentId,string SearchString, int MaxRowToReturn)
        {
            List<StudentJoinedCoursesResponseModal> lsStudentJoinedCoursesResponseModal = null;
            try
            {
                DataSet ds =await objCPDataService.GetJoinedCoursesForStudentAsync(StudentId,SearchString, MaxRowToReturn);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsStudentJoinedCoursesResponseModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentJoinedCoursesResponseModal(
                         dataRow.Field<long>("COURSE_ID"),
                         dataRow.Field<string>("COURSE_NAME"),
                         dataRow.Field<string>("COURSE_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                          dataRow.Field<int>("COURSE_PROGRESS")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsStudentJoinedCoursesResponseModal;
        }
        public async Task<bool> JoinStudentToInstructor(long StudentId,int InstructorId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.JoinStudentToInstructorAsync(StudentId, InstructorId);
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
        public async Task<bool> InsertReadTopicByStudent(long StudentId, long TopicId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertCompletedTopicforStudentAsync(TopicId, StudentId);
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
        public async Task<List<SearchInstructorResponseModal>> GetAllInstructorJoinedForStudent(long StudentId,string SearchString, int MaxRowToReturn)
        {
            List<SearchInstructorResponseModal> lsSearchInstructorResponseModal = null;
            try
            {
                DataSet ds =await objCPDataService.GetAllJoinedInstructorForStudentAsync(StudentId,SearchString, MaxRowToReturn);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsSearchInstructorResponseModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new SearchInstructorResponseModal(
                         dataRow.Field<int>("INSTRUCTOR_ID"),
                         dataRow.Field<string>("INSTRUCTOR_FIRST_NAME"),
                         dataRow.Field<string>("INSTRUCTOR_LAST_NAME"),
                         dataRow.Field<DateTime>("JOIN_DATE").ToString("d MMM yyyy"),
                          dataRow.Field<int>("NO_OF_COURSE_CREATED"),
                          dataRow.Field<int>("NO_OF_STUDENTS_JOINED")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
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
                DataSet ds = await objCPDataService.SearchForAssignmentAsync(SearchString, MaxRowToReturn);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsAssignmentDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AssignmentDetailsModel(
                         dataRow.Field<long>("ASSIGNMENT_ID"),
                         dataRow.Field<string>("ASSIGNMENT_NAME"),
                         dataRow.Field<string>("ASSIGNMENT_DESCRIPTION"),
                         dataRow.Field<byte>("ASSIGNMENT_TYPE"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_QUESTIONS"),
                          dataRow.Field<int>("NO_OF_SUBJECTIVE_QUESTIONS")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentDetailsModel;
        }
        public  bool InserAssignmentResponse(AssignmentSubmissionRequest objAssignmentSubmissionRequest)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertAssignmentResponse(objAssignmentSubmissionRequest.m_llStudentId, objAssignmentSubmissionRequest.m_llAssignmentId,
                           objAssignmentSubmissionRequest.m_dtStartTime, objAssignmentSubmissionRequest.m_dtFinishTime, objAssignmentSubmissionRequest.m_strResponse,
                           objAssignmentSubmissionRequest.m_iPercentageScore, objAssignmentSubmissionRequest.m_iTotalNoOfQuestions, ref objAssignmentSubmissionRequest.m_llSubmissionId);
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
        public async Task<List<TestDetailsModel>> SearchForTest(string SearchString, int MaxRowToReturn)
        {
            List<TestDetailsModel> lsTestDetailsModel = null;
            try
            {
                DataSet ds = await objCPDataService.SearchForTestAsync(SearchString, MaxRowToReturn);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsTestDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new TestDetailsModel(
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<string>("TEST_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_QUESTIONS"),
                         dataRow.Field<byte>("TEST_TYPE")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsTestDetailsModel;
        }
        public async Task<List<AssignmentsSubmissionModal>> GetAllAssignmentSubmissions(long StudentId)
        {
            List<AssignmentsSubmissionModal> lsAssignmentsSubmissionOfStudentResponse = null;
            try
            {
                DataSet ds = await objCPDataService.GetAllAssignmentSubmissionsForStudentAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsAssignmentsSubmissionOfStudentResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AssignmentsSubmissionModal(
                         dataRow.Field<long>("SUBMISSION_ID"),
                         dataRow.Field<int>("TOTAL_NO_OF_QUESTIONS"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("ASSIGNMANT_PERCENTAGE_SCORE"),
                         dataRow.Field<string>("ASSIGNMENT_NAME")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllAssignmentSubmissions", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentsSubmissionOfStudentResponse;
        }
        public async Task<GetAssignmentSubssionDetials> GetAssignmentResponse(long Submissionid,long StudentId)
        {
            GetAssignmentSubssionDetials objGetAssignmentSubssionDetials=null;
            try
            {

                DataSet ds = await objCPDataService.GetAssignmentResponseAsync(Submissionid, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objGetAssignmentSubssionDetials = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new GetAssignmentSubssionDetials(
                         dataRow.Field<string>("ASSIGNMENT_NAME"),
                         dataRow.Field<string>("ASSIGNMENT_RESPONSE_FOR_MCQ"),
                         dataRow.Field<DateTime>("ASSIGNMENT_START_TIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ASSIGNMENT_START_TIME"),
                          dataRow.Field<DateTime>("ASSIGNMENT_FINISH_TIME")
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
            return objGetAssignmentSubssionDetials;
        }
        public bool InsertTestResponse(TestSubmissionRequest objTestSubmissionRequest)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertTestResponse(objTestSubmissionRequest.m_llStudentId, objTestSubmissionRequest.m_llTestId,
                           objTestSubmissionRequest.m_dtStartTime, objTestSubmissionRequest.m_dtFinishTime, objTestSubmissionRequest.m_strResponse,
                           objTestSubmissionRequest.m_iPercentageScore, objTestSubmissionRequest.m_iTotalNoOfQuestions, ref objTestSubmissionRequest.m_llSubmissionId);
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
        public async Task<List<TestSubmissionModal>> GetAllTestSubmissions(long StudentId)
        {
            List<TestSubmissionModal> lsTestSubmissionModal = null;
            try
            {
                DataSet ds = await objCPDataService.GetAllTestSubmissionsForStudentAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsTestSubmissionModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new TestSubmissionModal(
                         dataRow.Field<long>("SUBMISSION_ID"),
                         dataRow.Field<int>("TOTAL_NO_OF_QUESTIONS"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("TEST_SCORE"),
                         dataRow.Field<string>("TEST_NAME")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllAssignmentSubmissions", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsTestSubmissionModal;
        }
        public async Task< GetTestSubmissionDetailsResponse> GetTestResponse(long Submissionid,long StudentId)
        {
            GetTestSubmissionDetailsResponse objGetTestSubmissionDetailsResponse = null;
            try
            {
                DataSet ds =await objCPDataService.GetTestResponseAsync(Submissionid,StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objGetTestSubmissionDetailsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new GetTestSubmissionDetailsResponse(
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<string>("TEST_RESPONSE"),
                         dataRow.Field<DateTime>("TEST_START_TIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("TEST_START_TIME"),
                          dataRow.Field<DateTime>("TEST_FINISH_TIME")
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
            return objGetTestSubmissionDetailsResponse;
        }
        public async Task<StudentHomeModal> GetStudentHomeDetails(long StudentId)
        {
            StudentHomeModal objStudentHomeModal = null;
            try
            {

                DataSet ds = await objCPDataService.GetStudentHomeDetailsAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objStudentHomeModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentHomeModal(
                         dataRow.Field<int>("COURSES_JOINED"),
                         dataRow.Field<int>("COURSES_COMPLETED"),
                         dataRow.Field<int>("ASSIGNMENTS_SUBMITTED"),
                         dataRow.Field<int>("TESTS_SUBMITTED")
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
            return objStudentHomeModal;
        }
        public async Task<bool> CheckIsStudentHasJoinedTheCourse(long StudentId,long CourseId)
        {
            bool result=false;   
            try
            {

                DataSet ds = await objCPDataService.CheckStudentHasJoinedTheCourseAsync(StudentId, CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasJoinedTheCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckIsStudentHasSubmittedTheTest(long StudentId, long TestId)
        {
            bool result = false;
            try
            {

                DataSet ds = await objCPDataService.CheckStudentHasSubmittedTheTestAsync(StudentId, TestId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasSubmittedTheTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckIsStudentHasSubmittedTheAssignment(long StudentId, long AssignmentId)
        {
            bool result = false;
            try
            {

                DataSet ds = await objCPDataService.CheckStudentHasSubmittedTheAssignmentAsync(StudentId, AssignmentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasSubmittedTheTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckIsTestSubmissionIdExsitsForStudent(long StudentId, long SubmissionId)
        {
            bool result = false;
            try
            {

                DataSet ds = await objCPDataService.CheckTestResponseIdExistsForStudentAsync(StudentId, SubmissionId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsTestSubmissionIdExsitsForStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckIsAssignmentSubmissionIdExsitsForStudent(long StudentId, long ResponseId)
        {
            bool result = false;
            try
            {
         
                DataSet ds = await objCPDataService.CheckAssignmentResponseIdExistsForStudentAsync(StudentId, ResponseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasSubmittedTheTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<InstructorProfileDetailsModal> GetInstructorProfileDetails(int InstructorId)
        {
            InstructorProfileDetailsModal result = null;
            try
            {

                DataSet ds = await objCPDataService.GetInstructorProfileDetailsAsync(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new InstructorProfileDetailsModal(
                         dataRow.Field<string>("INSTRUCTOR_FULL_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_COURSES_CREATED"),
                         dataRow.Field<int>("NO_OF_ASSIGNMENTS_CREATED"),
                         dataRow.Field<int>("NO_OF_TESTS_CREATED"),
                         dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                         dataRow.Field<int>("NO_OF_STUDENTS_JOINED_THE_COURSE")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasSubmittedTheTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<CourseDetailsModel>> GetAllCourseDetailsForInstructor(int InstructorId)
        {
            List<CourseDetailsModel> result = null;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetAllCourseAsync(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CourseDetailsModel(
                         dataRow.Field<long>("COURSE_ID"),
                         dataRow.Field<string>("COURSE_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("TOTAL_INDEXES"),
                          dataRow.Field<string>("COURSE_STATUS_NAME"),
                           dataRow.Field<int>("NO_OF_STUDENTS_JOINED")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllCourseDetailsForInstructor", Ex.ToString());
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
                         dataRow.Field<int>("NO_OF_STUDENTS_JOINED").ToString(),
                          dataRow.Field<bool?>("IS_MEETING_ACTIVE")
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
        public async Task<bool> JoinClassroom(long ClassroomId, long StudentId)
        {
            bool result = false;
            try
            {

                result = await objCPDataService.JoinStudentToClassroomAsync(ClassroomId,StudentId);
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasSubmittedTheTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> JoinClassroomMeeting(long MeetingId, long StudentId)
        {
            bool result = false;
            try
            {

                result = await objCPDataService.JoinStudentToMeetingAsync(MeetingId, StudentId);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "JoinClassroomMeeting", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<StudentClassroomModal>> GetJoinedClassroom(long StudentId)
        {
            List<StudentClassroomModal> objClassRoomModal = new List<StudentClassroomModal>();
            try
            {
                DataSet ds = await objCPDataService.GetJoinedClassroomForStudentAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objClassRoomModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentClassroomModal(
                         dataRow.Field<long>("CLASSROOM_ID"),
                         dataRow.Field<string>("CLASSROOM_NAME"),
                          dataRow.Field<string>("CLASSROOM_DESCRIPTION"),
                         dataRow.Field<DateTime>("JOINING_DATE").ToString("d MMM yyyy")
                         
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
            return objClassRoomModal;
        }
        public async Task<JitsiMeetingModal> GetClassroomMeetingDetails(long ClassroomId)
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
                         dataRow.Field<string>("CLASSROOM_NAME")
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
            return objJitsiMeetingModal;
        }
        public async Task<bool> InsertNewMessageToClassroomByStudent(InsertStudentMessageToClassroom insertStudentMessageToClassroom)
        {
            bool result = false;
            try
            {

                result = await objCPDataService.InsertNewStudentClassroomMessageAsync(insertStudentMessageToClassroom.m_llClassroomId,
                    insertStudentMessageToClassroom.m_strMessage, insertStudentMessageToClassroom.m_llStudentId);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "JoinClassroomMeeting", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckStudentAccessToClassroom(long StudentId, long ClassroomId)
        {
            bool result = false;
            try
            {

                DataSet ds = await objCPDataService.CheckStudentClassroomAccessAsync(ClassroomId,StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasJoinedTheCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<ClassroomStudentMessageModal>> GetAllClassroomMessageForInstructor(long ClassroomId,long StudentId)
        {
            List<ClassroomStudentMessageModal> lsResponse = new List<ClassroomStudentMessageModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomMessageAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomStudentMessageModal(
                         dataRow.Field<string>("MESSAGE"),
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("f"),
                         dataRow.Field<long>("MESSAGE_ID"),
                         dataRow.Field<bool>("IS_INSTRUCTOR"),
                         dataRow.Field<long?>("STUDENT_ID") != null && dataRow.Field<long?>("STUDENT_ID") == StudentId ? true : false
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
            return lsResponse;
        }
        public async Task<List<StudentClassroomAssignmentModal>> GetAllClassroomAssignment(long ClassroomId, long StudentId)
        {
            List<StudentClassroomAssignmentModal> lsResponse = new List<StudentClassroomAssignmentModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomAssignmntForStudentAsync(ClassroomId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentClassroomAssignmentModal(
                         dataRow.Field<long>("ASSIGNMENT_ID"),
                         dataRow.Field<string>("ASSIGNMENT_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<long?>("SUBMISSION_ID"),
                         dataRow.Field<int?>("ASSIGNMANT_PERCENTAGE_SCORE"),
                         dataRow.Field<string>("ASSIGNMENT_TYPE_NAME"),
                         dataRow.Field<int>("NO_OF_MCQ_QUESTIONS")+ dataRow.Field<int>("NO_OF_SUBJECTIVE_QUESTIONS"),
                         dataRow.Field<string>("SHARE_CODE")
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
            return lsResponse;
        }
        public async Task<List<StudentClassroomAssignmentModal>> GetAllClassroomSubmittedAssignment(long ClassroomId, long StudentId)
        {
            List<StudentClassroomAssignmentModal> lsResponse = new List<StudentClassroomAssignmentModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomAssignmentSubmissionsForStudentAsync(ClassroomId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentClassroomAssignmentModal(
                          dataRow.Field<string>("ASSIGNMENT_NAME"),
                         dataRow.Field<long>("SUBMISSION_ID"),
                         dataRow.Field<DateTime>("SUBMISSION_DATE").ToString("d MMM yyyy"),
                         dataRow.Field<int?>("ASSIGNMANT_PERCENTAGE_SCORE"),
                         dataRow.Field<int>("TOTAL_NO_OF_QUESTIONS")
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
            return lsResponse;
        }
        public async Task<List<StudentClassroomTestModal>> GetAllClassroomTest(long ClassroomId, long StudentId)
        {
            List<StudentClassroomTestModal> lsResponse = new List<StudentClassroomTestModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomTestForStudentAsync(ClassroomId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentClassroomTestModal(
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<long?>("SUBMISSION_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<string>("TEST_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_QUESTIONS"),
                         dataRow.Field<string>("TEST_TYPE_NAME"),
                         dataRow.Field<string>("SHARE_CODE")
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
            return lsResponse;
        }
        public async Task<List<StudentClassroomTestModal>> GetAllClassroomTestSubmissons(long ClassroomId, long StudentId)
        {
            List<StudentClassroomTestModal> lsResponse = new List<StudentClassroomTestModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomTestSubmissionsForStudentAsync(ClassroomId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentClassroomTestModal(
                         dataRow.Field<long>("SUBMISSION_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<DateTime>("SUBMISSION_DATE").ToString("d MMM yyyy"),
                         dataRow.Field<int>("TEST_SCORE"),
                         dataRow.Field<int>("PERCENTAGE_SCORE")
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
            return lsResponse;
        }
        public async Task<StudentClassroomHomeDetails> GetStudentClassroomHomeDetails(long ClassroomId, long StudentId)
        {
            StudentClassroomHomeDetails studentClassroomHomeDetails = null;
            try
            {
                DataSet ds = await objCPDataService.GetClassroomHomeDetailsForStudentAsync(ClassroomId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    studentClassroomHomeDetails = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentClassroomHomeDetails(
                         ClassroomId,
                         dataRow.Field<int>("NO_OF_MEETINGS_JOINED"),
                         dataRow.Field<int>("NO_OF_ASSIGNMENTS_SUBMITTED"),
                         dataRow.Field<int>("NO_OF_TESTS_SUBMITTED")
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
            return studentClassroomHomeDetails;
        }
    }
}