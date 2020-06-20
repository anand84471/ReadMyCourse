using StudentDashboard.HttpResponse;
using StudentDashboard.Models.Course;
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
    [AllowAnonymous]
    [RoutePrefix("api/v1/document")]
    public class DocumentApiController : ApiController
    {
        StringBuilder m_strLogMessage = new StringBuilder();
        DocumentService objDocumentService = new DocumentService();
        HomeService objHomeService=new HomeService();
        [Route("FetchFullTestDetails")]
        [HttpPost]
        public async Task<TestModel> GetFullTestDetails(long id,string AccessCode)
        {
            TestModel objResponse = null;
            try
            {
                if (await objDocumentService.CheckTestAccess(id, AccessCode))
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
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("FetchTestDetails")]
        [HttpPost]
        public async Task<GetTestDetailsResponseWithAccessCode> GetTestDetails(long id, string AccessCode)
        {
            GetTestDetailsResponseWithAccessCode objResponse = null;
            try
            {
                objResponse = await objDocumentService.GetTestDetails(id, AccessCode);
                if (objResponse != null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse = new GetTestDetailsResponseWithAccessCode();
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("FetchFullAssignmentDetails")]
        [HttpPost]
        public async Task<AssignmentModel> GetFullAssignmentDetails(long AssignmentId)
        {
            AssignmentModel objResponse = null;
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
            catch (Exception Ex)
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
        public async Task<AssignmentModel> GetAssignmentDetails(long AssignmentId, string AccessCode)
        {
            AssignmentModel objResponse = null;
            try
            {
                if (await objDocumentService.CheckAssignmentAccess(AssignmentId, AccessCode))
                {
                    objResponse = await objHomeService.GetIndependentAssignmentDetailsWithoutQuestion(AssignmentId);
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
    }
}
