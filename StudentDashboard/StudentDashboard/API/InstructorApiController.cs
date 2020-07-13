using StudentDashboard.DTO;
using StudentDashboard.HttpRequest;
using StudentDashboard.HttpResponse;
using StudentDashboard.HttpResponse.ClassRoom;
using StudentDashboard.Models.Course;
using StudentDashboard.Models.Instructor;
using StudentDashboard.Security;
using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using StudentDashboard.Zoom;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudentDashboard.API
{
    [JwtAuthentication]
    [RoutePrefix("api/v1/instructor")]
    public class InstructorApiController : ApiController
    {
        StringBuilder m_strLogMessage = new StringBuilder();
        HomeDTO objHomeDTO = new HomeDTO();
        HomeService objHomeService = new HomeService();
        
        [Route("addcourse")]
        [HttpPost]
        public async Task<InsertNewCourseResponse> InsertNewCourse([FromBody]CourseModel objCourseModel)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            int InstructorId = -1;
            int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
            InsertNewCourseResponse objInsertNewCourseResponse = new InsertNewCourseResponse();
            if(objCourseModel==null)
            {
               
                objInsertNewCourseResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                objInsertNewCourseResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
            }
            else
            {
                objCourseModel.m_iInstructorId = InstructorId;
                if (await objHomeService.InsertNewCourse(objCourseModel))
                {
                    objInsertNewCourseResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objInsertNewCourseResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    objInsertNewCourseResponse.m_llCourseId = objCourseModel.m_llCourseId;
                }
            }
            return objInsertNewCourseResponse;
        }
        private int GetInstructorIdInRequest()
        {
            int InstructorId = -1;
            try
            {
                if (ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
                {
                    int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorIdInRequest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return InstructorId;

        }
        [Route("addindex")]
        public async Task<InsertNewIndexResponse> InsertNewIndex([FromBody] IndexModel objIndexModel)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            string strCurrentMethodName = "InsertNewIndex";
            InsertNewIndexResponse objInsertNewIndexResponse = new InsertNewIndexResponse();
            try
            {
                if(objIndexModel==null)
                {
                    objInsertNewIndexResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objInsertNewIndexResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
                else
                {
                    int InstructorId = -1;
                    int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
                    if (InstructorId!=-1&&await objHomeDTO.CheckCourseIdExistsForInstrcutor(InstructorId, objIndexModel.m_llCourseId))
                    {
                        if (objHomeDTO.InsertNewIndex(objIndexModel))
                        {

                            objInsertNewIndexResponse.m_llIndexId = objIndexModel.m_llIndexId;
                            objInsertNewIndexResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                            objInsertNewIndexResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                        }
                    }
                   
                }
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objInsertNewIndexResponse;
        }
        
        [Route("addtopic")]
        public async Task<InsertNewIndexResponse> InsertTopics([FromBody] IndexModel objIndexModel)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            InsertNewIndexResponse objInsertNewIndexResponse = new InsertNewIndexResponse();
            try
            {
                if (objIndexModel == null)
                {
                    objInsertNewIndexResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objInsertNewIndexResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
                else
                {
                    int InstructorId = -1;
                    int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
                    if(InstructorId!=-1&&await objHomeDTO.CheckIndexIdExistsForInstrcutor(InstructorId,objIndexModel.m_llIndexId))
                    {
                        if (await objHomeService.InsertTopics(objIndexModel))
                        {
                            objInsertNewIndexResponse.m_llIndexId = objIndexModel.m_llIndexId;
                            objInsertNewIndexResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                            objInsertNewIndexResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                        }
                    }   
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertTopics", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objInsertNewIndexResponse;
        }
        [Route("addassignment")]
        public async Task<AddAssgnmentResponse> InsertNewAssignment(AssignmentModel objAssignmentModel)
        {
            AddAssgnmentResponse objAddAssgnmentResponse = new AddAssgnmentResponse();
            try
            {
                if(await objHomeService.InsertAssignment(objAssignmentModel))
                {
                    objAddAssgnmentResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objAddAssgnmentResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objAddAssgnmentResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objAddAssgnmentResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objAddAssgnmentResponse;
        }
        [HttpPost]
        [Route("addnewassignment")]
        public async Task<AddAssgnmentResponse> InsertNewIndependentAssignment(AssignmentModel objAssignmentModel)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            int InstructorId = -1;
            int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
            objAssignmentModel.m_iInstructorId = InstructorId;
            AddAssgnmentResponse objAddAssgnmentResponse = new AddAssgnmentResponse();
            try
            {
                if (objAssignmentModel != null)
                {
                    if (await objHomeService.InsertNewIndependentAssignment(objAssignmentModel))
                    {
                        objAddAssgnmentResponse.m_llAssignmentId = objAssignmentModel.m_llAssignemntId;
                        objAddAssgnmentResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objAddAssgnmentResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
                    else
                    {
                        objAddAssgnmentResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                        objAddAssgnmentResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objAddAssgnmentResponse;
        }
        [Route("addtest")]
        public async Task<AddTestResponse> InsertNewTest(TestModel objTestModel)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            int InstructorId = -1;
            int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
            AddTestResponse objAddTestResponse = new AddTestResponse();
            try
            {
                objTestModel.m_iInstructorId = InstructorId;
                if (await objHomeService.InsertTest(objTestModel))
                {
                    objAddTestResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objAddTestResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
            }
            catch(Exception Ex)
            {
                objAddTestResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                objAddTestResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);

            }
            return objAddTestResponse;
        }
        [Route("FetchTestDetails")]
        [HttpPost]
        public async Task<TestModel> GetTestDetails(long id)
        {
            TestModel objResponse = null;
            try
            {
                objResponse = await objHomeService.GetIndependentTestDetails(id);
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
        [HttpPost]
        [Route("addnewtest")]
        public async Task<AddAssgnmentResponse> InsertNewIndependentTest(TestModel objTestModel)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            int InstructorId = -1;
            int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
            AddAssgnmentResponse objAddAssgnmentResponse = new AddAssgnmentResponse();
            try
            {
                objTestModel.m_iInstructorId = InstructorId;
                if (objTestModel != null)
                {
                    if (await objHomeService.InsertNewIndependentTest(objTestModel))
                    {
                        objAddAssgnmentResponse.m_llAssignmentId = objTestModel.m_llTestId;
                        objAddAssgnmentResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objAddAssgnmentResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
                    else
                    {
                        objAddAssgnmentResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                        objAddAssgnmentResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objAddAssgnmentResponse;
        }
        [HttpPost]
        [Route("courses")]
        public async Task< GetAllCourseDetailsForInstructorResponseModel> GetAllCoursesOfInstructor()
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            int InstructorId = -1;
            int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
            GetAllCourseDetailsForInstructorResponseModel objResponse = new GetAllCourseDetailsForInstructorResponseModel();
            try
            {
                objResponse.m_lsCourseModel = await objHomeService.GetAllCourseDetailsForInstructor(InstructorId);
                if (objResponse.m_lsCourseModel != null)
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
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllCoursesOfInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("getcoursedetails")]
        public async Task<GetCourseDetailsApiResponse> GetCourseDetails(long CourseId)
        {
            GetCourseDetailsApiResponse objResponse = new GetCourseDetailsApiResponse();
            try
            {
                objResponse = await objHomeService.GetCourseDetails(CourseId);
                if (objResponse!=null)
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
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("deletecourse")]
        public async Task<APIDefaultResponse> DeleteCourse(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
                {
                    return null;
                }
                int InstructorId = -1;
                int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
                if (await objHomeService.CheckCourseIdExistsForInstrcutor(InstructorId, id))
                {
                    if (await objHomeService.DeleteCourse(id))
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
        [Route("activatecourse")]
        public async Task<APIDefaultResponse> ActivateCourse(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorId = -1;
                int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
                if(await objHomeService.CheckCourseIdExistsForInstrcutor(InstructorId,id))
                {
                    if (await objHomeService.ActivateCourse(id))
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
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

        [HttpPost]
        [Route("assignments")]
        public async Task<AllInstructorAssignmentsApiResponse> GetAllAssignmentsForInstructoe()
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            int InstructorId = -1;
            int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
            AllInstructorAssignmentsApiResponse objResponse = new AllInstructorAssignmentsApiResponse();
            try
            {

                objResponse.m_lsAssignments = await objHomeService.GetAssignmentForInstructor(InstructorId);
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllCoursesOfInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("tests")]
        public async Task<AllInstructorTestsApiResponse> GetAllTestsForInstructor()
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            int InstructorId = -1;
            int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
            AllInstructorTestsApiResponse objResponse = new AllInstructorTestsApiResponse();
            try
            {

                objResponse.m_lsTestDetailsModel =await objHomeService.GetInstructorTestDetails(InstructorId);
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllCoursesOfInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("AssignmentSubmissionDetails")]
        [HttpPost]
        public async Task<GetAssignmentSubssionDetials> GetAssignmentSubmissionDetails(long id,long StudentId)
        {
            GetAssignmentSubssionDetials objResponse = null;
            try
            {
                objResponse = await objHomeService.GetAssignmentResponse(id, StudentId);
                if (objResponse != null)
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
        [HttpPost]
        [Route("getactivity")]
        public async Task<GetInstructorActivityResponse> GetInstructorActivityDetails()
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            int InstructorId = -1;
            int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
            GetInstructorActivityResponse objResponse = new GetInstructorActivityResponse();
            try
            {
                objResponse.m_lsActivityDetails = await objHomeService.GetInstructorActivityDetails(InstructorId);
                if (objResponse.m_lsActivityDetails!=null)
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
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorActivityResponse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;

        }
        [Route("FetchAssignmentDetails")]
        [HttpPost]
        public async Task<AssignmentModel> GetAssignmentDetails(long AssignmentId)
        {
            AssignmentModel objResponse = null;
            try
            {
                objResponse = await objHomeService.GetIndependentAssignmentDetails(AssignmentId);
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
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("activateassignment")]
        public async Task<APIDefaultResponse> ActivateAssignment(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.ActivateAssignment(id))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ActivateAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("deleteassignment")]
        public async Task<APIDefaultResponse> DeleteAssignment(long AssignmentId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.DeleteAssignment(AssignmentId))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("DeleteIndependentAssignment")]
        public async Task<APIDefaultResponse> DeleteIndependentAssignment(long AssignmentId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.DeleteIndependentAssignment(AssignmentId))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("DeleteIndependentTest")]
        public async Task<APIDefaultResponse> DeleteIndependentTest(long TestId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.DeleteIndependentTest(TestId))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("activatetest")]
        public async Task<APIDefaultResponse> ActivateTest(long testid)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.ActivateTest(testid))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ActivateTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("deletetest")]
        public async Task<APIDefaultResponse> DeleteTest(long TestId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.DeleteTest(TestId))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("DeleteTestOfCourse")]
        public async Task<APIDefaultResponse> DeleteTestOfCourse(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.DeleteTestOfCourse(id))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteTestOfCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("DeleteMcqQuestion")]
        public async Task<APIDefaultResponse> DeleteMcqAssignmentQuestion(long id,long AssignmentId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorIdInRequest = GetInstructorIdInRequest();
                if(InstructorIdInRequest!=-1&&await objHomeService.CheckAssignmentIdExistsForInstrcutor(InstructorIdInRequest, AssignmentId))
                {
                    if (await objHomeService.DeleteMcqAssignmentQuestion(id))
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteMcqAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("UpdateMcqQuestion")]
        public async Task<APIDefaultResponse> UpdateMcqAssignmentQuestion([FromBody]McqQuestion McqQuestion)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.UpdateMcqAssignmentQuestion(McqQuestion))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateMcqAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }

        [HttpPost]
        [Route("AddMcqQuestion")]
        public async Task<APIDefaultResponse> AddMcqQuestion([FromBody]McqQuestion McqQuestion)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.InsertNewMcqQuestion(McqQuestion))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AddMcqQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("AddMcqQuestionToAssignment")]
        public async Task<APIDefaultResponse> AddMcqQuestionToAssignment([FromBody]McqQuestion McqQuestion)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorIdInRequest = GetInstructorIdInRequest();
                if(InstructorIdInRequest!=-1&&McqQuestion != null&& await objHomeService.CheckAssignmentIdExistsForInstrcutor(InstructorIdInRequest,McqQuestion.m_llAssignmentId))
                {
                    if (await objHomeService.InsertNewMcqAssignmentQuestion(McqQuestion))
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AddMcqQuestionToAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("contact")]
        public async Task<APIDefaultResponse> ContactUs([FromBody] ContactUsApiRequest objContactUsApiRequest)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objContactUsApiRequest != null&& await objHomeService.InserContatUsRequest(objContactUsApiRequest))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ContactUs", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("DeleteTopic")]
        public async Task<APIDefaultResponse> DeleteIndexTopic(TopicModel objTopicModel)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorId = -1;
                int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
                if(objTopicModel!=null&&await objHomeService.CheckIndexIdExistsForInstrcutor(InstructorId, objTopicModel.m_llIndexId))
                {
                    if (await objHomeService.DeleteIndexTopic(objTopicModel.m_llTopicId))
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
               
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteIndexTopic", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        //[HttpPost]
        //[Route("DeleteIndex")]
        //public async Task<APIDefaultResponse> DeleteCourseIndex(long id)
        //{
        //    APIDefaultResponse objResponse = new APIDefaultResponse();
        //    try
        //    {
        //        if (objHomeService.DeleteIndex(id))
        //        {
        //            objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
        //            objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
        //        }
        //        else
        //        {
        //            objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
        //            objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
        //        m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteCourseIndex", Ex.ToString());
        //        m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
        //        MainLogger.Error(m_strLogMessage);
        //    }
        //    return objResponse;
        //}
        [HttpPost]
        [Route("UpdateTopic")]
        public async Task<APIDefaultResponse> UpdateIndexTopic([FromBody] TopicModel objTopicModel)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorId = -1;
                int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
                if(objTopicModel != null&&await objHomeService.CheckIndexIdExistsForInstrcutor(InstructorId,objTopicModel.m_llIndexId))
                {
                    if (await objHomeService.UpdateIndexTopic(objTopicModel))
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
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateIndexTopic", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("UpdateIndex")]
        public async Task<APIDefaultResponse> UpdateIndex([FromBody] IndexModel objIndexModel)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorId = -1;
                int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
                if(objIndexModel != null &&await objHomeService.CheckIndexIdExistsForInstrcutor(InstructorId,objIndexModel.m_llIndexId))
                {
                    if ( await objHomeService.UpdateCourseIndex(objIndexModel))
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
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateIndexTopic", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("DeleteIndex")]
        public async Task<APIDefaultResponse> DeleteIndex(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorId = -1;
                int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
                if(await objHomeService.CheckIndexIdExistsForInstrcutor(InstructorId,id))
                {
                    if (await objHomeService.DeleteIndex(id))
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
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("UpdateCourse")]
        public async Task<APIDefaultResponse> UpdateCourse(CourseDetailsModel objCourse)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorId = -1;
                int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
                if(objCourse!=null&&await objHomeService.CheckCourseIdExistsForInstrcutor(InstructorId,objCourse.m_llCourseid))
                {
                    if (await objHomeService.UpdateFullCourseDetails(objCourse))
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
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("AddNewTopic")]
        public async Task<APIDefaultResponse> AddTopicToIndex(TopicModel objTopic)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
                {
                    return null;
                }
                int InstructorId = -1;
                int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
                if(await objHomeService.CheckIndexIdExistsForInstrcutor(InstructorId,objTopic.m_llIndexId))
                {
                    if (objTopic != null && await objHomeService.InsertNewTopic(objTopic))
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
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("UpdateTestDetails")]
        public async Task<APIDefaultResponse> UpdateTestDetails(TestDetailsModel objTestDetails)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorIdInRequest = GetInstructorIdInRequest();
                if(objTestDetails!=null&& InstructorIdInRequest!=-1&&
                    await objHomeService.CheckTestIdExistsForInstrcutor(InstructorIdInRequest, objTestDetails.m_llTestId))
                {
                    if (objTestDetails != null && await objHomeService.UpdateTestDetails(objTestDetails))
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("AddNewMcqTestQuestion")]
        public async Task<APIDefaultResponse> AddNewMcqTestQuestion(McqQuestion objTestModel)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorIdInRequest = GetInstructorIdInRequest();
                if(objTestModel!=null&& InstructorIdInRequest!=-1&&await objHomeService.CheckTestIdExistsForInstrcutor(InstructorIdInRequest,objTestModel.m_llTestId))
                {
                    if (await objHomeService.InsertNewMcqQuestion(objTestModel))
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("UpdateMcqQuestionTestDetails")]
        public async Task<APIDefaultResponse> UpdateMcqQuestionTestDetails(McqQuestion objTestModel)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorIdInRequest = GetInstructorIdInRequest();
                if(objTestModel!=null&&InstructorIdInRequest!=-1&&await objHomeService.CheckTestIdExistsForInstrcutor(InstructorIdInRequest,objTestModel.m_llTestId))
                {
                    if (await objHomeService.UpdateMcqTestQuestion(objTestModel))
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("AddNewAssignmentToCourse")]
        public async Task<APIDefaultResponse> AddNewAssignment(AssignmentModel objAssignmentModel)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorIdInRequest = GetInstructorIdInRequest();
                if (objAssignmentModel != null && InstructorIdInRequest != -1 && 
                    await objHomeService.CheckCourseIdExistsForInstrcutor(InstructorIdInRequest, objAssignmentModel.m_llCourseId))
                {
                    objAssignmentModel.m_iInstructorId = InstructorIdInRequest;
                    if (await objHomeService.InsertNewSeperateAssignmentToCourse(objAssignmentModel))
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AddNewAssignmentToCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("DeleteMcqTestQuestion")]
        public async Task<APIDefaultResponse> DeleteMcqTestQuestion(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.DeleteMcqTestQuestion(id))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteMcqTestQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("InsertNewTestToCourse")]
        public async Task<APIDefaultResponse> InsertNewTestToCourse(TestModel objTestModel)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorIdInRequest = GetInstructorIdInRequest();
                if (objTestModel != null && InstructorIdInRequest != -1 &&
                    await objHomeService.CheckCourseIdExistsForInstrcutor(InstructorIdInRequest, objTestModel.m_llCourseId))
                {
                    objTestModel.m_iInstructorId = InstructorIdInRequest;
                    if (await objHomeService.InsertNewTestToCourse(objTestModel))
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
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewTestToCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("UpdateAssignmentDetails")]
        public async Task<APIDefaultResponse> UpdateAssignmentDetails(AssignmentModel objAssignmentModel)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorIdInRequest = GetInstructorIdInRequest();
                if(objAssignmentModel!=null&& InstructorIdInRequest!=-1)
                {
                    if (await objHomeService.UpdateAssignmentDetails(objAssignmentModel))
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewTestToCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("AddSubjectiveAssignmentQuestion")]
        public async Task<APIDefaultResponse> AddSubjectiveAssignmentQuestion(SubjectiveQuestion objSubjectiveQuestion)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.InsertSubjectiveAssignmentQuestion(objSubjectiveQuestion))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AddSubjectiveAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("UpdateSubjectiveAssignmentQuestion")]
        public async Task<APIDefaultResponse> UpdateSubjectiveAssignmentQuestion(SubjectiveQuestion objSubjectiveQuestion)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.UpdateSubjectiveAssignmentQuestion(objSubjectiveQuestion))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateSubjectiveAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("DeleteSubjectiveAssignmentOfCourse")]
        public async Task<APIDefaultResponse> DeleteSubjectiveAssignmentOfCourse(long AssignmentId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.DeleteSubjectiveAssignmentOfCourse(AssignmentId))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteSubjectiveAssignmentOfCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("DeleteSubjectiveAssignmentQuestion")]
        public async Task<APIDefaultResponse> DeleteSubjectiveAssignmentQuestion(long QuestionId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.DeleteSubjectiveAssignmentQuestion(QuestionId))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteSubjectiveAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("GetAllAssignmentsSubmission")]
        public async Task<AssignmentSubmissionResponse> GetAllAssignmentsSubmission(long AssignmentId)
        {
            AssignmentSubmissionResponse objResponse = new AssignmentSubmissionResponse();
            try
            {
                objResponse.m_lsAssignmentSubmissionResponseModal = await objHomeService.GetAllSubmissionsOfAnAssignment(AssignmentId);
                if (objResponse.m_lsAssignmentSubmissionResponseModal!=null)
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteSubjectiveAssignmentOfCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("GetAllTestSubmissions")]
        public async Task<AssignmentSubmissionResponse> GetAllTestSubmissions(long id)
        {
            AssignmentSubmissionResponse objResponse = new AssignmentSubmissionResponse();
            try
            {
                objResponse.m_lsAssignmentSubmissionResponseModal = await objHomeService.GetAllTestSubmissions(id);
                if (objResponse.m_lsAssignmentSubmissionResponseModal != null)
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteSubjectiveAssignmentOfCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("Joinee")]
        public async Task<CourseJoinedResponse> GetAllStudentsJoinedToInstructs()
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            int InstructorId = -1;
            int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
            
            CourseJoinedResponse objResponse = new CourseJoinedResponse();
            try
            {
                objResponse.m_lsStudentsJoined = await objHomeService.GetAllStudentsJoinedToInstructor(InstructorId);
                if (objResponse.m_lsStudentsJoined != null)
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteSubjectiveAssignmentOfCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("v2/InsertNewCourse")]
        public InsertCourseV2Response InsertNewCourseV2(InsertCourseV2Request objInsertCourseV2Request)
        {

            InsertCourseV2Response objResponse = new InsertCourseV2Response();
            try
            {
                if (objInsertCourseV2Request!=null&&objHomeService.InsertNewCourseV2(objInsertCourseV2Request))
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteSubjectiveAssignmentOfCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
       
        [HttpPost]
        [Route("GetAllAlert")]
        public async Task<GetAllAlertForInstructorResponse> GetAllAlertForInstructor()
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            int InstructorId = -1;
            int.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out InstructorId);
            GetAllAlertForInstructorResponse objResponse = new GetAllAlertForInstructorResponse();
            try
            {
                objResponse.m_lsInstructorAlertModal = await objHomeService.GetAllAlertOfInstructor(InstructorId);
                if (objResponse.m_lsInstructorAlertModal != null)
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteSubjectiveAssignmentOfCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [HttpPost]
        [Route("search")]
        public async Task<InstructorSearchResponse> GetInstructorSerachResponse(InstructorSearchRequest objInstructorSearchRequest)
        {
            InstructorSearchResponse objResponse = new InstructorSearchResponse();
            try
            {
               
                if (objInstructorSearchRequest != null)
                {
                    objResponse = await objHomeService.GetInstructorSearchDetails(objInstructorSearchRequest);
                    if (objResponse != null)
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
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
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorSerachResponse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("TestSubmissionDetails")]
        [HttpPost]
        public async Task<GetTestSubmissionDetailsResponse> GetTestSubmissionDetails(long id,long StudentId)
        {
            if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return null;
            }
            //long.TryParse(ControllerContext.RequestContext.Principal.Identity.Name, out StudentId);
            GetTestSubmissionDetailsResponse objResponse = null;
            try
            {
                objResponse = await objHomeService.GetTestResponse(id, StudentId);
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
        [Route("InsertClassRoom")]
        [HttpPost]
        public async Task<ClassroomInsertResponse> InsertNewClassroom(ClassRoomModal objClassRoomModal)
        {
            ClassroomInsertResponse objResponse = new ClassroomInsertResponse();
            try
            {
                int InstructorIdInRequest = GetInstructorIdInRequest();
                if(InstructorIdInRequest!=-1&& objClassRoomModal != null)
                {
                    objClassRoomModal.m_iInstrutcorId = InstructorIdInRequest;
                    objClassRoomModal.m_llClassRoomId = await objHomeService.InsertNewClassroom(objClassRoomModal);
                    if(objClassRoomModal.m_llClassRoomId!=-1)
                    {
                        objResponse.m_llClassroomId = objClassRoomModal.m_llClassRoomId;
                        objResponse.SetSuccessResponse();
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewClassroom", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("InsertNewPostToClassroom")]
        [HttpPost]
        public async Task<APIDefaultResponse> InsertNewPostToClassroom(ClassroomPostModal objClassroomPostModal)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorIdInRequest = GetInstructorIdInRequest();
                if (InstructorIdInRequest != -1 && objClassroomPostModal != null)
                { 
                    if (await objHomeService.InsertNewPostToClassroom(objClassroomPostModal))
                    {
                        
                        objResponse.SetSuccessResponse();
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewClassroom", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("Classrooms")]
        [HttpPost]
        public async Task<GetAllClassroomForInstructorResponse> GetAllClassRoomForInstructor()
        {
            GetAllClassroomForInstructorResponse objResponse = new GetAllClassroomForInstructorResponse();
            try
            {
                int InstructorIdInRequest = GetInstructorIdInRequest();
                if (InstructorIdInRequest != -1)
                {

                    objResponse.m_lsClassRoomModal = await objHomeService.GetAllClassroomForIsntrcutor(InstructorIdInRequest);
                    if(objResponse.m_lsClassRoomModal != null)
                    {
                        objResponse.SetSuccessResponse();
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewClassroom", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }

        [Route("getzoomtoken")]
        [HttpPost]
        public async Task<string> GenateToken(string meeting_no,string role)
        {
            string secret = "";
            try
            {
                secret = GenrateToke.GenerateToken(meeting_no, role);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewClassroom", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return secret;
        }
        [HttpPost]
        [Route("activateclassroom")]
        public async Task<APIDefaultResponse> ActivateClassroom(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorId = GetInstructorIdInRequest();
                if (InstructorId!=-1||await objHomeService.CheckCourseIdExistsForInstrcutor(InstructorId, id))
                {
                    if (await objHomeService.ActivateClassroom(id))
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
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
        [HttpPost]
        [Route("CloseClassroomMeeting")]
        public async Task<APIDefaultResponse> CloseClassroomMeeting(long MeetingId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                int InstructorId = GetInstructorIdInRequest();
                if (InstructorId != -1 && await objHomeService.MarkMeetingClosed(MeetingId))
                {
                    objResponse.SetSuccessResponse();
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
        [Route("GetAllMeetingsForClassroom")]
        public async Task<GetAllClassroomMeetingResponse> GetAllMeetingsForClassroom(long ClassroomId)
        {
            GetAllClassroomMeetingResponse objResponse = new GetAllClassroomMeetingResponse();
            try
            {
                int InstructorId = GetInstructorIdInRequest();
                if (InstructorId != -1 )
                {
                    objResponse.m_lsClassroomMeetingModal = await objHomeService.GetAllMeetingForClassroom(ClassroomId);
                    if(objResponse.m_lsClassroomMeetingModal!=null)
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
        [Route("GetAllStudnetJoinedToClassroom")]
        public async Task<GetAllStudentJoinedToClassroomResponse> GetAllStudnetJoinedToClassroom(long ClassroomId)
        {
            GetAllStudentJoinedToClassroomResponse objResponse = new GetAllStudentJoinedToClassroomResponse();
            try
            {
                int InstructorId = GetInstructorIdInRequest();
                if (InstructorId != -1)
                {
                    objResponse.m_lsStudentClassrromJoinModal = await objHomeService.GetAllStudentsJoinedToClassroomResponse(ClassroomId);
                    if (objResponse.m_lsStudentClassrromJoinModal != null)
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
