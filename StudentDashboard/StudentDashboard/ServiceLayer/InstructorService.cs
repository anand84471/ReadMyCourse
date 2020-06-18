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
        public InstructorService()
        {
            objInstructorDTO = new InstructorDTO();
        }
        public async Task<bool> RegisterNewUser(InstructorRegisterModel objInstructorRegisterModel)
        {
            bool result = false;
            try
            {
                string EncryptedPassword = SHA256Encryption.ComputeSha256Hash(objInstructorRegisterModel.m_strPassword);
                objInstructorRegisterModel.m_strPassword = EncryptedPassword;
                result = await objInstructorDTO.RegisterNewInstructor(objInstructorRegisterModel);
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