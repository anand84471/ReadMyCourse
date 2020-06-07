using StudentDashboard.DTO;
using StudentDashboard.Models;
using StudentDashboard.Models.Course;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool RegisterNewUser(InstructorRegisterModel objInstructorRegisterModel)
        {
            bool result = false;
            try
            {
                string EncryptedPassword = SHA256Encryption.ComputeSha256Hash(objInstructorRegisterModel.m_strPassword);
                objInstructorRegisterModel.m_strPassword = EncryptedPassword;
                result = objInstructorDTO.RegisterNewInstructor(objInstructorRegisterModel);
            }
            catch (Exception Ex)
            {

            }
            return result;

        }
        public bool ValidateLoginDetails(InstructorRegisterModel objInstructorRegisterModel)
        {
            bool result = false;
            try
            {
                objInstructorRegisterModel.m_strPassword = SHA256Encryption.ComputeSha256Hash(objInstructorRegisterModel.m_strPassword);
                result = objInstructorDTO.ValidateInstructorLoginDetails(objInstructorRegisterModel);

            }
            catch (Exception Ex)
            {

            }
            return result;

        }
        public InstructorRegisterModel GetInstructorPostLoginDetails(int Id)
        {
           return objInstructorDTO.GetInstructorPostLoginDetails(Id);
        }
        public InstructorRegisterModel GetInstructorDetails(int InstructorId)
        {
            return objInstructorDTO.GetInstructorDetails(InstructorId);
        }
        public bool UpdateInstructorDetails(InstructorRegisterModel objInstructorRegisterModel)
        {
            return objInstructorDTO.UpdateInstructorDetails(objInstructorRegisterModel);
        }

    }
}