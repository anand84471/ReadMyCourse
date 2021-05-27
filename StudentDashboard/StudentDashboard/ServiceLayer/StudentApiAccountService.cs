using Newtonsoft.Json;
using StudentDashboard.AlertManager;
using StudentDashboard.BusinessLayer;
using StudentDashboard.DTO;
using StudentDashboard.HttpRequest;
using StudentDashboard.HttpRequest.Student;
using StudentDashboard.HttpResponse;
using StudentDashboard.HttpResponse.ClassRoom;
using StudentDashboard.JsonSerializableObject;
using StudentDashboard.Models;
using StudentDashboard.Models.Base;
using StudentDashboard.Models.Classroom;
using StudentDashboard.Models.Course;
using StudentDashboard.Models.Instructor;
using StudentDashboard.Models.OAuth;
using StudentDashboard.Models.RazorPay;
using StudentDashboard.Models.Social;
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
    public class StudentApiAccountService
    {
        StudentAccountServiceRepo _repo;
        StringBuilder m_strLogMessage;
        InstructorAlertManager objInstructorAlertManager;
        InstructorBusinessLayer objInstructorBusinessLayer;
        SMSServiceManager objSMSServiceManager;
        public StudentApiAccountService()
        {
            _repo = new StudentAccountServiceRepo();
            m_strLogMessage = new StringBuilder();
            objInstructorAlertManager = new InstructorAlertManager();
            objInstructorBusinessLayer = new InstructorBusinessLayer();
            objSMSServiceManager = new SMSServiceManager();
        }
        public StudentDetailsModel GetStudentDetails(int StudentId)
        {
            return new StudentDetailsModel();
        }

         public async Task<bool> AddQuestionAskForCourse(StudentCourseQuestionModal objStudentCourseQuestionModal)
        {
            return await _repo.AddQuestionAskForCourse(objStudentCourseQuestionModal);
        }
        public async Task<List<CourseDetailsModel>> SearchForCourse(string SearchString, int MaxRowToReturn, int NoOfRowseFetched, int SortingId)
        {
            List<CourseDetailsModel> lsCourseDetailsModel = null;
            try
            {
                lsCourseDetailsModel = await _repo.SearchForCourse(SearchString, MaxRowToReturn, NoOfRowseFetched, SortingId);
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
                lsStudentCourseQuestionModal = await _repo.GetAllQuestionOfStudentCourse(StudentId, CourseId);
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
        public async Task<List<CourseDetailsModel>> SearchForCourseForStudent(string SearchString, int MaxRowToReturn, int NoOfRowseFetched, int SortingId, long Studentid)
        {
            List<CourseDetailsModel> lsCourseDetailsModel = null;
            try
            {
                lsCourseDetailsModel = await _repo.SearchForCourse(SearchString, MaxRowToReturn, NoOfRowseFetched, SortingId, Studentid);
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
                objStudentRegisterModal = await _repo.GetStudentDetails(StudentId);
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
                result = await _repo.UpdateStudentDetails(objStudentRegisterModal);
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
        public async Task<bool> JoinStudentToCourse(StudentCourseJoinRequest studentCourseJoinRequest)
        {
            bool result = false;
            try
            {
                studentCourseJoinRequest.razorPayPaymentResponseModal.m_strOrderId = studentCourseJoinRequest.m_strOrderId;
                if (objInstructorBusinessLayer.ValidateRazorPayPaymentRequest(studentCourseJoinRequest.razorPayPaymentResponseModal))
                {
                    result = await _repo.JoinStudentToCourse(studentCourseJoinRequest.m_llCourseId,
                   studentCourseJoinRequest.m_llStudentId);
                }
                if (result)
                {
                    await objInstructorAlertManager.AddCourseJoinAlert(studentCourseJoinRequest.m_llStudentId,
                        studentCourseJoinRequest.m_llCourseId);
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
                result = await _repo.InsertReadTopicByStudent(StudentId, TopicId);
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
                result = await _repo.JoinStudentToInstructor(StudentId, InstructorId);
                if (result)
                {
                    await objInstructorAlertManager.AddStudentJoinAlert(StudentId, InstructorId);
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
                lsStudentJoinedCoursesResponseModal = await _repo.SerachForJoinedCourses(StudentId, SearchString, MaxRowToReturn);
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
                lsSearchInstructorResponseModal = await _repo.SearchForInstructor(SearchString, MaxRowToReturn);
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
        public async Task<List<SearchInstructorResponseModal>> SearchForInstructorNew(string SearchString, int MaxRowToReturn, int NoOfRowsFetched, long StudentId)
        {
            List<SearchInstructorResponseModal> lsSearchInstructorResponseModal = null;
            try
            {
                lsSearchInstructorResponseModal = await _repo.SearchForInstructorNew(SearchString, MaxRowToReturn, NoOfRowsFetched, StudentId);
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
        public async Task<List<SearchInstructorResponseModal>> SearchForJoinedInstructor(long StudentId, string SearchString, int MaxRowToReturn)
        {
            List<SearchInstructorResponseModal> lsSearchInstructorResponseModal = null;
            try
            {
                lsSearchInstructorResponseModal = await _repo.GetAllInstructorJoinedForStudent(StudentId, SearchString, MaxRowToReturn);
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
        public async Task<List<AssignmentDetailsModel>> SearchForAssignment(string SearchString, int MaxRowToReturn, long LastFecthedId)
        {
            List<AssignmentDetailsModel> lsAssignmentDetailsModel = null;
            try
            {
                lsAssignmentDetailsModel = await _repo.SearchForAssignment(SearchString, MaxRowToReturn, LastFecthedId);
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
        public async Task<List<TestDetailsModel>> SearchForTest(string SearchString, int MaxRowToReturn, long LastFetchedId)
        {
            List<TestDetailsModel> lsAssignmentDetailsModel = null;
            try
            {
                lsAssignmentDetailsModel = await _repo.SearchForTest(SearchString, MaxRowToReturn, LastFetchedId);
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
        public async Task<bool> InserAssignmentResponse(AssignmentSubmissionRequest objAssignmentSubmissionRequest)
        {
            bool result = false;
            try
            {
                objAssignmentSubmissionRequest.ProcessRequest();
                result = _repo.InserAssignmentResponse(objAssignmentSubmissionRequest);
                if (result)
                {
                    await objInstructorAlertManager.AddAssignmentSubmissionAlert(objAssignmentSubmissionRequest.m_llStudentId, objAssignmentSubmissionRequest.m_llAssignmentId);
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
                result = _repo.InsertTestResponse(objTestSubmissionRequest);
                if (result)
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
                lsAssignmentsSubmissionOfStudentResponse = await _repo.GetAllAssignmentSubmissions(StudentId);
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
                lsTestSubmissionModal = await _repo.GetAllTestSubmissions(StudentId);
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
        public async Task<GetAssignmentSubssionDetials> GetAssignmentResponse(long SubmissionId, long StudentId)
        {
            GetAssignmentSubssionDetials objGetAssignmentSubssionDetials = null;
            try
            {
                objGetAssignmentSubssionDetials = await _repo.GetAssignmentResponse(SubmissionId, StudentId);
                if (objGetAssignmentSubssionDetials != null)
                {
                    List<AssignmentSubmissionResponseJsonSerializable> lsAssignmentSubmissionResponseJsonSerializable = JsonConvert.DeserializeObject<List<AssignmentSubmissionResponseJsonSerializable>>(objGetAssignmentSubssionDetials.m_strResponse);

                    objGetAssignmentSubssionDetials.m_lsAssignmentQuestionResponse = ConvertFromJsonObjectToAssignmentResponse(lsAssignmentSubmissionResponseJsonSerializable);
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
        public async Task<GetTestSubmissionDetailsResponse> GetTestResponse(long SubmissionId, long StudentId)
        {
            GetTestSubmissionDetailsResponse objGetTestSubmissionDetailsResponse = null;
            try
            {
                objGetTestSubmissionDetailsResponse = await _repo.GetTestResponse(SubmissionId, StudentId);
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
                objStudentHomeModal = await _repo.GetStudentHomeDetails(StudentId);
                if (objStudentHomeModal != null)
                {
                    objStudentHomeModal.m_lsClassrooms = await _repo.GetStudentHomeClassroomDetails(StudentId);
                    objStudentHomeModal.m_lsCourses = await _repo.GetStudentHomeCoursesDetails(StudentId);
                }
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
            return await _repo.CheckIsStudentHasJoinedTheCourse(StudentId, CourseId);
        }
        public async Task<bool> CheckIsStudentHasSubmittedTheTest(long StudentId, long TestId)
        {
            return await _repo.CheckIsStudentHasSubmittedTheTest(StudentId, TestId);
        }
        public async Task<bool> CheckIsStudentHasSubmittedTheAssignment(long StudentId, long AssignmentId)
        {
            return await _repo.CheckIsStudentHasSubmittedTheAssignment(StudentId, AssignmentId);
        }
        public async Task<bool> CheckIsTestSubmissionIdExsitsForStudent(long StudentId, long SubmissionId)
        {
            return await _repo.CheckIsTestSubmissionIdExsitsForStudent(StudentId, SubmissionId);
        }
        public async Task<bool> CheckIsAssignmentSubmissionIdExsitsForStudent(long StudentId, long SubmissionId)
        {
            return await _repo.CheckIsAssignmentSubmissionIdExsitsForStudent(StudentId, SubmissionId);
        }
        public async Task<InstructorProfileDetailsModal> GetInstructorProfileDetails(int InstructorId, long StudentId)
        {
            return await _repo.GetInstructorProfileDetails(InstructorId, StudentId);
        }
        public async Task<List<CourseDetailsModel>> GetAllCourseDetailsForInstructor(int InstructorId)
        {
            return await _repo.GetAllCourseDetailsForInstructor(InstructorId);
        }
        public async Task<ClassRoomModal> GetClassroomDetailsForStudent(long ClassroomId)
        {
            ClassRoomModal classRoomModal = await _repo.GetClassroomDetailsForStudent(ClassroomId);
            if (classRoomModal.m_dtClassStartDate != null)
            {
                classRoomModal.timeScheduleDetails = MasterUtilities.GetTimer((DateTime)classRoomModal.m_dtClassStartDate - DateTime.Now);
            }
            return classRoomModal;
        }
        public async Task<bool> JoinClassroom(StudentClassroomJoinRequest studentClassroomJoinRequest)
        {
            bool result = false;
            studentClassroomJoinRequest.razorPayPaymentResponseModal.m_strOrderId = studentClassroomJoinRequest.m_strOrderId;
            if (objInstructorBusinessLayer.ValidateRazorPayPaymentRequest(studentClassroomJoinRequest.razorPayPaymentResponseModal))
            {
                result = await _repo.JoinClassroom(studentClassroomJoinRequest.m_llClassroomId,
                    studentClassroomJoinRequest.m_llStudentId);
                await _repo.InsertRazorPayPaymentResponse(studentClassroomJoinRequest.razorPayPaymentResponseModal);
                await objInstructorAlertManager.AddClassroomJoinAlert(studentClassroomJoinRequest.m_llStudentId,
                    studentClassroomJoinRequest.m_llClassroomId);
            }
            return result;
        }
        public async Task<bool> MarkClassroomPaySuccess(StudentClassroomJoinRequest studentClassroomJoinRequest)
        {
            bool result = false;
            studentClassroomJoinRequest.razorPayPaymentResponseModal.m_strOrderId = studentClassroomJoinRequest.m_strOrderId;
            if (objInstructorBusinessLayer.ValidateRazorPayPaymentRequest(studentClassroomJoinRequest.razorPayPaymentResponseModal))
            {
                result = await _repo.MarkClassroomPaymentSuccess(studentClassroomJoinRequest.m_llClassroomId,
                    studentClassroomJoinRequest.m_llStudentId);
                await _repo.InsertRazorPayPaymentResponse(studentClassroomJoinRequest.razorPayPaymentResponseModal);
            }
            return result;
        }
        public async Task<bool> JoinClassroomTrial(long StudentId, long ClassroomId)
        {
            bool result = false;
            if (await _repo.JoinClassroom(ClassroomId, StudentId))
            {
                result = true;
            }
            return result;
        }
        public async Task<bool> JoinClassroomMeeting(long MeetingId, long StudentId)
        {
            return await _repo.JoinClassroomMeeting(MeetingId, StudentId);
        }
        public async Task<List<StudentClassroomModal>> GetJoinedClassroom(long StudentId)
        {
            return await _repo.GetJoinedClassroom(StudentId);
        }
        public async Task<JitsiMeetingModal> GetClassroomMeetingDetails(long ClassroomId)
        {
            return await _repo.GetClassroomMeetingDetails(ClassroomId);
        }
        public async Task<bool> InsertNewMessageToClassroomByStudent(InsertStudentMessageToClassroom insertStudentMessageToClassroom)
        {
            return await _repo.InsertNewMessageToClassroomByStudent(insertStudentMessageToClassroom);
        }
        public async Task<bool> CheckStudentAccessToClassroom(long StudentId, long ClassroomId)
        {
            return await _repo.CheckStudentAccessToClassroom(StudentId, ClassroomId);
        }
        public async Task<List<ClassroomStudentMessageModal>> GetAllClassroomMessageForStudent(long ClassroomId, long StudentId)
        {
            return await _repo.GetAllClassroomMessageForStudent(ClassroomId, StudentId);
        }
        public async Task<List<ClassroomStudentMessageModal>> GetAllClassroomLastMessagesForStudentAfterLast(long ClassroomId, long LastMessageId, long StudentId)
        {
            return await _repo.GetAllClassroomLastMessagesForStudentAfterLast(ClassroomId, LastMessageId, StudentId);
        }
        public async Task<List<StudentClassroomAssignmentModal>> GetAllClassroomAssignment(long ClassroomId, long StudentId)
        {
            return await _repo.GetAllClassroomAssignment(ClassroomId, StudentId);
        }
        public async Task<List<StudentClassroomAssignmentModal>> GetAllClassroomSubmittedAssignment(long ClassroomId, long StudentId)
        {
            return await _repo.GetAllClassroomSubmittedAssignment(ClassroomId, StudentId);
        }
        public async Task<List<StudentClassroomTestModal>> GetAllClassroomTest(long ClassroomId, long StudentId)
        {
            return await _repo.GetAllClassroomTest(ClassroomId, StudentId);
        }
        public async Task<List<StudentClassroomTestModal>> GetAllClassroomTestSubmissons(long ClassroomId, long StudentId)
        {
            return await _repo.GetAllClassroomTestSubmissons(ClassroomId, StudentId);
        }
        public async Task<StudentClassroomHomeDetails> GetStudentClassroomHomeDetails(long ClassroomId, long StudentId)
        {
            return await _repo.GetStudentClassroomHomeDetails(ClassroomId, StudentId);
        }
        public async Task<bool> UpdateProfilePicture(StudentProfileChangeRequest objStudentProfilePictureUpdtaeRequest)
        {
            bool result = false;
            try
            {

                result = await _repo.UpdateProfilePicture(objStudentProfilePictureUpdtaeRequest);


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
        public async Task<StudentRegisterModal> GetStudentBasicDetails(long StudentId)
        {
            return await _repo.GetStudentBasicDetails(StudentId);
        }
        public async Task<List<GetPublicClassroomsResponse>> SearchClassroom(int NoOfRowsFetched, long StudentId, string QueryString)
        {
            if (QueryString == null)
            {
                QueryString = "";
            }
            return await _repo.SearchClassroom(NoOfRowsFetched, Constants.MAX_ITEMS_TO_BE_RETURNED, StudentId, QueryString);
        }
        public async Task<RazorPayPaymentRequestModal> GetClassroomPaymentData(ClassroomPaymentRequestDTO classroomPaymentRequestDTO)
        {
            RazorPayPaymentRequestModal razorPayPaymentRequestModal = null;
            PaymentRequestDTO paymentRequestDTO = await _repo.GetClassroomPaymentInfo(classroomPaymentRequestDTO.m_llClassroomId, classroomPaymentRequestDTO.m_llStudentId);
            if (classroomPaymentRequestDTO != null)
            {
                razorPayPaymentRequestModal = new RazorPayPaymentRequestModal();
                if (paymentRequestDTO.m_iClassroomPayment == 0)
                {
                    razorPayPaymentRequestModal.m_bIsJoined = await _repo.JoinClassroom(classroomPaymentRequestDTO.m_llClassroomId, classroomPaymentRequestDTO.m_llStudentId);
                    razorPayPaymentRequestModal.m_bIsFreeCourse = true;
                }
                else
                {
                    paymentRequestDTO.m_strCurrency = objInstructorBusinessLayer.GetCurrencyValue(classroomPaymentRequestDTO.m_iCountryCode);
                    paymentRequestDTO.m_iClassroomPayment = objInstructorBusinessLayer.GetAmountBasedOnCounty(classroomPaymentRequestDTO.m_iCountryCode, paymentRequestDTO.m_iClassroomPayment);
                    paymentRequestDTO.m_iClassroomPayment = objInstructorBusinessLayer.GetCouponDiscount(classroomPaymentRequestDTO.m_strCouponCode, paymentRequestDTO.m_iClassroomPayment, await _repo.GetAllCoupons());
                    RazorPayPaymentDataModal razorPayPaymentDataModal = objInstructorBusinessLayer.CreateRazorPaymentRequest(paymentRequestDTO);
                    razorPayPaymentRequestModal.m_strOrderId = razorPayPaymentDataModal.m_strOrderId;
                    razorPayPaymentRequestModal.razorPayPaymentDataModal = new RazorPayPaymentDataModal();
                    razorPayPaymentRequestModal.razorPayPaymentDataModal.m_strCurrency = razorPayPaymentDataModal.m_strCurrency;
                    razorPayPaymentRequestModal.razorPayPaymentDataModal.m_iAmountInPaise = paymentRequestDTO.m_iClassroomPayment;
                    razorPayPaymentRequestModal.m_strLogoUrl = Constants.WEBSITE_LOGO_URL;
                    razorPayPaymentRequestModal.m_strRazorPayKey = MvcApplication._strRazorPayKey;
                    razorPayPaymentRequestModal.m_strSiteName = Constants.WEBSITE_NAME;
                    razorPayPaymentRequestModal.razorPayCustomerData = new RazorPayCustomerData();
                    razorPayPaymentRequestModal.razorPayCustomerData.m_strContact = paymentRequestDTO.m_strCustomerPhoneNo;
                    razorPayPaymentRequestModal.razorPayCustomerData.m_strEmail = paymentRequestDTO.m_strCutomerEmail;
                    razorPayPaymentRequestModal.razorPayCustomerData.m_strName = paymentRequestDTO.m_strCustomerName;
                    if (!await _repo.InsertPaymentOrderRequest(razorPayPaymentRequestModal))
                    {
                        razorPayPaymentRequestModal = null;
                    }
                }

            }
            return razorPayPaymentRequestModal;
        }
        public async Task<RazorPayPaymentRequestModal> GetCoursePaymentData(ClassroomPaymentRequestDTO classroomPaymentRequestDTO)
        {
            RazorPayPaymentRequestModal razorPayPaymentRequestModal = null;
            PaymentRequestDTO paymentRequestDTO = await _repo.GetCoursePaymentInfo(classroomPaymentRequestDTO.m_llClassroomId, classroomPaymentRequestDTO.m_llStudentId);
            if (paymentRequestDTO != null)
            {
                razorPayPaymentRequestModal = new RazorPayPaymentRequestModal();
                if (paymentRequestDTO.m_iClassroomPayment == 0)
                {
                    razorPayPaymentRequestModal.m_bIsJoined = await _repo.JoinStudentToCourse(classroomPaymentRequestDTO.m_llClassroomId, classroomPaymentRequestDTO.m_llStudentId);
                    razorPayPaymentRequestModal.m_bIsFreeCourse = true;
                }
                else
                {
                    RazorPayPaymentDataModal razorPayPaymentDataModal = objInstructorBusinessLayer.CreateRazorPaymentRequest(paymentRequestDTO);
                    razorPayPaymentRequestModal.m_strOrderId = razorPayPaymentDataModal.m_strOrderId;
                    razorPayPaymentRequestModal.razorPayPaymentDataModal = new RazorPayPaymentDataModal();
                    razorPayPaymentRequestModal.razorPayPaymentDataModal.m_strCurrency = objInstructorBusinessLayer.GetCurrencyValue(classroomPaymentRequestDTO.m_iCountryCode);
                    razorPayPaymentRequestModal.razorPayPaymentDataModal.m_iAmountInPaise = paymentRequestDTO.m_iClassroomPayment;
                    razorPayPaymentRequestModal.m_strLogoUrl = Constants.WEBSITE_LOGO_URL;
                    razorPayPaymentRequestModal.m_strRazorPayKey = MvcApplication._strRazorPayKey;
                    razorPayPaymentRequestModal.m_strSiteName = Constants.WEBSITE_NAME;
                    razorPayPaymentRequestModal.razorPayCustomerData = new RazorPayCustomerData();
                    razorPayPaymentRequestModal.razorPayCustomerData.m_strContact = paymentRequestDTO.m_strCustomerPhoneNo;
                    razorPayPaymentRequestModal.razorPayCustomerData.m_strEmail = paymentRequestDTO.m_strCutomerEmail;
                    razorPayPaymentRequestModal.razorPayCustomerData.m_strName = paymentRequestDTO.m_strCustomerName;
                    if (!await _repo.InsertPaymentOrderRequest(razorPayPaymentRequestModal))
                    {
                        razorPayPaymentRequestModal = null;
                    }
                }

            }
            return razorPayPaymentRequestModal;
        }
        public async Task<List<StudentTestSearchResultModal>> GetFreeTestsForStudent(StudentTestSearchRequest studentTestSearchRequest)
        {
            studentTestSearchRequest.m_iNoOfRowsToBeFetched = 10;
            return await _repo.GetFreeTestsForStudent(studentTestSearchRequest);
        }
        public async Task<ClassroomJoinDetailsModal> GetClassroomDetailsForStudentJoin(long ClassroomId)
        {
            return await _repo.GetClassroomDetailsForStudentJoin(ClassroomId);
        }
        public async Task<bool> RegisterStudentFreeToClassroom(long ClassroomId, long StudentId)
        {
            return await _repo.JoinClassroom(ClassroomId, StudentId);
        }
        public async Task<StudentSearchResponse> GetStudentSearchResult(StudentSearchRequestModal studentSearchRequestModal)
        {
            StudentSearchResponse studentSearchResponse = new StudentSearchResponse();
            try
            {

                studentSearchResponse.m_lsCourses = await _repo.SearchForCourse(studentSearchRequestModal.m_strQueryString, 10, 0, (int)Constants.FilterTypeId.DATE_OF_CREATION_DESCENDING);
                studentSearchResponse.m_lsLiveClasses = await _repo.SearchClassroom(0, 10, studentSearchRequestModal.m_llStudentId, studentSearchRequestModal.m_strQueryString);
                studentSearchResponse.m_lsTest = await _repo.SearchForTest(studentSearchRequestModal.m_strQueryString, 10, 0);
                studentSearchResponse.m_lsInstructors = await _repo.SearchForInstructor(studentSearchRequestModal.m_strQueryString, 10);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetStudentSearchResult", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return studentSearchResponse;
        }
        public async Task<List<StudentDetailToFolllow>> GetAllStudentsToJoin(GetStudentsToFollowRequest getStudentsToFollowRequest)
        {
            List<StudentDetailToFolllow> lsStudentDetailToFolllow = null;
            try
            {
                getStudentsToFollowRequest.m_iNoOfRowsToBeFetched = Constants.MAX_ITEMS_TO_BE_RETURNED;
                if (getStudentsToFollowRequest.m_strSearchString == null)
                {
                    getStudentsToFollowRequest.m_strSearchString = "";
                }
                lsStudentDetailToFolllow = await _repo.GetAllStudentsToJoin(getStudentsToFollowRequest);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllStudentsToJoin", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsStudentDetailToFolllow;
        }
        public async Task<StudentPublicProfileResponse> GetStudentPublicProfileResponse(long StudentId)
        {
            return await _repo.GetStudentPublicProfileResponse(StudentId);
        }
        public async Task<bool> JoinStudentToStudent(long StudentId, long StudentToBeJoinedId)
        {
            return await _repo.JoinStudentToStudent(StudentId, StudentToBeJoinedId);
        }
        public async Task<List<StudentFollowedByStudentDetails>> GetAllStudentsFollwoedByStudent(GetAllStudentFollowedByStudentRequest getAllStudentFollowedByStudentRequest)
        {
            List<StudentFollowedByStudentDetails> lsStudentFollowedByStudentDetails = null;
            try
            {
                getAllStudentFollowedByStudentRequest.m_iNoOfRowsToBeFetched = Constants.MAX_ITEMS_TO_BE_RETURNED;
                lsStudentFollowedByStudentDetails = await _repo.GetAllStudentsFollwoedByStudent(getAllStudentFollowedByStudentRequest);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllStudentsToJoin", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsStudentFollowedByStudentDetails;
        }
        public async Task<bool> CheckStudentFollowingStudent(long StudentId, long StudentToBeFollowedId)
        {
            return await _repo.CheckStudentFollowingStudent(StudentId, StudentToBeFollowedId);
        }
        public async Task<StudentToStudentConnectionDetails> FetchStudentToStudentConnectionDetails(long StudentId, long FriendId)
        {
            return await _repo.FetchStudentToStudentConnectionDetails(StudentId, FriendId);
        }
        public async Task<StudentProfileDetails> FetchStudentSelfDetails(long StudentId)
        {
            return await _repo.FetchStudentSelfDetails(StudentId);
        }
        public async Task<List<StudentLiveClassMeetingDetails>> GetAllLiveClassMeetingDetailsForStudnet(long StudentId, long ClassroomId)
        {
            return await _repo.GetAllLiveClassMeetingDetailsForStudnet(StudentId, ClassroomId);
        }
        public async Task<List<StudentLiveClassMeetingDetails>> GetAllTrialLiveClassMeetingDetailsForStudnet(long StudentId, long ClassroomId)
        {
            return await _repo.GetAllTrialLiveClassMeetingDetailsForStudnet(StudentId, ClassroomId);
        }
        public async Task<StudentLiveClassMeetingDetails> GetLiveClassMeetingDetailsForStudnet(GetClassroomMeetingDetailsForStudentRequest getClassroomMeetingDetailsForStudentRequest)
        {
            return await _repo.GetLiveClassMeetingDetailsForStudnet(getClassroomMeetingDetailsForStudentRequest);
        }
        public async Task<ClassroomSyllabusDetailsModal> GetClassroomSyllabus(long ClassroomId)
        {
            ClassroomSyllabusDetailsModal classroomSyllabusDetailsModal = await _repo.GetClassroomSyllabus(ClassroomId);
            if (classroomSyllabusDetailsModal != null)
            {
                classroomSyllabusDetailsModal.m_lsClassroomWeekWiseSyallabus = JsonConvert.DeserializeObject<List<ClassroomWeekWiseSyallabus>>(classroomSyllabusDetailsModal.m_strSerializedSyllabus);
            }
            return classroomSyllabusDetailsModal;
        }
        public async Task<ClassroomScheduleDetails> GetClassroomSchedule(long ClassroomId)
        {
            ClassroomScheduleDTO classroomScheduleDTO = await _repo.GetClassroomScheduleDetails(ClassroomId);
            ClassroomScheduleDetails classroomScheduleDetails = null;
            if (classroomScheduleDTO != null)
            {
                classroomScheduleDetails = JsonConvert.DeserializeObject<ClassroomScheduleDetails>(classroomScheduleDTO.m_strClassroomScheduleObj);
                classroomScheduleDetails.m_strClassroomScheduleInsertionTime = classroomScheduleDTO.m_dtClassroomScheduleInsertionTime.ToString();
                classroomScheduleDetails.m_strClassroomScheduleUpdationTime = classroomScheduleDTO.m_dtClassroomScheduleUpdationTime.ToString();
            }
            if (classroomScheduleDetails != null && classroomScheduleDetails.m_lsDayWiseScheduleDetails != null)
            {
                var days = new List<String> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
                for (var i = 0; i < classroomScheduleDetails.m_lsDayWiseScheduleDetails.Count; i++)
                {
                    classroomScheduleDetails.m_lsDayWiseScheduleDetails[i].m_strDayName = days[i];
                }
            }
            return classroomScheduleDetails;
        }
        public async Task<bool> InsertClassroomFeedback(ClassroomFeedbackRequest classroomFeedbackRequest)
        {
            return await _repo.InsertClassroomFeedback(classroomFeedbackRequest);
        }
        public StudentRegisterModal GetStudentModalFromGoogleModal(GoogleSignInRequest googleSignInRequest)
        {
            StudentRegisterModal studentRegisterModal = new StudentRegisterModal();
            try
            {

                studentRegisterModal.m_strFirstName = googleSignInRequest.m_strFirstName;
                studentRegisterModal.m_strLastName = googleSignInRequest.m_strLastName;
                studentRegisterModal.m_strEmail = googleSignInRequest.m_strEmailId;
                studentRegisterModal.m_strPhoneNo = googleSignInRequest.m_strPhoneNo;
                studentRegisterModal.m_strProfileUrl = googleSignInRequest.m_strProfileUrl;
                studentRegisterModal.m_strGmailId = googleSignInRequest.m_strGoogleId;
                if (studentRegisterModal.m_strProfileUrl == null || studentRegisterModal.m_strProfileUrl == "")
                {
                    studentRegisterModal.m_strProfileUrl = "../Images/avatar-user.png";
                }
            }
            catch (Exception Ex)
            {

                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetStudentModalFromGoogleModal", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return studentRegisterModal;
        }
        public async Task<bool> RegisterNewStudentViaGmail(StudentRegisterModal objStudentRegisterModal)
        {
            bool result = false;
            try
            {
                objStudentRegisterModal.m_strPhoneNo = objStudentRegisterModal.m_strPhoneNo;
                objStudentRegisterModal.m_strPhoneNoVarificationGuid = objInstructorBusinessLayer.GetSmsVerificationString();
                //objStudentRegisterModal.m_strEmailVarificationGuid = objInstructorBusinessLayer.GetEmailVerficationString();
                if (await _repo.RegisterNewStudentViaEmail(objStudentRegisterModal))
                {
                    result = true;
                    var SmsVarificationLink = objInstructorBusinessLayer.GetLinkForSmsVarification(objStudentRegisterModal.m_strEmailVarificationGuid,
                        objStudentRegisterModal.m_strEmail, Constants.SMS_VERIFICATION_LINK_TYPE_ID_FOR_STUDENT);
                    //await objSMSServiceManager.SendInstructorPhoneNoVarification(SmsVarificationLink, objStudentRegisterModal.m_strPhoneNo);
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
        public async Task<StudentRegisterModal> CheckGmailUserAlreadyExists(StudentRegisterModal user)
        {
            StudentRegisterModal studentRegisterModal = null;
            try
            {
                studentRegisterModal = await _repo.CheckGmailUserAlreadyExists(user);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckGmailUserAlreadyExists", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return studentRegisterModal;
        }
        public async Task<bool> InsertOtpToVarifyAccount(long StudentId)
        {
            bool result = false;
            try
            {
                string Otp = objInstructorBusinessLayer.GenerateOtp();
                StudentRegisterModal studentDetails = await GetStudentDetails(StudentId);
                if (studentDetails != null)
                {
                    if (await _repo.InsertOtpToVarifyAccount(Otp, StudentId))
                    {
                        result = await objSMSServiceManager.SendPhoneNoVarificationOtp(Otp, studentDetails.m_strPhoneNo);
                    }
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertOtpToVarifyAccount", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> VarifyPhoneNo(string Otp, string StudentUserId, string PhoneNoVarificationGuid)
        {
            bool result = false;
            try
            {
                result = await _repo.VarifyPhoneNo(Otp, StudentUserId, PhoneNoVarificationGuid);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "VarifyPhoneNo", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdatePhoneNoOfGmailRegStudentAsync(string PhoneNo, string StudentUserId, string PhoneNoVarificationGuid)
        {
            bool result = false;
            try
            {
                result = await _repo.UpdatePhoneNoOfGmailRegStudentAsync(PhoneNo, StudentUserId, PhoneNoVarificationGuid);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "VarifyPhoneNo", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<StudentsJoinedToClassroomDetailsForStudent>> GetStudentsJoinedToClassroom(StudentsJoinedToClassroomRequestForStudent studentsJoinedToClassroomRequestForStudent)
        {
            List<StudentsJoinedToClassroomDetailsForStudent> lsStudentsJoinedToClassroomDetailsForStudent = null;
            try
            {
                studentsJoinedToClassroomRequestForStudent.m_iMaxRowsToBeFetched = Constants.MAX_ITEMS_TO_BE_RETURNED;
                lsStudentsJoinedToClassroomDetailsForStudent = await _repo.GetStudentsJoinedToClassroom(studentsJoinedToClassroomRequestForStudent);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "VarifyPhoneNo", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsStudentsJoinedToClassroomDetailsForStudent;
        }
        public async Task<AvgReviewModel> GetAvgClassroomRating(long ClassroomId)
        {
            AvgReviewModel avgReviewModel = new AvgReviewModel();
            List<RatingNormal> lsRatingNormal = null;
            try
            {
                lsRatingNormal = await _repo.GetAvgClassroomRating(ClassroomId);
                var TotalRatings = 0;
                var AvgRatingSum = 0;
                foreach (var data in lsRatingNormal)
                {
                    TotalRatings += data.m_iNoOfRating;

                    switch (data.m_iRating)
                    {
                        case 1:
                            {
                                AvgRatingSum += 1 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage1StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 2:
                            {
                                AvgRatingSum += 2 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage2StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 3:
                            {
                                AvgRatingSum += 3 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage3StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 4:
                            {
                                AvgRatingSum += 4 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage4StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 5:
                            {
                                AvgRatingSum += 5 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage5StartRating = data.m_iNoOfRating;
                                break;
                            }
                    }
                }
                if (TotalRatings != 0)
                {
                    avgReviewModel.m_iTotalReviews = TotalRatings;
                    avgReviewModel.m_fPercentage1StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage1StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage2StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage2StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage3StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage3StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage4StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage4StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage5StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage5StartRating, TotalRatings);
                    avgReviewModel.m_fAvgRating = AvgRatingSum / TotalRatings;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "VarifyPhoneNo", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return avgReviewModel;
        }
        public async Task<List<ReviewModel>> GetAllClassroomReviews(MasterSearchRequest masterSearchRequest, long ClassroomId)
        {
            List<ReviewModel> lsReviewModel = null;
            try
            {
                lsReviewModel = await _repo.GetAllClassroomReviews(ClassroomId, masterSearchRequest.m_iNoOfRowsFetched, Constants.MAX_ITEMS_TO_BE_RETURNED);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "VarifyPhoneNo", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsReviewModel;
        }
        public async Task<ReviewsDetails> GetClassroomReview(long ClassroomId)
        {
            ReviewsDetails classroomReviewsResponse = new ReviewsDetails();
            try
            {
                classroomReviewsResponse.avgReviewModel = await GetAvgClassroomRating(ClassroomId);
                classroomReviewsResponse.lsReviews = await GetAllClassroomReviews(new MasterSearchRequest() { m_iNoOfRowsFetched = 0 }, ClassroomId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "VarifyPhoneNo", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return classroomReviewsResponse;
        }
        public async Task<bool> UpdateStudentBasicProfileDetailsAsync(StudentProfileUpdateRequest request)
        {
            bool result = false;
            try
            {
                result = await _repo.
                    UpdateStudentBasicProfileDetailsAsync(request);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "VarifyPhoneNo", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> ChangePasswordAsync(StudentChangePasswordRequest request)
        {
            bool result = false;
            try
            {
                request.HashedPassword = SHA256Encryption.ComputeSha256Hash(request.Password);
                result = await _repo.
                    ChangePasswordAsync(request);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ChangePasswordAsync", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
    }
}