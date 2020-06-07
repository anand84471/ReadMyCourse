﻿using StudentDashboard.DTO;
using StudentDashboard.HttpRequest;
using StudentDashboard.HttpResponse;
using StudentDashboard.Models.Course;
using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using System;
using System.Text;
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
        public InsertNewCourseResponse InsertNewCourse([FromBody]CourseModel objCourseModel)
        {
            InsertNewCourseResponse objInsertNewCourseResponse = new InsertNewCourseResponse();
            if(objCourseModel==null)
            {
                objInsertNewCourseResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                objInsertNewCourseResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
            }
            else
            {
                if(objHomeService.InsertNewCourse(objCourseModel))
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
        public InsertNewIndexResponse InsertTopics([FromBody] IndexModel objIndexModel)
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
                    if (objHomeService.InsertTopics(objIndexModel))
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
        public AddAssgnmentResponse InsertNewAssignment(AssignmentModel objAssignmentModel)
        {
            AddAssgnmentResponse objAddAssgnmentResponse = new AddAssgnmentResponse();
            try
            {
                if(objHomeService.InsertAssignment(objAssignmentModel))
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
        public AddAssgnmentResponse InsertNewIndependentAssignment(AssignmentModel objAssignmentModel)
        {
            AddAssgnmentResponse objAddAssgnmentResponse = new AddAssgnmentResponse();
            try
            {
                if (objAssignmentModel != null)
                {
                    if (objHomeService.InsertNewIndependentAssignment(objAssignmentModel))
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
        public AddTestResponse InsertNewTest(TestModel objTestModel)
        {

            AddTestResponse objAddTestResponse = new AddTestResponse();
            try
            {
                if(objHomeService.InsertTest(objTestModel))
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
        public AddAssgnmentResponse InsertNewIndependentTest(TestModel objTestModel)
        {
            AddAssgnmentResponse objAddAssgnmentResponse = new AddAssgnmentResponse();
            try
            {
                if (objTestModel != null)
                {
                    if (objHomeService.InsertNewIndependentTest(objTestModel))
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
        public GetAllCourseDetailsForInstructorResponseModel GetAllCoursesOfInstructor(string InstructorUserName)
        {
            GetAllCourseDetailsForInstructorResponseModel objResponse = new GetAllCourseDetailsForInstructorResponseModel();
            try
            {
               
                objResponse.m_lsCourseModel = objHomeService.GetAllCourseDetailsForInstructor(InstructorUserName);
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
        public GetCourseDetailsApiResponse GetCourseDetails(long CourseId)
        {
            GetCourseDetailsApiResponse objResponse = new GetCourseDetailsApiResponse();
            try
            {
                objResponse = objHomeService.GetCourseDetails(CourseId);
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
        public APIDefaultResponse DeleteCourse(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.DeleteCourse(id))
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
        public APIDefaultResponse ActivateCourse(long CourseId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.ActivateCourse(CourseId))
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
        public AllInstructorAssignmentsApiResponse GetAllAssignmentsForInstructoe(int id)
        {
            AllInstructorAssignmentsApiResponse objResponse = new AllInstructorAssignmentsApiResponse();
            try
            {

                objResponse.m_lsAssignments = objHomeService.GetAssignmentForInstructor(id);
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
        public AllInstructorTestsApiResponse GetAllTestsForInstructor(int id)
        {
            AllInstructorTestsApiResponse objResponse = new AllInstructorTestsApiResponse();
            try
            {

                objResponse.m_lsTestDetailsModel = objHomeService.GetInstructorTestDetails(id);
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
        public GetInstructorActivityResponse GetInstructorActivityDetails(int id)
        {
            GetInstructorActivityResponse objResponse = new GetInstructorActivityResponse();
            try
            {
                objResponse.m_lsActivityDetails = objHomeService.GetInstructorActivityDetails(id);
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
        public APIDefaultResponse ActivateAssignment(long AssignmentId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.ActivateAssignment(AssignmentId))
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
        public APIDefaultResponse DeleteAssignment(long AssignmentId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.DeleteAssignment(AssignmentId))
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
        public APIDefaultResponse DeleteIndependentAssignment(long AssignmentId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.DeleteIndependentAssignment(AssignmentId))
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
        public APIDefaultResponse DeleteIndependentTest(long TestId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.DeleteIndependentTest(TestId))
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
        public APIDefaultResponse ActivateTest(long testid)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.ActivateTest(testid))
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
        public APIDefaultResponse DeleteTest(long TestId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.DeleteTest(TestId))
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
        public APIDefaultResponse DeleteTestOfCourse(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.DeleteTestOfCourse(id))
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
        public APIDefaultResponse DeleteMcqAssignmentQuestion(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.DeleteMcqAssignmentQuestion(id))
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
        public APIDefaultResponse UpdateMcqAssignmentQuestion([FromBody]McqQuestion McqQuestion)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.UpdateMcqAssignmentQuestion(McqQuestion))
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
        public APIDefaultResponse AddMcqQuestion([FromBody]McqQuestion McqQuestion)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.InsertNewMcqQuestion(McqQuestion))
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
        public APIDefaultResponse AddMcqQuestionToAssignment([FromBody]McqQuestion McqQuestion)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.InsertNewMcqAssignmentQuestion(McqQuestion))
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
        public APIDefaultResponse ContactUs([FromBody] ContactUsApiRequest objContactUsApiRequest)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objContactUsApiRequest!=null&&objHomeService.InserContatUsRequest(objContactUsApiRequest))
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
        public APIDefaultResponse DeleteIndexTopic(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if ( objHomeService.DeleteIndexTopic(id))
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
        //public APIDefaultResponse DeleteCourseIndex(long id)
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
        public APIDefaultResponse UpdateIndexTopic([FromBody] TopicModel objTopicModel)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objTopicModel != null && objHomeService.UpdateIndexTopic(objTopicModel))
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
        public APIDefaultResponse UpdateIndex([FromBody] IndexModel objIndexModel)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objIndexModel != null && objHomeService.UpdateCourseIndex(objIndexModel))
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
        public APIDefaultResponse DeleteIndex(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.DeleteIndex(id))
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
        public APIDefaultResponse UpdateCourse(CourseDetailsModel objCourse)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objCourse!=null&&objHomeService.UpdateFullCourseDetails(objCourse))
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
        public APIDefaultResponse AddTopicToIndex(TopicModel objTopic)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objTopic != null && objHomeService.InsertNewTopic(objTopic))
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
        public APIDefaultResponse UpdateTestDetails(TestDetailsModel objTestDetails)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objTestDetails != null && objHomeService.UpdateTestDetails(objTestDetails))
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
        public APIDefaultResponse AddNewMcqTestQuestion(McqQuestion objTestModel)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objTestModel != null && objHomeService.InsertNewMcqQuestion(objTestModel))
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
        public APIDefaultResponse UpdateMcqQuestionTestDetails(McqQuestion objTestModel)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objTestModel != null && objHomeService.UpdateMcqTestQuestion(objTestModel))
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
        public APIDefaultResponse AddNewAssignment(AssignmentModel objAssignmentModel)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objAssignmentModel != null && objHomeService.InsertNewSeperateAssignmentToCourse(objAssignmentModel))
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
        public APIDefaultResponse DeleteMcqTestQuestion(long id)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.DeleteMcqTestQuestion(id))
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
        public APIDefaultResponse InsertNewTestToCourse(TestModel objTestModel)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.InsertNewTestToCourse(objTestModel))
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
        public APIDefaultResponse UpdateAssignmentDetails(AssignmentModel objAssignmentModel)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.UpdateAssignmentDetails(objAssignmentModel))
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
        public APIDefaultResponse AddSubjectiveAssignmentQuestion(SubjectiveQuestion objSubjectiveQuestion)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.InsertSubjectiveAssignmentQuestion(objSubjectiveQuestion))
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
        public APIDefaultResponse UpdateSubjectiveAssignmentQuestion(SubjectiveQuestion objSubjectiveQuestion)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.UpdateSubjectiveAssignmentQuestion(objSubjectiveQuestion))
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
        public APIDefaultResponse DeleteSubjectiveAssignmentOfCourse(long AssignmentId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.DeleteSubjectiveAssignmentOfCourse(AssignmentId))
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
        public APIDefaultResponse DeleteSubjectiveAssignmentQuestion(long QuestionId)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (objHomeService.DeleteSubjectiveAssignmentQuestion(QuestionId))
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
        public AssignmentSubmissionResponse GetAllAssignmentsSubmission(long AssignmentId)
        {
            AssignmentSubmissionResponse objResponse = new AssignmentSubmissionResponse();
            try
            {
                objResponse.m_lsAssignmentSubmissionResponseModal = objHomeService.GetAllSubmissionsOfAnAssignment(AssignmentId);
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
        public AssignmentSubmissionResponse GetAllTestSubmissions(long id)
        {
            AssignmentSubmissionResponse objResponse = new AssignmentSubmissionResponse();
            try
            {
                objResponse.m_lsAssignmentSubmissionResponseModal = objHomeService.GetAllTestSubmissions(id);
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
        public CourseJoinedResponse GetAllStudentsJoinedToInstructs(int id)
        {
            CourseJoinedResponse objResponse = new CourseJoinedResponse();
            try
            {
                objResponse.m_lsStudentsJoined = objHomeService.GetAllStudentsJoinedToInstructor(id);
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
        public GetAllAlertForInstructorResponse GetAllAlertForInstructor(int id)
        {
            GetAllAlertForInstructorResponse objResponse = new GetAllAlertForInstructorResponse();
            try
            {
                objResponse.m_lsInstructorAlertModal = objHomeService.GetAllAlertOfInstructor(id);
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
        public InstructorSearchResponse GetInstructorSerachResponse(InstructorSearchRequest objInstructorSearchRequest)
        {
            InstructorSearchResponse objResponse = new InstructorSearchResponse();
            try
            {
               
                if (objInstructorSearchRequest != null)
                {
                    objResponse = objHomeService.GetInstructorSearchDetails(objInstructorSearchRequest);
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