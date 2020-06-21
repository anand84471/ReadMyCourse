using StudentDashboard.BusinessLayer;
using StudentDashboard.DTO;
using StudentDashboard.Models;
using StudentDashboard.Models.Course;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public InstructorService()
        {
            objInstructorDTO = new InstructorDTO();
            objDocumentService = new DocumentService();
            objInstructorBusinessLayer = new InstructorBusinessLayer();
            objSMSServiceManager = new SMSServiceManager();

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

    }
}