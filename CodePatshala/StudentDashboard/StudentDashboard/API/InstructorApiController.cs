using StudentDashboard.DTO;
using StudentDashboard.HttpRequest;
using StudentDashboard.HttpResponse;
using StudentDashboard.Models.Course;
using StudentDashboard.Security;
using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudentDashboard.API
{
    [Authorize]
    [RoutePrefix("api/v1/instructor")]
    public class InstructorApiController : ApiController
    {
        StringBuilder m_strLogMessage = new StringBuilder();
        HomeDTO objHomeDTO = new HomeDTO();
        HomeService objHomeService = new HomeService(); 
        [Route("addcourse")]
        [HttpPost]
        [JwtAuthentication]
        public async Task<InsertNewCourseResponse> InsertNewCourse([FromBody]CourseModel objCourseModel)
        {
            InsertNewCourseResponse objInsertNewCourseResponse = new InsertNewCourseResponse();
            if(objCourseModel==null)
            {
                objInsertNewCourseResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                objInsertNewCourseResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
            }
            else
            {
                if(await objHomeService.InsertNewCourse(objCourseModel))
                {
                    objInsertNewCourseResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objInsertNewCourseResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    objInsertNewCourseResponse.m_llCourseId = objCourseModel.m_llCourseId;
                }
            }
            return objInsertNewCourseResponse;
        }
        [Route("addindex")]
        public InsertNewIndexResponse InsertNewIndex([FromBody] IndexModel objIndexModel)
        {
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
                    if(objHomeDTO.InsertNewIndex(objIndexModel))
                    {
                        objInsertNewIndexResponse.m_llIndexId = objIndexModel.m_llIndexId;
                        objInsertNewIndexResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objInsertNewIndexResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
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
                    if (await objHomeService.InsertTopics(objIndexModel))
                    {
                        objInsertNewIndexResponse.m_llIndexId = objIndexModel.m_llIndexId;
                        objInsertNewIndexResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objInsertNewIndexResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
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
            AddAssgnmentResponse objAddAssgnmentResponse = new AddAssgnmentResponse();
            try
            {
                if (objAssignmentModel != null)
                {
                    if (await objHomeService.InsertNewIndependentAssignment(objAssignmentModel))
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

            AddTestResponse objAddTestResponse = new AddTestResponse();
            try
            {
                if(await objHomeService.InsertTest(objTestModel))
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
        [HttpPost]
        [Route("addnewtest")]
        public async Task<AddAssgnmentResponse> InsertNewIndependentTest(TestModel objTestModel)
        {
            AddAssgnmentResponse objAddAssgnmentResponse = new AddAssgnmentResponse();
            try
            {
                if (objTestModel != null)
                {
                    if (await objHomeService.InsertNewIndependentTest(objTestModel))
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
        public async Task< GetAllCourseDetailsForInstructorResponseModel> GetAllCoursesOfInstructor(string InstructorUserName)
        {
            GetAllCourseDetailsForInstructorResponseModel objResponse = new GetAllCourseDetailsForInstructorResponseModel();
            try
            {
               
                objResponse.m_lsCourseModel = await objHomeService.GetAllCourseDetailsForInstructor(InstructorUserName);
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
        public async Task<APIDefaultResponse> ActivateCourse(long CourseId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.ActivateCourse(CourseId))
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

        [HttpPost]
        [Route("assignments")]
        public async Task<AllInstructorAssignmentsApiResponse> GetAllAssignmentsForInstructoe(int id)
        {
            AllInstructorAssignmentsApiResponse objResponse = new AllInstructorAssignmentsApiResponse();
            try
            {

                objResponse.m_lsAssignments = await objHomeService.GetAssignmentForInstructor(id);
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
        public async Task<AllInstructorTestsApiResponse> GetAllTestsForInstructor(int id)
        {
            AllInstructorTestsApiResponse objResponse = new AllInstructorTestsApiResponse();
            try
            {

                objResponse.m_lsTestDetailsModel =await objHomeService.GetInstructorTestDetails(id);
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
        [HttpPost]
        [Route("getactivity")]
        public async Task<GetInstructorActivityResponse> GetInstructorActivityDetails(int id)
        {
            GetInstructorActivityResponse objResponse = new GetInstructorActivityResponse();
            try
            {
                objResponse.m_lsActivityDetails = await objHomeService.GetInstructorActivityDetails(id);
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
        [HttpPost]
        [Route("activatecourse")]
        public async Task<APIDefaultResponse> ActivateAssignment(long AssignmentId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.ActivateAssignment(AssignmentId))
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
        public async Task<APIDefaultResponse> DeleteMcqAssignmentQuestion(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.DeleteMcqAssignmentQuestion(id))
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
                if (await objHomeService.InsertNewMcqAssignmentQuestion(McqQuestion))
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
        public async Task<APIDefaultResponse> DeleteIndexTopic(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (await objHomeService.DeleteIndexTopic(id))
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
                if (objTopicModel != null && await objHomeService.UpdateIndexTopic(objTopicModel))
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
                if (objIndexModel != null && await objHomeService.UpdateCourseIndex(objIndexModel))
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
                if (objCourse!=null&& await objHomeService.UpdateFullCourseDetails(objCourse))
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
                if (objTestDetails != null && await objHomeService.UpdateTestDetails(objTestDetails))
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
                if (objTestModel != null && await objHomeService.InsertNewMcqQuestion(objTestModel))
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
                if (objTestModel != null && await objHomeService.UpdateMcqTestQuestion(objTestModel))
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
                if (objAssignmentModel != null && await objHomeService.InsertNewSeperateAssignmentToCourse(objAssignmentModel))
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
                if (await objHomeService.UpdateAssignmentDetails(objAssignmentModel))
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
        public async Task<CourseJoinedResponse> GetAllStudentsJoinedToInstructs(int id)
        {
            CourseJoinedResponse objResponse = new CourseJoinedResponse();
            try
            {
                objResponse.m_lsStudentsJoined = await objHomeService.GetAllStudentsJoinedToInstructor(id);
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
        public async Task<GetAllAlertForInstructorResponse> GetAllAlertForInstructor(int id)
        {
            GetAllAlertForInstructorResponse objResponse = new GetAllAlertForInstructorResponse();
            try
            {
                objResponse.m_lsInstructorAlertModal = await objHomeService.GetAllAlertOfInstructor(id);
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
    }
   
}
