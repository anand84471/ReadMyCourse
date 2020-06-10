using StudentDashboard.HttpRequest;
using StudentDashboard.HttpResponse;
using StudentDashboard.Models.Course;
using StudentDashboard.Security;
using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudentDashboard.API
{
    //[Authorize]
    [RoutePrefix("api/v1/Course")]
    public class CourseApiController : ApiController
    {
        HomeService objHomeService = new HomeService();
        StudentService objStudentService = new StudentService();
        StringBuilder m_strLogMessage = new StringBuilder();
        [Route("FetchAboutCourse")]
        [HttpPost]
        public async Task<AboutCourseResponse> AboutCourse(int id)
        {
            AboutCourseResponse objResonse = new AboutCourseResponse();
            try
            {
                objResonse=await objHomeService.GetAboutCourse(id);
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AboutCourseResponse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResonse;
        }
        [Route("FetchCourseIndexDetails")]
        [HttpPost]
        public async Task<IndexModel> GetCourseIndexDetails(int Id)
        {
            IndexModel objResponse = new IndexModel();
            try
            {
                objResponse = await objHomeService.GetIndexDetails(Id);
                if (objResponse != null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse = new IndexModel();
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("FetchAssignmentDetails")]
        [HttpPost]
        public async Task<AssignmentModel> GetAssignmentDetails(long AssignmentId)
        {
            AssignmentModel objResponse=null;
            try
            {
                objResponse = await objHomeService.GetAssignmentDetails(AssignmentId);
                if (objResponse != null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse = new AssignmentModel();
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("FetchTestDetails")]
        [HttpPost]
        public async Task<TestModel> GetTestDetails(long id)
        {
            TestModel objResponse = null;
            try
            {
                objResponse = await objHomeService.GetFullMcqTestDetails(id);
                if (objResponse != null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse = new TestModel();
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("SearchCourse")]
        [HttpPost]
        public SearchCourseHttpResponse SearchCourse([FromBody]SerchCourseRequest objSerchCourseRequest)
        {
            SearchCourseHttpResponse objResponse = new SearchCourseHttpResponse();
            try
            {
                if (objSerchCourseRequest != null)
                {
                    objResponse.lsCourseDetailsModel = objStudentService.SearchForCourse(objSerchCourseRequest.m_strKey, 10, objSerchCourseRequest.m_iNoOfRowsFetched,
                                      objSerchCourseRequest.m_iSortingId);
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("SearchCourseForStudent")]
        [HttpPost]
        public SearchCourseHttpResponse SearchCourseForStudent([FromBody]SerchCourseRequest objSerchCourseRequest)
        {
            SearchCourseHttpResponse objResponse = new SearchCourseHttpResponse();
            try
            {
                if (objSerchCourseRequest != null)
                {
                    objResponse.lsCourseDetailsModel = objStudentService.SearchForCourseForStudent(objSerchCourseRequest.m_strKey, 10, objSerchCourseRequest.m_iNoOfRowsFetched,
                                      objSerchCourseRequest.m_iSortingId, objSerchCourseRequest.m_llStudentId);
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [JwtAuthentication]
        [Route("JoinCourse")]
        [HttpPost]
        public APIDefaultResponse JoinCourse(long CourseId, long StudentId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objStudentService.JoinStudentToCourse(CourseId, StudentId))
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("JoinStudentToInstructor")]
        [HttpPost]
        public APIDefaultResponse JoinInstructor(long StudentId,int InstructorId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objStudentService.JoinStudentToInstructor(StudentId, InstructorId))
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("GetJoinedCourses")]
        [HttpPost]
        public StudentJoinedCoursesResponse GetJoinedCourses([FromBody] GetJoinedCourseRequest objGetJoinedCourseRequest)
        {
            StudentJoinedCoursesResponse objResponse = new StudentJoinedCoursesResponse();
            try
            {
                objResponse.lsStudentJoinedCoursesResponseModal = objStudentService.SerachForJoinedCourses(
                    objGetJoinedCourseRequest.m_llStudentGid, objGetJoinedCourseRequest.m_strSearchString, 10);
                if (objResponse.lsStudentJoinedCoursesResponseModal!=null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("InstructorSearch")]
        [HttpPost]
        public SearchInstructorResponse InstructorSearch([FromBody] GetJoinedCourseRequest objGetJoinedCourseRequest)
        {
            SearchInstructorResponse objResponse = new SearchInstructorResponse();
            try
            {
                objResponse.lsSearchInstructorResponseModal = objStudentService.SearchForInstructor(
                   objGetJoinedCourseRequest.m_strSearchString, 10);
                if (objResponse.lsSearchInstructorResponseModal != null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("GetJoinedInstructors")]
        [HttpPost]
        public SearchInstructorResponse GetJoinedInstructors([FromBody] GetJoinedCourseRequest objGetJoinedCourseRequest)
        {
            SearchInstructorResponse objResponse = new SearchInstructorResponse();
            try
            {
                objResponse.lsSearchInstructorResponseModal = objStudentService.SearchForJoinedInstructor(objGetJoinedCourseRequest.m_llStudentGid,
                   objGetJoinedCourseRequest.m_strSearchString, 10);
                if (objResponse.lsSearchInstructorResponseModal != null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("SearchAssignments")]
        [HttpPost]
        public SearchForAssignmentsApiResponse SearchForAssignments([FromBody] GetJoinedCourseRequest objGetJoinedCourseRequest)
        {
            SearchForAssignmentsApiResponse objResponse = new SearchForAssignmentsApiResponse();
            try
            {
                objResponse.m_lsAssignments = objStudentService.SearchForAssignment(objGetJoinedCourseRequest.m_strSearchString, 10);
                if (objResponse.m_lsAssignments != null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("SearchTest")]
        [HttpPost]
        public SearchForTestResponse SearchTest([FromBody] GetJoinedCourseRequest objGetJoinedCourseRequest)
        {
            SearchForTestResponse objResponse = new SearchForTestResponse();
            try
            {
                objResponse.m_lsTestDetailsModel = objStudentService.SearchForTest(objGetJoinedCourseRequest.m_strSearchString, 10);
                if (objResponse.m_lsTestDetailsModel != null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("GetAllAssignmentSubmissionsForStudent")]
        [HttpPost]
        public AssignmentSubmissionOfStudnetResponse GetAllAssignmentSubmissionForStudent(long StudentId)
        {
            AssignmentSubmissionOfStudnetResponse objResponse = new AssignmentSubmissionOfStudnetResponse();
            try
            {
                objResponse.m_lsOfStudentResponse = objStudentService.GetAllAssignmentSubmissions(StudentId);
                if (objResponse.m_lsOfStudentResponse != null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllAssignmentSubmissionForStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("GetAllTestSubmissionsForStudent")]
        [HttpPost]
        public TestSubmissionOfStudentResponse GetAllTestSubmissionForStudent(long StudentId)
        {
            TestSubmissionOfStudentResponse objResponse = new TestSubmissionOfStudentResponse();
            try
            {
                objResponse.m_lsTestSubmissionModal = objStudentService.GetAllTestSubmissions(StudentId);
                if (objResponse.m_lsTestSubmissionModal != null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllAssignmentSubmissionForStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("InsertAssignmentResponse")]
        [HttpPost]
        public SubmitAssignmentResponse InsertAssignmentResponse([FromBody] AssignmentSubmissionRequest objGetJoinedCourseRequest)
        {
            SubmitAssignmentResponse objResponse = new SubmitAssignmentResponse();
            try
            {
                if (objStudentService.InserAssignmentResponse(objGetJoinedCourseRequest))
                {
                    objResponse.m_llSubmissionId = objGetJoinedCourseRequest.m_llSubmissionId;
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignmentResponse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("InsertTestResponse")]
        [HttpPost]
        public SubmitAssignmentResponse InsertTestResponse([FromBody] TestSubmissionRequest objInsertTestResponse)
        {
            SubmitAssignmentResponse objResponse = new SubmitAssignmentResponse();
            try
            {
                if (objStudentService.InsertTestResponse(objInsertTestResponse))
                {
                    objResponse.m_llSubmissionId = objInsertTestResponse.m_llSubmissionId;
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignmentResponse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("AssignmentSubmissionDetails")]
        [HttpPost]
        public GetAssignmentSubssionDetials GetAssignmentSubmissionDetails(long id)
        {
            GetAssignmentSubssionDetials objResponse =null;
            try
            {
                objResponse = objStudentService.GetAssignmentResponse(id);
                if (objResponse!=null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse = new GetAssignmentSubssionDetials();
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAssignmentSubmissionDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("TestSubmissionDetails")]
        [HttpPost]
        public GetTestSubmissionDetailsResponse GetTestSubmissionDetails(long id)
        {
            GetTestSubmissionDetailsResponse objResponse = null;
            try
            {
                objResponse = objStudentService.GetTestResponse(id);
                if (objResponse != null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse = new GetTestSubmissionDetailsResponse();
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAssignmentSubmissionDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
    }  
}

