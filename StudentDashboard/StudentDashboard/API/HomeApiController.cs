using StudentDashboard.HttpResponse;
using StudentDashboard.Models.Base;
using StudentDashboard.Models.Category;
using StudentDashboard.Models.Classroom;
using StudentDashboard.Models.Instructor;
using StudentDashboard.Models.Review;
using StudentDashboard.Models.Search;
using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StudentDashboard.API
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [AllowAnonymous]
    [RoutePrefix("api/v1/home")]
    public class HomeApiController : ApiController
    {
        StringBuilder m_strLogMessage = new StringBuilder();
        HomeApiService _service;
        public HomeApiController()
        {
            _service = new HomeApiService();
        }
        [Route("GetClassroomCategories")]
        [HttpPost]
        public async Task<MasterApiResponse<List<CategoryModel>>> GetCategories()
        {
            MasterApiResponse<List<CategoryModel>> response = new MasterApiResponse<List<CategoryModel>>();
            try
            {
                response.data = await _service.FetchAllClassroomCategories();
                if (response.data!=null)
                {
                    response.SetSuccessResponse();
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCategories", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("GetClassroomsForCategories")]
        [HttpPost]
        public async Task<MasterApiResponse<List<ClassroomShowcaseDetails>>> GetClassroomsForCategories(SearchRequest<int> request)
        {
            MasterApiResponse<List<ClassroomShowcaseDetails>> response = new MasterApiResponse<List<ClassroomShowcaseDetails>>();
            try
            {
                response.data = await _service.GetAllClassroomForCategoriesAsync(request);
                if (response.data != null)
                {
                    response.SetSuccessResponse();
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetClassroomsForCategories", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("GetPopularClassrooms")]
        [HttpPost]
        public async Task<MasterApiResponse<List<ClassroomShowcaseDetails>>> GetPopularCourses(SearchBaseRequest request)
        {
            MasterApiResponse<List<ClassroomShowcaseDetails>> response = new MasterApiResponse<List<ClassroomShowcaseDetails>>();
            try
            {
                response.data = await _service.GetPopularClassroomsAsync(request);
                if (response.data != null)
                {
                    response.SetSuccessResponse();
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetClassroomsForCategories", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("GetTrendingClassrooms")]
        [HttpPost]
        public async Task<MasterApiResponse<List<ClassroomShowcaseDetails>>> GetTrendingClassrooms(SearchBaseRequest request)
        {
            MasterApiResponse<List<ClassroomShowcaseDetails>> response = new MasterApiResponse<List<ClassroomShowcaseDetails>>();
            try
            {
                response.data = await _service.GetTrendingClassroomsAsync(request);
                if (response.data != null)
                {
                    response.SetSuccessResponse();
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetClassroomsForCategories", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("GetInstructorPublicDetails")]
        [HttpPost]
        public async Task<MasterApiResponse<InstructorPublicProfileDetails>> GetInstructorPublicDetails(int InstructorId)
        {
            MasterApiResponse<InstructorPublicProfileDetails> response =
                new MasterApiResponse<InstructorPublicProfileDetails>();
            try
            {
                response.data = await _service.GetInstructorPublicDetailsAsync(InstructorId);
                if (response.data != null)
                {
                    response.SetSuccessResponse();
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorPublicDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("GetInstructorClassrooms")]
        [HttpPost]
        public async Task<MasterApiResponse<List<ClassroomShowcaseDetails>>> GetInstructorClassrooms(SearchRequest<int> request)
        {
            MasterApiResponse<List<ClassroomShowcaseDetails>> response =
                new MasterApiResponse<List<ClassroomShowcaseDetails>>();
            try
            {
                response.data = await _service.GetAllClassroomForInstructorAsync(request);
                if (response.data != null)
                {
                    response.SetSuccessResponse();
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorPublicDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("FetchNextInstructorReviews")]
        [HttpPost]
        public async Task<MasterApiResponse<List<ReviewModel>>> GetInstructorReviews(SearchRequest<int> request)
        {
            MasterApiResponse<List<ReviewModel>> response =
                new MasterApiResponse<List<ReviewModel>>();
            try
            {
                response.data = await _service.GetAllReviewsForInstructorAsync(request);
                if (response.data != null)
                {
                    response.SetSuccessResponse();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorPublicDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("GetClassroomDetails")]
        [HttpPost]
        public async Task<MasterApiResponse<ClassroomPublicDetails>> GetClassroomDetailsAsync(long ClassroomId)
        {
            MasterApiResponse<ClassroomPublicDetails> response =
                new MasterApiResponse<ClassroomPublicDetails>();
            try
            {
                response.data = await _service.GetClassroomPublicDetailsAsync(ClassroomId);
                if (response.data != null)
                {
                    response.SetSuccessResponse();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorPublicDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("GetClassroomReviews")]
        [HttpPost]
        public async Task<MasterApiResponse<ReviewsDetails>> GetClassroomReviewsAsync(long ClassroomId)
        {
            MasterApiResponse<ReviewsDetails> response =
                new MasterApiResponse<ReviewsDetails>();
            try
            {
                response.data = await _service.GetClassroomReviewAsync(ClassroomId);
                if (response.data != null)
                {
                    response.SetSuccessResponse();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorPublicDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("GetInstructorReviews")]
        [HttpPost]
        public async Task<MasterApiResponse<ReviewsDetails>> GetInstructorReviews(int InstructorId)
        {
            MasterApiResponse<ReviewsDetails> response =
                new MasterApiResponse<ReviewsDetails>();
            try
            {
                response.data = await _service.GetInstructorReviewAsync(InstructorId);
                if (response.data != null)
                {
                    response.SetSuccessResponse();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorPublicDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("FetchNextClassroomReviewsAsync")]
        [HttpPost]
        public async Task<MasterApiResponse<List<ReviewModel>>> FetchNextClassroomReviewsAsync(SearchRequest<long> request)
        {
            MasterApiResponse<List<ReviewModel>> response =
                new MasterApiResponse<List<ReviewModel>>();
            try
            {
                response.data = await _service.GetAllClassroomReviews(request);
                if (response.data != null)
                {
                    response.SetSuccessResponse();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorPublicDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
    }
}
