using StudentDashboard.Models.Session;
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
    public class StudentSessionRepo
    {
        CPDataService.CpDataServiceClient objCPDataService;
        StringBuilder m_strLogMessage = new StringBuilder();
        public StudentSessionRepo()
        {
            objCPDataService = new CPDataService.CpDataServiceClient();
        }
        public async Task<StudentSession> GetStudentSessionDetailsAsync(string Token)
        {
            StudentSession sessionInfo = null;
            try
            {
                DataSet ds = await objCPDataService.GetStudentSessionDetailsAsync(Token);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    sessionInfo = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentSession(
                         Token,
                         dataRow.Field<DateTime>("SESSION_EXPIRY_TIME"),
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<bool>("IS_LOGOUT")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return sessionInfo;
        }
        public async Task<bool> InsertStudentSessionAsync(StudentSession studentSession)
        {
            bool result = false;
            try
            {
                result= await objCPDataService.InsertStudentSessionAsync(studentSession.StudentId
                    , studentSession.Token, studentSession.ExpiryTime);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertStudentSession", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> LogoutSessionAsync(StudentSession session)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.LogoutStudentSessionAsync(session.Token);

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