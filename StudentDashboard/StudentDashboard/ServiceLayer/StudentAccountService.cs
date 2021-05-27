using StudentDashboard.AppConstants;
using StudentDashboard.BusinessLayer;
using StudentDashboard.HttpRequest.Student;
using StudentDashboard.HttpResponse.Master;
using StudentDashboard.HttpResponse.Student;
using StudentDashboard.Models.Session;
using StudentDashboard.Repository;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.ServiceLayer
{
    public class StudentAccountService
    {
        StudentAccountRepo _repo;
        StudentSessionService _sessionService;
        StudentBusinessLayer _businessLayer;
        SMSServiceManager _smsService;
        public StudentAccountService()
        {
            _repo = new StudentAccountRepo();
            _sessionService = new StudentSessionService();
            _smsService = new SMSServiceManager();
            _businessLayer = new StudentBusinessLayer();
        }
        public async Task<StudentRegisterResponse> RegisterNewStudentAsync(StudentRegisterRequest student)
        {
            StudentRegisterResponse response = null;
            student.Password= SHA256Encryption.ComputeSha256Hash(student.Password);
            if (await _repo.RegisterNewStudentAsync(student))
            {
                var StudentId = await _repo.ValidateLoginAsync(student.EmailId, student.Password);
                if (StudentId > 0)
                {
                    var token = await _sessionService.CreateSessionAsync(new StudentSession()
                    {
                        StudentId = StudentId,
                        EmailId = student.EmailId
                    });
                    if (token != null)
                    {
                        response = new StudentRegisterResponse();
                        response.EmailId = student.EmailId;
                        response.JwtToken = token;
                        response.FirstName = student.FirstName;
                    }
                }
            }
            return response;
        }
        public async Task<StudentRegisterResponse> RegisterNewStudentWithGoogleAsync(StudentRegisterGoogleRequest student)
        {
            StudentRegisterResponse response = null;
            if (await _repo.RegisterWithGoogleAsync(student))
            {
                var StudentId = await _repo.ValidateGoogleLoginAsync(student.GoogleId,student.EmailId);
                if (StudentId > 0)
                {
                    var token = await _sessionService.CreateSessionAsync(new StudentSession()
                    {
                        StudentId = StudentId,
                        EmailId = student.EmailId
                    });
                    if (token != null)
                    {
                        response = new StudentRegisterResponse();
                        response.EmailId = student.EmailId;
                        response.JwtToken = token;
                        response.FirstName = student.FirstName;
                    }
                }
            }
            return response;
        }
        public async Task<StudentRegisterResponse> LoginAsync(StudentLoginRequest student)
        {
            StudentRegisterResponse response = null;
            student.Password = SHA256Encryption.ComputeSha256Hash(student.Password);
            var StudentId = await _repo.ValidateLoginAsync(student.EmailId,student.Password);
            if (StudentId > 0)
            {
                var token = await _sessionService.CreateSessionAsync(new StudentSession()
                {
                    StudentId = StudentId,
                    EmailId = student.EmailId
                });
                if (token != null)
                {
                    response = new StudentRegisterResponse();
                    response.EmailId = student.EmailId;
                    response.JwtToken = token;
                }
            }
            return response;
        }
        public async Task<MasterResponse<StudentRegisterResponse>> LoginGoogleAsync(StudentLoginGoogleRequest student)
        {
            MasterResponse<StudentRegisterResponse> response = new MasterResponse<StudentRegisterResponse>();
            var StudentId = await _repo.ValidateGoogleLoginAsync(student.GoogleId, student.EmailId);
            if (StudentId > 0)
            {
                var token = await _sessionService.CreateSessionAsync(new StudentSession()
                {
                    StudentId = StudentId,
                    EmailId = student.EmailId
                });
                if (token != null)
                {
                    response.data = new StudentRegisterResponse();
                    response.data.EmailId = student.EmailId;
                    response.data.JwtToken = token;
                }
            }
            else
            {
                response.error = new MasterError();
                response.error.m_iErrorCode = ErrorConstants.GOOGLE_LOGIN_FAILED;
                response.error.m_strErrorMessage = ErrorConstants.GOOGLE_USER_NOT_EXISTS_MSG;

            }
            return response;
        }
        public async Task<MasterResponse<PasswordResetResponse>> GetForgotPasswordTokenAsync(StudentForgotPasswordRequest forgotPasswordRequest)
        {
            MasterResponse<PasswordResetResponse> response = new MasterResponse<PasswordResetResponse>();
            var studentInfo = await _repo.GetStudentDetailsByEmailId(forgotPasswordRequest.EmailId);
            if (studentInfo != null)
            {

                var token= _businessLayer.GeneratePasswordVeryficationToken();
                var otp = _businessLayer.GenerateOtp();
                if (await _repo.InsertPasswordRecovery(forgotPasswordRequest.EmailId, token, otp))
                {
                    response.data = new PasswordResetResponse();
                    response.data.PasswordRecoveryToken = token;
                    response.data.EmailId = forgotPasswordRequest.EmailId;
                    await _smsService.SendPhoneNoVarificationOtp(otp, studentInfo.m_strPhoneNo);
                }
            }
            else
            {
                response.error = new MasterError();
                response.error.m_iErrorCode = ErrorConstants.USER_ID_DOES_NOT_EXISTS;
                response.error.m_strErrorMessage = ErrorConstants.USER_ID_DOES_NOT_EXISTS_MSG;
            }
            return response;
        }
        public async Task<MasterResponseBase> ValidatePasswordUpdateAsync(StudentForgotPasswordValidateRequest request)
        {
            MasterResponseBase result = new MasterResponseBase();
            try
            {
                if (await _repo.ValidatePasswordRecodevrtOtp(request.EmailId,
                    request.PasswordRecoveryToken, request.PasswordRecoveryOtp))
                {
                    if(await _repo.MarkOtpVerifiedForPasswordRecodevry(request.EmailId,
                        request.PasswordRecoveryToken))
                    {
                        result.SetSuccessReponse();
                    }
                }
                else
                {
                    result.error = new MasterError();
                    result.error.m_iErrorCode = ErrorConstants.WRONG_OTP;
                    result.error.m_strErrorMessage = ErrorConstants.WRONG_OTP_MESSAGE;
                }

            }
            catch (Exception Ex)
            {
               
            }
            return result;
        }
        public async Task<bool> UpdateStudentPasswordAsync(StudentForgotPasswordProcessRequest request)
        {
            bool result = false;
            try
            {
                request.Password= SHA256Encryption.ComputeSha256Hash(request.Password);
                if (await _repo.UpdateStudentPasswordAsync(request.EmailId,
                    request.PasswordRecoveryToken, request.Password))
                {
                    result = await _repo.MarkOtpVerifiedForPasswordRecodevry(request.EmailId,
                        request.PasswordRecoveryToken);
                }

            }
            catch (Exception Ex)
            {

            }
            return result;
        }
        public async Task<bool> LogoutSessionAsync(StudentSession session)
        {
            bool result = false;
            try
            {
                result= await _sessionService.LogoutSessionAsync(session);
            }
            catch (Exception Ex)
            {

            }
            return result;
        }
    }
}