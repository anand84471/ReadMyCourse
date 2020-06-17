using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.BusinessLayer
{
    public class InstructorBusinessLayer
    {
        StringBuilder m_strLogMessage;
        TinyUrlService objTinyUrlService;
        public InstructorBusinessLayer()
        {
            objTinyUrlService = new TinyUrlService();
            m_strLogMessage = new StringBuilder();
        }
        public async Task<string> GetTinyUrlForAssignment(long id,string AccessCode)
        {
            string result = null;
            try
            {
                string path = Constants.BASE_URL_PATH_FOR_ASSIGNMENT + id + "&access_code=" + AccessCode;
                result = await objTinyUrlService.GetTinyUrl(path);
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTinyUrlForAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<string> GetTinyUrlForTest(long id, string AccessCode)
        {
            string result = null;
            try
            {
                string path = Constants.BASE_URL_PATH_FOR_TEST + id + "&access_code=" + AccessCode;
                result = await objTinyUrlService.GetTinyUrl(path);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTinyUrlForTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public string GetShareCodeForAssignment()
        {
            string result = null;
            try
            {
                return RandomAccessCode();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTinyUrlForAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string RandomAccessCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
    }
   
}