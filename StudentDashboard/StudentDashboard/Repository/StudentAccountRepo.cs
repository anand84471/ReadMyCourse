using StudentDashboard.HttpRequest.Student;
using StudentDashboard.Models.Session;
using StudentDashboard.Models.Student;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.Repository
{
    public class StudentAccountRepo
    {
        CPDataService.CpDataServiceClient objCPDataService;
        StringBuilder m_strLogMessage = new StringBuilder();
        public StudentAccountRepo()
        {
            objCPDataService = new CPDataService.CpDataServiceClient();
        }
        public async Task<bool> RegisterNewStudentAsync(StudentRegisterRequest student)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.RegisterNewStudentAsync(student.FirstName, student.LastName,
                               student.EmailId, student.Password, student.PhoneNo,
                               "", "");
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
        public async Task< long> ValidateLoginAsync(string EmailId,string HashedPassword)
        {
            long StudentId = -1;
            try
            {
                await Task.Run(()=> objCPDataService.ValidateStudentLogin(EmailId,
                    HashedPassword, ref StudentId));

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ValidateLoginAsync", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return StudentId;
        }
        public async Task<bool> RegisterWithGoogleAsync(StudentRegisterGoogleRequest student)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.RegisterNewStudentViaGmailAsync(
                   student.GoogleId, student.FirstName, student.LastName,
                               student.EmailId, student.PhoneNo,
                               "", student.ImageUrlSmall);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterWithGoogle", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<long> ValidateGoogleLoginAsync(string GoogleId,string EmailId)
        {
            long StudentId = -1;
            try
            {
                DataSet ds = await objCPDataService.CheckGmailUserAlreadyExistsAsync(GoogleId,
                    EmailId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    StudentId = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentRegisterModal(
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<bool>("IS_PHONE_NO_VERIFIED"),
                         dataRow.Field<string>("PROFILE_URL"),
                         dataRow.Field<string>("PHONE_NO"),
                         dataRow.Field<string>("PHONE_NO_VERIFICATION_LINK_GUID")
                         )).ToList()[0].m_llStudentId;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return StudentId;
        }
        public async Task<StudentRegisterModal> GetStudentDetailsByEmailId(string EmailId)
        {
            StudentRegisterModal studentInfo = null;
            try
            {
                DataSet ds = await objCPDataService.GetStudentDetailsByEmailIdAsync(EmailId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    studentInfo = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentRegisterModal()
                     {
                         m_llStudentId = dataRow.Field<long>("STUDENT_ID"),
                         m_strPhoneNo = dataRow.Field<string>("PHONE_NO")
                     }
                         ).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetStudentDetailsByEmailId", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return studentInfo;
        }
        public async Task<bool> InsertPasswordRecovery(string UserId, string Token, string OTP)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertStudentPasswordRecoveryRequestAsync(UserId, Token, OTP);

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
        public async Task<bool> MarkOtpVerifiedForPasswordRecodevry(string UserId, string Token)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.MarkOtpVarifiedAsync(UserId, Token);

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
        public async Task<bool> ValidatePasswordRecodevrtOtp(string UserId, string Token, string OTP)
        {
            bool result = false;
            DateTime? TokenExpiryTime = null;
            try
            {
                DataSet ds = await objCPDataService.ValidateStudentPasswordRecoveryRequestAsync(UserId, Token, OTP);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    TokenExpiryTime = ds.Tables[0].AsEnumerable().Select(
                     dataRow => (dataRow.Field<DateTime?>("LAST_PASSWORD_RECOVERY_REQUEST_TIME"))).ToList()[0];
                }
                if (TokenExpiryTime != null && DateTime.Now - TokenExpiryTime > TimeSpan.FromSeconds(MvcApplication._forgotPasswordExpiryTimeInMinutes))
                {
                    result = true;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ValidatePasswordRecodevrtOtp", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateStudentPasswordAsync(string UserId, string Token, string HashedPasword)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.ChanegPasswordAfterAuthenticationAsync(UserId, Token, HashedPasword);

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
       

    }
}