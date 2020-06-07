using Newtonsoft.Json;
using StudentDashboard.AlertManager;
using StudentDashboard.DTO;
using StudentDashboard.HttpRequest;
using StudentDashboard.HttpResponse;
using StudentDashboard.JsonSerializableObject;
using StudentDashboard.Models.Course;
using StudentDashboard.Models.Student;
using StudentDashboard.Utilities;
using StudentDashboard.Views.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace StudentDashboard.ServiceLayer
{
    public class StudentService
    {
        StudentDTO objStudentDTO;
        StringBuilder m_strLogMessage;
        InstructorAlertManager objInstructorAlertManager;
        public StudentService()
        {
            objStudentDTO = new StudentDTO();
            m_strLogMessage = new StringBuilder();
            objInstructorAlertManager = new InstructorAlertManager();

        }
        public StudentDetailsModel GetStudentDetails(int StudentId)
        {
            return new StudentDetailsModel();
        }
        public bool RegisterNewStudent(StudentRegisterModal objStudentRegisterModal)
        {
            bool result = false;
            try
            {
                string EncryptedPassword = SHA256Encryption.ComputeSha256Hash(objStudentRegisterModal.m_strPassword);
                objStudentRegisterModal.m_strHashedPassword = EncryptedPassword;
                result = objStudentDTO.RegisterNewStudent(objStudentRegisterModal);
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewUser", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;

        }
        public List<CourseDetailsModel> SearchForCourse(string SearchString, int MaxRowToReturn,int NoOfRowseFetched,int SortingId)
        {
            List<CourseDetailsModel> lsCourseDetailsModel = null;
            try
            {
                lsCourseDetailsModel = objStudentDTO.SearchForCourse(SearchString, MaxRowToReturn, NoOfRowseFetched, SortingId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewUser", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsCourseDetailsModel;
        }
        public List<CourseDetailsModel> SearchForCourseForStudent(string SearchString, int MaxRowToReturn, int NoOfRowseFetched, int SortingId,long Studentid)
        {
            List<CourseDetailsModel> lsCourseDetailsModel = null;
            try
            {
                lsCourseDetailsModel = objStudentDTO.SearchForCourse(SearchString, MaxRowToReturn, NoOfRowseFetched, SortingId, Studentid);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewUser", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsCourseDetailsModel;
        }
        public StudentRegisterModal GetStudentDetails(long StudentId)
        {
            StudentRegisterModal objStudentRegisterModal = null;
            try
            {
                objStudentRegisterModal = objStudentDTO.GetStudentDetails(StudentId);
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
        public bool UpdateStudentDetails(StudentRegisterModal objStudentRegisterModal)
        {
            bool result = false;
            try
            {
                result = objStudentDTO.UpdateStudentDetails(objStudentRegisterModal);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetStudentDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool JoinStudentToCourse(long CourseId, long StudentId)
        {
            bool result = false;
            try
            {
                result = objStudentDTO.JoinStudentToCourse(CourseId, StudentId);
                if(result)
                {
                    objInstructorAlertManager.AddCourseJoinAlert(StudentId, CourseId);
                }
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
        public bool JoinStudentToInstructor(long StudentId, int InstructorId)
        {
            bool result = false;
            try
            {
                result = objStudentDTO.JoinStudentToInstructor(StudentId, InstructorId);
                if(result)
                {
                    objInstructorAlertManager.AddStudentJoinAlert(StudentId, InstructorId);
                }
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

        public List<StudentJoinedCoursesResponseModal> SerachForJoinedCourses(long StudentId, string SearchString, int MaxRowToReturn)
        {
            List<StudentJoinedCoursesResponseModal> lsStudentJoinedCoursesResponseModal = null;
            try
            {
                lsStudentJoinedCoursesResponseModal = objStudentDTO.SerachForJoinedCourses(StudentId, SearchString, MaxRowToReturn);
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
        public List<SearchInstructorResponseModal> SearchForInstructor(string SearchString, int MaxRowToReturn)
        {
            List<SearchInstructorResponseModal> lsSearchInstructorResponseModal = null;
            try
            {
                lsSearchInstructorResponseModal = objStudentDTO.SearchForInstructor(SearchString, MaxRowToReturn);
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
        public List<SearchInstructorResponseModal> SearchForJoinedInstructor(long StudentId,string SearchString, int MaxRowToReturn)
        {
            List<SearchInstructorResponseModal> lsSearchInstructorResponseModal = null;
            try
            {
                lsSearchInstructorResponseModal = objStudentDTO.GetAllInstructorJoinedForStudent(StudentId,SearchString, MaxRowToReturn);
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
        public List<AssignmentDetailsModel> SearchForAssignment(string SearchString, int MaxRowToReturn)
        {
            List<AssignmentDetailsModel> lsAssignmentDetailsModel = null;
            try
            {
                lsAssignmentDetailsModel = objStudentDTO.SearchForAssignment(SearchString, MaxRowToReturn);
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
        public List<TestDetailsModel> SearchForTest(string SearchString, int MaxRowToReturn)
        {
            List<TestDetailsModel> lsAssignmentDetailsModel = null;
            try
            {
                lsAssignmentDetailsModel = objStudentDTO.SearchForTest(SearchString, MaxRowToReturn);
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
        public List<AssignmentsSubmissionModal> GetAllAssignmentSubmissions(long StudentId)
        {
            List<AssignmentsSubmissionModal> lsAssignmentsSubmissionOfStudentResponse = null;
            try
            {
                lsAssignmentsSubmissionOfStudentResponse = objStudentDTO.GetAllAssignmentSubmissions(StudentId);
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
        public List<TestSubmissionModal> GetAllTestSubmissions(long StudentId)
        {
            List<TestSubmissionModal> lsTestSubmissionModal = null;
            try
            {
                lsTestSubmissionModal = objStudentDTO.GetAllTestSubmissions(StudentId);
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
        public GetAssignmentSubssionDetials GetAssignmentResponse(long SubmissionId)
        {
            GetAssignmentSubssionDetials objGetAssignmentSubssionDetials = null;
            try
            {
                objGetAssignmentSubssionDetials=objStudentDTO.GetAssignmentResponse(SubmissionId);
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
        public GetTestSubmissionDetailsResponse GetTestResponse(long SubmissionId)
        {
            GetTestSubmissionDetailsResponse objGetTestSubmissionDetailsResponse = null;
            try
            {
                objGetTestSubmissionDetailsResponse = objStudentDTO.GetTestResponse(SubmissionId);
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
        public StudentHomeModal GetStudentHomeDetails(long StudentId)
        {
            StudentHomeModal objStudentHomeModal = null;
            try
            {
                objStudentHomeModal = objStudentDTO.GetStudentHomeDetails(StudentId);
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
    }
}