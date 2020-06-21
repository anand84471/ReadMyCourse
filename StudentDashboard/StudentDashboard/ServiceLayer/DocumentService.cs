using StudentDashboard.DTO;
using StudentDashboard.HttpResponse;
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

        public async Task<bool> CheckTestAccess(long TestId, string AccessCode)
        {
            return await objDocumentDTO.CheckTestAccess(TestId, AccessCode);
        }
        public async Task<bool> CheckAssignmentAccess(long AssignmentId, string AccessCode)
        {
            return await objDocumentDTO.CheckAssignmentAccess(AssignmentId, AccessCode);
        }
        //public async Task<GetAssignmentDetailsResponseForAnonymous> GetAssignmentDetailsWithAccessCode(long AssignmentId, string AccessCode)
        //{
        //    return await objDocumentDTO.GetAssignmentDetailsWithAccessCodes(AssignmentId, AccessCode);
        //}
        public async Task<GetTestDetailsResponseWithAccessCode> GetTestDetails(long TestId, string AccessCode)
        {
            return await objDocumentDTO.GetTestDetails(TestId, AccessCode);
        }
        public async Task<GetWebsiteHomeDetailsResponse> GetHomeDetails()
        {
            return await objDocumentDTO.GetHomeDetails();
        }
        public async Task<bool> InsertSMSNotification(string Message, string SmsReceiver, int SmsNotificationTypeId)
        {
            return await objDocumentDTO.InsertSMSNotification(Message, SmsReceiver, SmsNotificationTypeId);
        }


    }
}