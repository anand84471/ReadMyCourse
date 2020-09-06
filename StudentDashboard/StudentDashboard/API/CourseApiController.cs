using Microsoft.AspNet.Identity;
using StudentDashboard.HttpRequest;
using StudentDashboard.HttpResponse;
using StudentDashboard.HttpResponse.ClassRoom;
using StudentDashboard.Models;
using StudentDashboard.Models.Course;
using StudentDashboard.Models.Student;
using StudentDashboard.Security;
using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using static StudentDashboard.Constants;

namespace StudentDashboard.API
{
    [JwtAuthentication]
    [RoutePrefix("api/v1/Course")]
    public class CourseApiController : ApiController
    {
        HomeService objHomeService = new HomeService();
        StudentService objStudentService = new StudentService();
        StringBuilder m_strLogMessage = new StringBuilder();
        StudentUserProvider objStudentUserProvider = new StudentUserProvider();
        [AllowAnonymous]
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
        [Route("FetchIndexProgress")]
        [HttpPost]
        public async Task<StudentIndexModal> FetchCourseIndexProgressDetailsForStudent(long IndexId)
        {
            StudentIndexModal objResponse = new StudentIndexModal();
            try
            {
                long StudentId = GetStudentIdInRequest();
                if(StudentId!=-1)
                {
                    objResponse = await objHomeService.GetIndexProgressForStudent(IndexId, StudentId);
                    if (objResponse != null)
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
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
            return objResponse;
        }
        //[Route("FetchStudentCourseProgress")]
        //[HttpPost]
        //public async Task<StudentCourseModal> FetchStudentCourseProgress(long CourseId)
        //{
        //    StudentCourseModal objResonse = new StudentCourseModal();
        //    try
        //    {
        //        objResonse = await objHomeService.GetAboutCourse(id);
        //    }
        //    catch (Exception Ex)
        //    {
        //        m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
        //        m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AboutCourseResponse", Ex.ToString());
        //        m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
        //        MainLogger.Error(m_strLogMessage);
        //    }
        //    return objResonse;
        //}
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
        [Route("FetchStudentAssignmentProgress")]
        [HttpPost]
        public async Task<StudentAssignmentProgressModal> FetchStudentTestprogress(long AssignmentId)
        {
            StudentAssignmentProgressModal objResponse = null;
            try
            {
                long StudentId = GetStudentIdInRequest();
                if(StudentId!=-1)
                {
                    objResponse = await objHomeService.FetchStudentAssignmentProgress(StudentId, AssignmentId);
                    if (objResponse != null)
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
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
            return objResponse;
        }
        [Route("FetchStudentTestprogress")]
        [HttpPost]
        public async Task<StudentTestProgressModal> FetchStudentAssignmentprogress(long id)
        {
            StudentTestProgressModal objResponse = null;
            long StudentId = GetStudentIdInRequest();
            try
            {
                if (StudentId != -1)
                {
                    objResponse = await objHomeService.GetStudentTestProgress(id, StudentId);
                    if (objResponse != null)
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
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
            return objResponse;
        }
        [Route("SearchCourse")]
        [HttpPost]
        public async Task<SearchCourseHttpResponse> SearchCourse([FromBody]SerchCourseRequest objSerchCourseRequest)
        {
            SearchCourseHttpResponse objResponse = new SearchCourseHttpResponse();
            try
            {
                if (objSerchCourseRequest != null)
                {
                    objResponse.lsCourseDetailsModel = await objStudentService.SearchForCourse(objSerchCourseRequest.m_strKey, 10, objSerchCourseRequest.m_iNoOfRowsFetched,
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
        [Route("AddCourseQuestion")]
        [HttpPost]
        public async Task<APIDefaultResponse> AddCourseQuestionByStudent([FromBody] StudentCourseQuestionModal objRequest)
        {
            SearchCourseHttpResponse objResponse = new SearchCourseHttpResponse();
            try
            {
                long StudentIdInRequest = GetStudentIdInRequest();
                if (StudentIdInRequest!=-1&&objRequest != null)
                {
                    objRequest.m_llStudentId = StudentIdInRequest;
                    if (await objStudentService.AddQuestionAskForCourse(objRequest))
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
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
        [Route("FetchQuestionAsked")]
        [HttpPost]
        public async Task<AllQuestionAskedByStudentForCourse>FetchQuestionAsked(long CourseId)
        {
            AllQuestionAskedByStudentForCourse objResponse = new AllQuestionAskedByStudentForCourse();
            try
            {
                long StudentIdInRequest = GetStudentIdInRequest();
                if (StudentIdInRequest != -1 )
                {
                    objResponse.m_lsStudentCourseQuestionModal = await objStudentService.GetAllQuestionOfStudentCourse(StudentIdInRequest, CourseId);
                    if(objResponse!=null)
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "FetchQuestionAsked", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("SearchCourseForStudent")]
        [HttpPost]
        public async Task< SearchCourseHttpResponse> SearchCourseForStudent([FromBody]SerchCourseRequest objSerchCourseRequest)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            long StudentId = -1;
            long.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out StudentId);

            SearchCourseHttpResponse objResponse = new SearchCourseHttpResponse();
            try
            {

                if (objSerchCourseRequest != null)
                {
                    objSerchCourseRequest.m_llStudentId = StudentId;
                    objResponse.lsCourseDetailsModel =await objStudentService.SearchForCourseForStudent(objSerchCourseRequest.m_strKey, 10, objSerchCourseRequest.m_iNoOfRowsFetched,
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
        public async Task<APIDefaultResponse> JoinCourse(long CourseId)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            long StudentId=-1;
            long.TryParse(ControllerContext.RequestContext.Principal.Identity.Name,out StudentId);
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objStudentService.JoinStudentToCourse(CourseId, StudentId))
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
        private long GetStudentIdInRequest()
        {
            long StudentId = -1;
            try
            {
                if (ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
                {
                    long.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out StudentId);
                }
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return StudentId;
        }
        [Route("JoinStudentToInstructor")]
        [HttpPost]
        public async Task<APIDefaultResponse> JoinInstructor(int InstructorId)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            long StudentId = -1;
            long.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out StudentId);
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objStudentService.JoinStudentToInstructor(StudentId, InstructorId))
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
        [Route("MarkTopicFinished")]
        [HttpPost]
        public async Task<APIDefaultResponse> MarkTopicFinished(long TopicId)
        {
          
            long StudentId = GetStudentIdInRequest();
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (StudentId!=-1&&await objStudentService.InsertReadTopicByStudent(StudentId, TopicId))
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
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
        public async Task<StudentJoinedCoursesResponse> GetJoinedCourses([FromBody] GetJoinedCourseRequest objGetJoinedCourseRequest)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated|| objGetJoinedCourseRequest==null)
            {
                return null;
            }
            long StudentId = -1;
            long.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out StudentId);
            StudentJoinedCoursesResponse objResponse = new StudentJoinedCoursesResponse();
            try
            {
                objGetJoinedCourseRequest.m_llStudentGid = StudentId;
                objResponse.lsStudentJoinedCoursesResponseModal =await objStudentService.SerachForJoinedCourses(
                    objGetJoinedCourseRequest.m_llStudentGid, objGetJoinedCourseRequest.m_strSearchString,Constants.MAX_ITEMS_TO_BE_RETURNED );
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
        public async Task<SearchInstructorResponse> InstructorSearch([FromBody] GetJoinedCourseRequest objGetJoinedCourseRequest)
        {

            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated || objGetJoinedCourseRequest == null)
            {
                return null;
            }
            long StudentId = -1;
            long.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out StudentId);
            SearchInstructorResponse objResponse = new SearchInstructorResponse();
            try
            {
                objGetJoinedCourseRequest.m_llStudentGid = StudentId;
                objResponse.lsSearchInstructorResponseModal =await objStudentService.SearchForInstructor(
                   objGetJoinedCourseRequest.m_strSearchString, Constants.MAX_ITEMS_TO_BE_RETURNED);
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
        public async Task<SearchInstructorResponse> GetJoinedInstructors([FromBody] GetJoinedCourseRequest objGetJoinedCourseRequest)
        {

            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated || objGetJoinedCourseRequest == null)
            {
                return null;
            }
            long StudentId = -1;
            long.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out StudentId);
            SearchInstructorResponse objResponse = new SearchInstructorResponse();
            try
            {
                objGetJoinedCourseRequest.m_llStudentGid = StudentId;
                objResponse.lsSearchInstructorResponseModal = await objStudentService.SearchForJoinedInstructor(objGetJoinedCourseRequest.m_llStudentGid,
                   objGetJoinedCourseRequest.m_strSearchString, Constants.MAX_ITEMS_TO_BE_RETURNED);
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
        public async Task<SearchForAssignmentsApiResponse> SearchForAssignments([FromBody] GetJoinedCourseRequest objGetJoinedCourseRequest)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated || objGetJoinedCourseRequest == null)
            {
                return null;
            }
            SearchForAssignmentsApiResponse objResponse = new SearchForAssignmentsApiResponse();
            try
            {
                objResponse.m_lsAssignments =await objStudentService.SearchForAssignment(objGetJoinedCourseRequest.m_strSearchString, Constants.MAX_ITEMS_TO_BE_RETURNED);
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
        public async Task<SearchForTestResponse> SearchTest([FromBody] GetJoinedCourseRequest objGetJoinedCourseRequest)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated || objGetJoinedCourseRequest == null)
            {
                return null;
            }
            SearchForTestResponse objResponse = new SearchForTestResponse();
            try
            {
                objResponse.m_lsTestDetailsModel = await objStudentService.SearchForTest(objGetJoinedCourseRequest.m_strSearchString, 10);
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
        public async Task<AssignmentSubmissionOfStudnetResponse> GetAllAssignmentSubmissionForStudent()
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated )
            {
                return null;
            }
            long StudentId = -1;
            long.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out StudentId);
            AssignmentSubmissionOfStudnetResponse objResponse = new AssignmentSubmissionOfStudnetResponse();
            try
            {
                objResponse.m_lsOfStudentResponse = await objStudentService.GetAllAssignmentSubmissions(StudentId);
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
        public async Task<TestSubmissionOfStudentResponse> GetAllTestSubmissionForStudent()
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            long StudentId = -1;
            long.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out StudentId);
            TestSubmissionOfStudentResponse objResponse = new TestSubmissionOfStudentResponse();
            try
            {
                objResponse.m_lsTestSubmissionModal = await objStudentService.GetAllTestSubmissions(StudentId);
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
        public SubmitAssignmentResponse InsertAssignmentResponse([FromBody] AssignmentSubmissionRequest objAssignmentSubmissionRequest)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated|| objAssignmentSubmissionRequest == null)
            {
                return null;
            }
            long StudentId = -1;
            long.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out StudentId);
            SubmitAssignmentResponse objResponse = new SubmitAssignmentResponse();
            try
            {
                objAssignmentSubmissionRequest.m_llStudentId = StudentId;
                if (objStudentService.InserAssignmentResponse(objAssignmentSubmissionRequest))
                {
                    objResponse.m_llSubmissionId = objAssignmentSubmissionRequest.m_llSubmissionId;
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

            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated || objInsertTestResponse == null)
            {
                return null;
            }
            long StudentId = -1;
            long.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out StudentId);
            SubmitAssignmentResponse objResponse = new SubmitAssignmentResponse();
            try
            {
                objInsertTestResponse.m_llStudentId = StudentId;
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
        public async Task<GetAssignmentSubssionDetials> GetAssignmentSubmissionDetails(long id)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            long StudentId = -1;
            long.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out StudentId);
            GetAssignmentSubssionDetials objResponse =null;
            try
            {
                objResponse = await objStudentService.GetAssignmentResponse(id,StudentId);
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
        public async Task<GetTestSubmissionDetailsResponse> GetTestSubmissionDetails(long id)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            long StudentId = -1;
            long.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out StudentId);
            GetTestSubmissionDetailsResponse objResponse = null;
            try
            {
                objResponse = await objStudentService.GetTestResponse(id, StudentId);
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
        [Route("GetClasroomDetails")]
        [HttpPost]
        public async Task<ClassroomDetailsResponse> GetClassroomDetails(long id)
        {
            ClassroomDetailsResponse objResponse = new ClassroomDetailsResponse();
            try
            {
                long StudentidInRequest = GetStudentIdInRequest();
                if (StudentidInRequest != -1)
                {
                    objResponse.m_objClassRoomModal = await objStudentService.GetClassroomDetailsForStudent(id);
                    if (objResponse != null)
                    {
                        objResponse.SetSuccessResponse();
                    }
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
        [Route("JoinClassroom")]
        [HttpPost]
        public async Task<APIDefaultResponse> JoinClassroom(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                long StudentidInRequest = GetStudentIdInRequest();
                if (StudentidInRequest != -1)
                {
                    if(await objStudentService.JoinClassroom(id,StudentidInRequest))
                    {
                        objResponse.SetSuccessResponse();
                    }
                    
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
        [Route("GetJoinedClassroom")]
        [HttpPost]
        public async Task<StudentClassroomResponse> GetJoinedClassrooms()
        {
            StudentClassroomResponse objResponse = new StudentClassroomResponse();
            try
            {
                long StudentidInRequest = GetStudentIdInRequest();
                if (StudentidInRequest != -1)
                {
                    objResponse.m_lsStudentClassroomModal = await objStudentService.GetJoinedClassroom(StudentidInRequest);
                    if (objResponse != null)
                    {
                        objResponse.SetSuccessResponse();
                    }
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
        [Route("JoinClassroomMeeting")]
        [HttpPost]
        public async Task<StudentClassroomResponse> JoinClassroomMeeting(long MeetingId)
        {
            StudentClassroomResponse objResponse = new StudentClassroomResponse();
            try
            {
                long StudentidInRequest = GetStudentIdInRequest();
                if (StudentidInRequest != -1)
                {
                    
                    if (await objStudentService.JoinClassroomMeeting(MeetingId, StudentidInRequest))
                    {
                        objResponse.SetSuccessResponse();
                    }
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
        [Route("InsertNewMessageToClassroom")]
        [HttpPost]
        public async Task<StudentClassroomResponse> InsertNewMessageToClassroom(InsertStudentMessageToClassroom insertStudentMessageToClassroom)
        {
            StudentClassroomResponse objResponse = new StudentClassroomResponse();
            try
            {
                long StudentidInRequest = GetStudentIdInRequest();
                if (StudentidInRequest != -1&&await objStudentService.CheckStudentAccessToClassroom(StudentidInRequest, insertStudentMessageToClassroom.m_llClassroomId))
                {
                    insertStudentMessageToClassroom.m_llStudentId = StudentidInRequest;
                    if (await objStudentService.InsertNewMessageToClassroomByStudent(insertStudentMessageToClassroom))
                    {
                        objResponse.SetSuccessResponse();
                    }
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
        [Route("GetAllClassroomMessage")]
        [HttpPost]
        public async Task<GetClassroomAllMessageResponseForStudent> GetAllClassroomMessage(long ClassroomId)
        {
            GetClassroomAllMessageResponseForStudent objResponse = new GetClassroomAllMessageResponseForStudent();
            try
            {
                long StudentidInRequest = GetStudentIdInRequest();
                if (StudentidInRequest != -1 && await objStudentService.CheckStudentAccessToClassroom(StudentidInRequest, ClassroomId))
                {
                    objResponse.m_lsGetClassroomAllMessageResponseForStudent =
                        await objStudentService.GetAllClassroomMessageForStudent(ClassroomId,StudentidInRequest
                        );
                    if (objResponse.m_lsGetClassroomAllMessageResponseForStudent != null)
                    {
                        objResponse.SetSuccessResponse();
                    }
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
        [Route("GetAllClassroomAssignment")]
        [HttpPost]
        public async Task<GetAllStudentClassroomAssignment> GetAllClassroomAssignment(long ClassroomId)
        {
            GetAllStudentClassroomAssignment objResponse = new GetAllStudentClassroomAssignment();
            try
            {
                long StudentidInRequest = GetStudentIdInRequest();
                if (StudentidInRequest != -1 && await objStudentService.CheckStudentAccessToClassroom(StudentidInRequest, ClassroomId))
                {
                    objResponse.m_lsStudentClassroomAssignmentModal =
                        await objStudentService.GetAllClassroomAssignment(ClassroomId, StudentidInRequest
                        );
                    if (objResponse.m_lsStudentClassroomAssignmentModal != null)
                    {
                        objResponse.SetSuccessResponse();
                    }
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
        [Route("GetAllClassroomSubmittedAssignment")]
        [HttpPost]
        public async Task<GetAllStudentClassroomAssignment> GetAllClassroomSubmittedAssignment(long ClassroomId)
        {
            GetAllStudentClassroomAssignment objResponse = new GetAllStudentClassroomAssignment();
            try
            {
                long StudentidInRequest = GetStudentIdInRequest();
                if (StudentidInRequest != -1 && await objStudentService.CheckStudentAccessToClassroom(StudentidInRequest, ClassroomId))
                {
                    objResponse.m_lsStudentClassroomAssignmentModal =
                        await objStudentService.GetAllClassroomSubmittedAssignment(ClassroomId, StudentidInRequest
                        );
                    if (objResponse.m_lsStudentClassroomAssignmentModal != null)
                    {
                        objResponse.SetSuccessResponse();
                    }
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
        [Route("GetAllClassroomTest")]
        [HttpPost]
        public async Task<GetAllStudentClassroomtTest> GetAllClassroomTest(long ClassroomId)
        {
            GetAllStudentClassroomtTest objResponse = new GetAllStudentClassroomtTest();
            try
            {
                long StudentidInRequest = GetStudentIdInRequest();
                if (StudentidInRequest != -1 && await objStudentService.CheckStudentAccessToClassroom(StudentidInRequest, ClassroomId))
                {
                    objResponse.m_lsStudentClassroomTestModal =
                        await objStudentService.GetAllClassroomTest(ClassroomId, StudentidInRequest
                        );
                    if (objResponse.m_lsStudentClassroomTestModal != null)
                    {
                        objResponse.SetSuccessResponse();
                    }
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
        [Route("GetAllClassroomTestSubmissions")]
        [HttpPost]
        public async Task<GetAllStudentClassroomtTest> GetAllClassroomTestSubmissions(long ClassroomId)
        {
            GetAllStudentClassroomtTest objResponse = new GetAllStudentClassroomtTest();
            try
            {
                long StudentidInRequest = GetStudentIdInRequest();
                if (StudentidInRequest != -1 && await objStudentService.CheckStudentAccessToClassroom(StudentidInRequest, ClassroomId))
                {
                    objResponse.m_lsStudentClassroomTestModal =
                        await objStudentService.GetAllClassroomTestSubmissons(ClassroomId, StudentidInRequest
                        );
                    if (objResponse.m_lsStudentClassroomTestModal != null)
                    {
                        objResponse.SetSuccessResponse();
                    }
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
        [HttpPost]
        [Route("UploadImage")]
        public async Task<InstructorFileUploadResponse> UploadImage()
        {
            InstructorFileUploadResponse objResponse = new InstructorFileUploadResponse();
            try
            {
                long StudentIdInRequest = GetStudentIdInRequest();
                if (StudentIdInRequest != -1)
                {
                    List<AwsFileUploadRequest> lsAwsFileUploadRequest = new List<AwsFileUploadRequest>();
                    if (!Request.Content.IsMimeMultipartContent())
                    {
                        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                    }
                    string rootpath = HttpContext.Current.Server.MapPath("~/Uploads/Student/Images");

                    var provider = new MultipartFileStreamProvider(rootpath);

                    var task = await Request.Content.ReadAsMultipartAsync(provider).

                        ContinueWith(t =>
                        {
                            if (t.IsCanceled || t.IsFaulted)
                            {
                                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                            }
                            foreach (MultipartFileData item in provider.FileData)
                            {
                                try
                                {
                                    string name = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                                    string newfilename = StudentIdInRequest.ToString() + "_" + Guid.NewGuid() + Path.GetExtension(name);
                                    File.Move(item.LocalFileName, Path.Combine(rootpath, newfilename));
                                    Uri baseuri = new Uri(Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty));
                                    string fileRelativePath = "~/Uploads/Student/Images/" + newfilename;
                                    Uri filefullpath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));
                                    lsAwsFileUploadRequest.Add(new AwsFileUploadRequest(newfilename, fileRelativePath));
                                }
                                catch (Exception Ex)
                                {
                                    throw new Exception();
                                }
                            }

                            return Request.CreateResponse(HttpStatusCode.Created);
                        });
                    objResponse.m_strAwsLocation = await objHomeService.UploadFiles(lsAwsFileUploadRequest, (int)FileUploadTypeId.IMAGE);
                    if (objResponse.m_strAwsLocation != null)
                    {
                        objResponse.SetSuccessResponse();
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateClassroomDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("ClassroomAttachments")]
        public async Task<GetAllClassroomAttachmentResponse> GetAllClassroomAttachments(long ClassroomId)
        {
            GetAllClassroomAttachmentResponse objResponse = new GetAllClassroomAttachmentResponse();
            try
            {
                long StudentIdInRequest = GetStudentIdInRequest();
                if (StudentIdInRequest != -1 && await objStudentService.CheckStudentAccessToClassroom(StudentIdInRequest,ClassroomId))
                {
                    objResponse.m_lsAttachments = await objHomeService.GetAllClassroomAttachments(ClassroomId);
                    if (objResponse.m_lsAttachments != null)
                    {
                        objResponse.SetSuccessResponse();
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateClassroomDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("GetAllClassroomMessageAfterLast")]
        public async Task<GetClassroomAllMessageResponseForStudent> GetAllClassroomMessageAfterLastMessage(long ClassroomId, long LastMessageId)
        {
            GetClassroomAllMessageResponseForStudent objResponse = new GetClassroomAllMessageResponseForStudent();
            try
            {
                long StudentId = GetStudentIdInRequest();
                if (StudentId != -1 && await objStudentService.CheckStudentAccessToClassroom(StudentId,ClassroomId))
                {
                    objResponse.m_lsGetClassroomAllMessageResponseForStudent = await objStudentService.GetAllClassroomLastMessagesForStudentAfterLast(ClassroomId, LastMessageId,StudentId);
                    if (objResponse.m_lsGetClassroomAllMessageResponseForStudent != null)
                    {
                        objResponse.SetSuccessResponse();
                    }
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
        [HttpPost]
        [Route("SearchClassroom")]
        public async Task<SearchStudentPublicClassroomResponse> GetPublicClassroom(long LastClassroomId, string SearchString)
        {
            SearchStudentPublicClassroomResponse objResponse = new SearchStudentPublicClassroomResponse();
            try
            {
                long StudentId = GetStudentIdInRequest();
                if (StudentId != -1 )
                {
                    objResponse.m_lsGetPublicClassroomsResponse = await objStudentService.SearchClassroom(LastClassroomId, StudentId,SearchString);
                    if (objResponse.m_lsGetPublicClassroomsResponse != null)
                    {
                        objResponse.SetSuccessResponse();
                    }
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
    }  


}

