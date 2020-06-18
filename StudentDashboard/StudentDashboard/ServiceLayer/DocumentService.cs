using StudentDashboard.DTO;

using StudentDashboard.Utilities;
using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.ServiceLayer
{
    public class DocumentService
    {
        StringBuilder m_strLogMessage;
        DocumentDTO objDocumentDTO;
        public DocumentService()
        {
            m_strLogMessage = new StringBuilder();
            objDocumentDTO = new DocumentDTO();
        }

        //public async Task<TestModalForAnonymousAccess> GetTestDetails(long TestId, string TestAccessCode)
        //{
        //    TestModalForAnonymousAccess objTestModel = null;
        //    try
        //    {
        //        objTestModel = await objDocumentDTO.GetTestDetails(TestId, TestAccessCode);
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
        public async Task<bool> CheckTestAccess(long TestId, string AccessCode)
        {
            return await objDocumentDTO.CheckTestAccess(TestId, AccessCode);
        }
        public async Task<bool> CheckAssignmentAccess(long AssignmentId, string AccessCode)
        {
            return await objDocumentDTO.CheckAssignmentAccess(AssignmentId, AccessCode);
        }
        //public async Task<AssignmentModalForAnonymousAccess> GetAssignmentDetails(long TestId, string TestAccessCode)
        //{
        //    AssignmentModalForAnonymousAccess objAssignmentModal = null;
        //    try
        //    {
        //        objAssignmentModal = await objDocumentDTO.GetAssignmentDetails(TestId, TestAccessCode);
        //    }
        //    catch (Exception Ex)
        //    {
        //        m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
        //        m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTestDetails", Ex.ToString());
        //        m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
        //        MainLogger.Error(m_strLogMessage);
        //    }
        //    return objAssignmentModal;
        //}
    }
}