using StudentDashboard.HttpRequest;
using StudentDashboard.HttpResponse;
using StudentDashboard.Models;
using StudentDashboard.Models.Course;
using StudentDashboard.Models.Student;
using StudentDashboard.Utilities;
using StudentDashboard.Views.Student;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
        public bool RegisterNewStudent(StudentRegisterModal objStudentDetailsModal)
        {
            bool result = false;
            try
            {
                result = objCPDataService.RegisterNewStudent(objStudentDetailsModal.m_strFirstName,objStudentDetailsModal.m_strLastName,
                               objStudentDetailsModal.m_strEmail,objStudentDetailsModal.m_strHashedPassword,objStudentDetailsModal.m_strPhoneNo);

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
        public bool InsertActivityForInstructor(long StudentId, string ActivityMessage)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertActivityForStudent( ActivityMessage,StudentId);
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
        public List<CourseDetailsModel> SearchForCourse(string SearchString,int MaxRowToReturn,int NoOfRowsFetched,int SortingTypeId)
        {
            List<CourseDetailsModel> lsCourseDetailsModel = null;
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.SearchForCourse(SearchString,MaxRowToReturn, NoOfRowsFetched, SortingTypeId);
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
        public List<CourseDetailsModel> SearchForCourse(string SearchString, int MaxRowToReturn, int NoOfRowsFetched, int SortingTypeId,long StudentId)
        {
            List<CourseDetailsModel> lsCourseDetailsModel = null;
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.SearchForCourseForStudent(SearchString, MaxRowToReturn, NoOfRowsFetched, SortingTypeId, StudentId);
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
        public StudentRegisterModal GetStudentDetails(long StudentId)
        {
            StudentRegisterModal objStudentRegisterModal = null;
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetStudentDetails(StudentId);
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
        public bool UpdateStudentDetails(StudentRegisterModal objStudentRegisterModal)
        {
            bool result = false;
            try
            {
                result = objCPDataService.UpdateStudentDetails(objStudentRegisterModal.m_strFirstName, objStudentRegisterModal.m_strLastName,
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
        public bool JoinStudentToCourse(long CourseId,long StudentId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.JoinStudentToCourse(CourseId,StudentId);
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
        public List<SearchInstructorResponseModal> SearchForInstructor(string SearchString,int MaxRowToReturn )
        {
            List<SearchInstructorResponseModal> lsSearchInstructorResponseModal = null;
            try
            {
                DataSet ds = objCPDataService.SearchForInstructor(SearchString, MaxRowToReturn);
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
        public List<StudentJoinedCoursesResponseModal> SerachForJoinedCourses(long StudentId,string SearchString, int MaxRowToReturn)
        {
            List<StudentJoinedCoursesResponseModal> lsStudentJoinedCoursesResponseModal = null;
            try
            {
                DataSet ds = objCPDataService.GetJoinedCoursesForStudent(StudentId,SearchString, MaxRowToReturn);
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
        public bool JoinStudentToInstructor(long StudentId,int InstructorId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.JoinStudentToInstructor(StudentId, InstructorId);
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
        public List<SearchInstructorResponseModal> GetAllInstructorJoinedForStudent(long StudentId,string SearchString, int MaxRowToReturn)
        {
            List<SearchInstructorResponseModal> lsSearchInstructorResponseModal = null;
            try
            {
                DataSet ds = objCPDataService.GetAllJoinedInstructorForStudent(StudentId,SearchString, MaxRowToReturn);
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
        public List<AssignmentDetailsModel> SearchForAssignment(string SearchString, int MaxRowToReturn)
        {
            List<AssignmentDetailsModel> lsAssignmentDetailsModel = null;
            try
            {
                DataSet ds = objCPDataService.SearchForAssignment(SearchString, MaxRowToReturn);
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
        public bool InserAssignmentResponse(AssignmentSubmissionRequest objAssignmentSubmissionRequest)
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
        public List<TestDetailsModel> SearchForTest(string SearchString, int MaxRowToReturn)
        {
            List<TestDetailsModel> lsTestDetailsModel = null;
            try
            {
                DataSet ds = objCPDataService.SearchForTest(SearchString, MaxRowToReturn);
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
        public List<AssignmentsSubmissionModal> GetAllAssignmentSubmissions(long StudentId)
        {
            List<AssignmentsSubmissionModal> lsAssignmentsSubmissionOfStudentResponse = null;
            try
            {
                DataSet ds = objCPDataService.GetAllAssignmentSubmissionsForStudent(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsAssignmentsSubmissionOfStudentResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AssignmentsSubmissionModal(
                         dataRow.Field<long>("SUBMISSION_ID"),
                         dataRow.Field<int>("TOTAL_NO_OF_QUESTIONS"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("ASSIGNMANT_PERCENTAGE_SCORE")
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
        public GetAssignmentSubssionDetials GetAssignmentResponse(long Submissionid)
        {
            GetAssignmentSubssionDetials objGetAssignmentSubssionDetials=null;
            try
            {

                DataSet ds = objCPDataService.GetAssignmentResponse(Submissionid);
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
        public List<TestSubmissionModal> GetAllTestSubmissions(long StudentId)
        {
            List<TestSubmissionModal> lsTestSubmissionModal = null;
            try
            {
                DataSet ds = objCPDataService.GetAllTestSubmissionsForStudent(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsTestSubmissionModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new TestSubmissionModal(
                         dataRow.Field<long>("SUBMISSION_ID"),
                         dataRow.Field<int>("TOTAL_NO_OF_QUESTIONS"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("TEST_SCORE")
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
        public GetTestSubmissionDetailsResponse GetTestResponse(long Submissionid)
        {
            GetTestSubmissionDetailsResponse objGetTestSubmissionDetailsResponse = null;
            try
            {
                DataSet ds = objCPDataService.GetTestResponse(Submissionid);
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
        public StudentHomeModal GetStudentHomeDetails(long StudentId)
        {
            StudentHomeModal objStudentHomeModal = null;
            try
            {

                DataSet ds = objCPDataService.GetStudentHomeDetails(StudentId);
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
    }
}