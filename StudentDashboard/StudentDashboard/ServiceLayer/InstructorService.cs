using Newtonsoft.Json;
using StudentDashboard.BusinessLayer;
using StudentDashboard.DTO;
using StudentDashboard.HttpRequest;
using StudentDashboard.Models;
using StudentDashboard.Models.Classroom;
using StudentDashboard.Models.Course;
using StudentDashboard.Models.Instructor;
using StudentDashboard.Models.Student;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.ServiceLayer
{
    public class InstructorService
    {
        InstructorDTO objInstructorDTO;
        DocumentService objDocumentService;
        InstructorBusinessLayer objInstructorBusinessLayer;
        SMSServiceManager objSMSServiceManager;
        StringBuilder m_strLogMessage;
        public InstructorService()
        {
            objInstructorDTO = new InstructorDTO();
            objDocumentService = new DocumentService();
            objInstructorBusinessLayer = new InstructorBusinessLayer();
            objSMSServiceManager = new SMSServiceManager();
            m_strLogMessage = new StringBuilder();
        }
        public async Task<bool> RegisterNewUser(InstructorRegisterModel objInstructorRegisterModel)
        {
            bool result = false;
            try
            {
                string EncryptedPassword = SHA256Encryption.ComputeSha256Hash(objInstructorRegisterModel.m_strPassword);
                objInstructorRegisterModel.m_strPassword = EncryptedPassword;
                objInstructorRegisterModel.m_strPhoneNoVarificationGuid = objInstructorBusinessLayer.GetSmsVerificationString();
                objInstructorRegisterModel.m_strEmailVarificationGuid = objInstructorBusinessLayer.GetEmailVerficationString();
                if(await objInstructorDTO.RegisterNewInstructor(objInstructorRegisterModel))
                {
                    result = true;
                    var SmsVarificationLink =objInstructorBusinessLayer.GetLinkForSmsVarification(objInstructorRegisterModel.m_strEmailVarificationGuid,
                        objInstructorRegisterModel.m_strEmail,Constants.SMS_VERIFICATION_LINK_TYPE_ID_FOR_INSTRUCTOR);
                    await objSMSServiceManager.SendInstructorPhoneNoVarification(SmsVarificationLink, "+91"+objInstructorRegisterModel.m_strPhoneNo);
                }

            }
            catch (Exception Ex)
            {

            }
            return result;

        }
        public async Task<bool> ValidateLoginDetails(InstructorRegisterModel objInstructorRegisterModel)
        {
            bool result = false;
            try
            {
                objInstructorRegisterModel.m_strPassword = SHA256Encryption.ComputeSha256Hash(objInstructorRegisterModel.m_strPassword);
                result = await objInstructorDTO.ValidateInstructorLoginDetails(objInstructorRegisterModel);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ValidateLoginDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;

        }
        public async Task<InstructorRegisterModel> GetInstructorPostLoginDetails(int Id)
        {
           return await objInstructorDTO.GetInstructorPostLoginDetails(Id);
        }
        public async Task<InstructorRegisterModel> GetInstructorDetails(int InstructorId)
        {
            return await objInstructorDTO.GetInstructorDetails(InstructorId);
        }
        public async Task<bool> UpdateInstructorDetails(InstructorRegisterModel objInstructorRegisterModel)
        {
            return await objInstructorDTO.UpdateInstructorDetails(objInstructorRegisterModel);
        }
        public async Task<string> InsertPasswordRecovery(string InstrcutorUserId)
        {
            string AuthToken = null;
            try
            {
                var InstrcutorId = objInstructorDTO.GetInstructorIdFromUserId(InstrcutorUserId);
                if (InstrcutorId != -1)
                {
                    InstructorRegisterModel objStudentRegisterModal = await objInstructorDTO.GetInstructorDetails(InstrcutorId);
                    if (objStudentRegisterModal != null)
                    {
                        string OTP = objInstructorBusinessLayer.GenerateOtp();
                        AuthToken = objInstructorBusinessLayer.GeneratePasswordVeryficationToken();
                        await objSMSServiceManager.SendInstructorPasswordRecoveryOTP(OTP, "+91" + objStudentRegisterModal.m_strPhoneNo);
                        await objInstructorDTO.InsertPasswordRecoveryForInstructor(InstrcutorUserId, AuthToken, OTP);
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertPasswordRecovery", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return AuthToken;
        }
        public async Task<bool> ValidatePasswordRecodevrtOtp(StudentUpdatePasswordRequestModal objStudentUpdatePasswordRequestModal)
        {

            bool result = false;
            try
            {
                if (await objInstructorDTO.ValidatePasswordRecoveryOtpForInstructor(objStudentUpdatePasswordRequestModal.m_strUserName,
                    objStudentUpdatePasswordRequestModal.m_strToken, objStudentUpdatePasswordRequestModal.m_strOtp))
                {
                    result = await objInstructorDTO.MarkOtpVerifiedForPasswordRecoveryForInstructor(objStudentUpdatePasswordRequestModal.m_strUserName,
                        objStudentUpdatePasswordRequestModal.m_strToken);
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> ChangePasswordAfterAuth(StudentUpdatePasswordRequestModal objStudentUpdatePasswordRequestModal)
        {

            bool result = false;
            try
            {
                if (objStudentUpdatePasswordRequestModal.m_strPassword.Equals(objStudentUpdatePasswordRequestModal.m_strMatchPassword))
                {
                    objStudentUpdatePasswordRequestModal.m_strHashedPassword = SHA256Encryption.ComputeSha256Hash(objStudentUpdatePasswordRequestModal.m_strPassword);
                    result = await objInstructorDTO.UpdateInstructorPasswordAfterAuth(objStudentUpdatePasswordRequestModal.m_strUserName,
                        objStudentUpdatePasswordRequestModal.m_strToken, objStudentUpdatePasswordRequestModal.m_strHashedPassword);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ChangePasswordAfterAuth", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InertNewMeetingToClassroom(JitsiMeetingModal objJitsiMeetingModal)
        {

            bool result = false;
            try
            {
                objJitsiMeetingModal.m_strMeetingName = objInstructorBusinessLayer.GetRandomMeetingName();
                objJitsiMeetingModal.m_strMeetingPassword = objInstructorBusinessLayer.GetRandomMeetingPassword();
                objJitsiMeetingModal.m_strMeetingPassword = "";
                result = await objInstructorDTO.InertNewMeetingToClassroom(objJitsiMeetingModal);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ChangePasswordAfterAuth", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }

        public async Task<ClassRoomModal> GetClassroomDetailsForInstructor(long ClassroomId, int InstructorId)
        {
            ClassRoomModal objClassRoomModal = null;
            try
            {
                objClassRoomModal = await objInstructorDTO.GetClassroomDetailsForInstructor(ClassroomId, InstructorId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ChangePasswordAfterAuth", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objClassRoomModal;
        }
        public async Task<JitsiMeetingModal> GetClassroomMeetingDetails(long ClassroomId, int InstructorId)
        {

            JitsiMeetingModal objJitsiMeetingModal = null;
            try
            {
                objJitsiMeetingModal = await objInstructorDTO.GetClassroomMeetingDetails(ClassroomId,-1);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ChangePasswordAfterAuth", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objJitsiMeetingModal;
        }
        public async Task<bool> CheckClassroomAccess(long ClassroomId, int InstructorId)
        {
            return await objInstructorDTO.CheckClassroomAccess(ClassroomId, InstructorId);
        }
        public async Task<bool> DeleteClassroomAssignment(long ClassroomID, long AssignmentId)
        {
            return await objInstructorDTO.DeleteClassroomAssignment(ClassroomID, AssignmentId);
        }
        public async Task<InstructorRegisterModel> GetInstructorBasicDetails(int Id)
        {
            return await objInstructorDTO.GetInstructorBasicDetails(Id);
        }
        public async Task<bool> UpdateInstructorAcademicRecords(InstructorAcademicRecordUpdateRequest
            instructorAcademicRecordUpdateRequest)
        {
            InstructorAcademicRecordDTO instructorAcademicRecordDTO = new InstructorAcademicRecordDTO();
            instructorAcademicRecordDTO.m_iInstructorId = instructorAcademicRecordUpdateRequest.m_iInstructorId;
            instructorAcademicRecordDTO.m_strLinkedIn = instructorAcademicRecordUpdateRequest.m_strLinkedIn;
            instructorAcademicRecordDTO.m_strGoogleScholarId = instructorAcademicRecordUpdateRequest.m_strGoogleScholarId;
            instructorAcademicRecordDTO.m_strLatestQualification = instructorAcademicRecordUpdateRequest.m_strLatestQualification;
            instructorAcademicRecordDTO.m_strConferencesAttends = JsonConvert.SerializeObject(instructorAcademicRecordUpdateRequest.m_lsInstructorConferencesAttendsModal);
            instructorAcademicRecordDTO.m_strProjectsDone = JsonConvert.SerializeObject(instructorAcademicRecordUpdateRequest.m_lsInstructorProjectsDetailsModal);
            instructorAcademicRecordDTO.m_strAcademicPublications = JsonConvert.SerializeObject(instructorAcademicRecordUpdateRequest.m_lsInstructorAcadmeicsPublicationModal);
            instructorAcademicRecordDTO.m_strCertificates = JsonConvert.SerializeObject(instructorAcademicRecordUpdateRequest.m_lsInstructorCertificateModal);
            return await objInstructorDTO.UpdateInstructorAcademicRecord(instructorAcademicRecordDTO);
        }
    }
}