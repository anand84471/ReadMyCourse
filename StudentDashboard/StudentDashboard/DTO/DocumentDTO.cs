using StudentDashboard.Models.Course;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.DTO
{
    public class DocumentDTO
    {
        CPDataService.CpDataServiceClient objCPDataService;
        StringBuilder m_strLogMessage = new StringBuilder();
        public DocumentDTO()
        {
            objCPDataService = new CPDataService.CpDataServiceClient();
        }
        //public async Task<TestModalForAnonymousAccess> GetTestDetails(long TestId,string AccessCode)
        //{
        //    TestModalForAnonymousAccess objTestModel = null;
        //    try { 
        //        DataSet ds = await objCPDataService.GetTestDetailsWithAccessCodeAsync(TestId, AccessCode);
        //        if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            objTestModel = ds.Tables[0].AsEnumerable().Select(
        //             dataRow => new TestModalForAnonymousAccess(
        //                 dataRow.Field<long>("TEST_ID"),
        //                 dataRow.Field<string>("TEST_NAME"),
        //                 dataRow.Field<string>("TEST_DESCRIPTION"),
        //                 dataRow.Field<DateTime>("TEST_ACTIVATION_DATETIME").ToString("d MMM yyyy"),
        //                 dataRow.Field<byte>("TEST_TYPE"),
        //                 dataRow.Field<String>("INSTRUCTOR_NAME"),
        //                 dataRow.Field<int>("TOTAL_ALLOWED_TIME"),
        //                 dataRow.Field<int>("TOTAL_MARKS"),
        //                 dataRow.Field<int>("TOTAL_NO_OF_QUESTIONS"),
        //                 dataRow.Field<int>("NO_OF_SUBMISSIONS"),
        //                 dataRow.Field<DateTime?>("TEST_START_TIME")
        //                 )).ToList()[0];
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
        //        m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTestDetails", Ex.ToString());
        //        m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
        //        MainLogger.Error(m_strLogMessage);
        //    }
        //    return objTestModel;
        //}
        //public async Task<AssignmentModalForAnonymousAccess> GetAssignmentDetails(long AssignmentId,string AccessCode)
        //{
        //    AssignmentModalForAnonymousAccess objAssignmentModel = null;
        //    try
        //    {
        //        DataSet ds = await objCPDataService.GetAssignmentDetailsWithAccessCodeAsync(AssignmentId,AccessCode);
        //        if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            objAssignmentModel = ds.Tables[0].AsEnumerable().Select(
        //             dataRow => new AssignmentModalForAnonymousAccess(
        //                 dataRow.Field<long>("ASSIGNMENT_ID"),
        //                 dataRow.Field<string>("ASSIGNMENT_NAME"),
        //                 dataRow.Field<string>("ASSIGNMENT_DESCRIPTION"),
        //                 dataRow.Field<DateTime>("ASSIGNMENT_ACTIVATION_DATETIME").ToString("d MMM yyyy"),
        //                 dataRow.Field<byte>("ASSIGNMENT_TYPE"),
        //                 dataRow.Field<>("INSTRUCTOR_NAME"),
        //                 dataRow.Field<int>("NO_OF_QUESTIONS"),
        //                 dataRow.Field<int>("NO_OF_SUBMISSIONS")
        //                 )).ToList()[0];
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
        //        m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAssignmentDetails", Ex.ToString());
        //        m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
        //        MainLogger.Error(m_strLogMessage);
        //    }
        //    return objAssignmentModel;
        //}
        public async Task<bool> CheckTestAccess(long TestId, string AccessCode)
        {
            bool result = false;
            try
            {
                DataSet ds = await objCPDataService.CheckTestAccessAsync(TestId, AccessCode);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckTestAccess", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckAssignmentAccess(long AssignmentId, string AccessCode)
        {
            bool result = false;
            try
            {
                DataSet ds = await objCPDataService.CheckAssignmentAccessAsync(AssignmentId, AccessCode);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckAssignmentAccess", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
    }
}