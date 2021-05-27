using StudentDashboard.HttpRequest.Student;
using StudentDashboard.HttpResponse;
using StudentDashboard.HttpResponse.Master;
using StudentDashboard.HttpResponse.Student;
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
using System.Web.Http.Cors;

namespace StudentDashboard.API
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [AllowAnonymous]
    [RoutePrefix("api/v1/student")]
    public class StudentApiController : ApiController
    {
        StudentAccountService _service;
        StringBuilder m_strLogMessage;
        public StudentApiController()
        {
            m_strLogMessage = new StringBuilder();
            _service = new StudentAccountService();
        }
        [HttpGet]
        public string Index()
        {
            return "Passed";
        }
        [Route("register")]
        [HttpPost]
        public async Task<MasterResponse<StudentRegisterResponse>> Register(StudentRegisterRequest student)
        {
            MasterResponse < StudentRegisterResponse > response = new MasterResponse<StudentRegisterResponse>();
            try
            {
                response.data = await _service.RegisterNewStudentAsync(student);
               if (response.data != null)
                {
                    response.SetSuccessReponse();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "Register", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("register/fb")]
        [HttpPost]
        public async Task<MasterResponse<StudentRegisterResponse>> RegisterFb(StudentRegisterRequest student)
        {
            MasterResponse<StudentRegisterResponse> response = new MasterResponse<StudentRegisterResponse>();
            try
            {
                response.data = await _service.RegisterNewStudentAsync(student);
                if (response.data != null)
                {
                    response.SetSuccessReponse();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("register/google")]
        [HttpPost]
        public async Task<MasterResponse<StudentRegisterResponse>> RegisterGoogle(StudentRegisterGoogleRequest student)
        {
            MasterResponse<StudentRegisterResponse> response = new MasterResponse<StudentRegisterResponse>();
            try
            {
                response.data = await _service.RegisterNewStudentWithGoogleAsync(student);
                if (response.data != null)
                {
                    response.SetSuccessReponse();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("login")]
        [HttpPost]
        public async Task<MasterResponse<StudentRegisterResponse>> Login(StudentLoginRequest student)
        {
            MasterResponse<StudentRegisterResponse> response = new MasterResponse<StudentRegisterResponse>();
            try
            {
                response.data = await _service.LoginAsync(student);
                if (response.data != null)
                {
                    response.SetSuccessReponse();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "Register", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("login/google")]
        [HttpPost]
        public async Task<MasterResponse<StudentRegisterResponse>> LoginGoogle(StudentLoginGoogleRequest student)
        {
            MasterResponse<StudentRegisterResponse> response = new MasterResponse<StudentRegisterResponse>();
            try
            {
                response = await _service.LoginGoogleAsync(student);
                if (response.data != null)
                {
                    response.SetSuccessReponse();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LoginGoogle", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("account/reset/password/request")]
        [HttpPost]
        public async Task<MasterResponse<PasswordResetResponse>> ResetPasswordRequest(StudentForgotPasswordRequest student)
        {
            MasterResponse<PasswordResetResponse> response = new MasterResponse<PasswordResetResponse>();
            try
            {
                response = await _service.GetForgotPasswordTokenAsync(student);
                if (response.data != null)
                {
                    response.SetSuccessReponse();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LoginGoogle", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("account/reset/password/validate")]
        [HttpPost]
        public async Task<MasterResponseBase> 
            ResetPasswordValidate(StudentForgotPasswordValidateRequest request)
        {
            MasterResponseBase response = new MasterResponseBase();
            try
            {
                response = await _service.ValidatePasswordUpdateAsync(request);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LoginGoogle", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }
        [Route("account/reset/password/process")]
        [HttpPost]
        public async Task<MasterResponseBase>
             ResetPasswordProcess(StudentForgotPasswordProcessRequest request)
        {
            MasterResponseBase response = new MasterResponseBase();
            try
            {
                if (await _service.UpdateStudentPasswordAsync(request))
                {
                    response.SetSuccessReponse();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LoginGoogle", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return response;
        }

    }
}
