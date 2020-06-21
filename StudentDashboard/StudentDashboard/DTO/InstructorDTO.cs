using StudentDashboard.Models;
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
    public class InstructorDTO
    {
        CPDataService.CpDataServiceClient objCPDataService;
        StringBuilder m_strLogMessage = new StringBuilder();
        public InstructorDTO()
        {
            objCPDataService = new CPDataService.CpDataServiceClient();
        }
        public async Task<bool> RegisterNewInstructor(InstructorRegisterModel objInstructorRegisterModal)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.RegisterNewInstructorAsync(objInstructorRegisterModal.m_strFirstName, objInstructorRegisterModal.m_strLastName, objInstructorRegisterModal.m_strPhoneNo, objInstructorRegisterModal.m_strEmail, objInstructorRegisterModal.m_strPassword,
                    objInstructorRegisterModal.m_strPhoneNoVarificationGuid,objInstructorRegisterModal.m_strEmailVarificationGuid);

            }
            catch (Exception Ex)
            {

            }
            return result;
        }

        public async Task<bool> ValidateInstructorLoginDetails(InstructorRegisterModel objInstructorRegisterModel)
        {
            bool result = false;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.ValidateInstructorLoginDetailsAsync(objInstructorRegisterModel.m_strEmail, objInstructorRegisterModel.m_strPassword);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<InstructorRegisterModel> lsRegisterModel = ds.Tables[0].AsEnumerable().Select(
                      dataRow => new InstructorRegisterModel(
                          dataRow.Field<string>("INSTRUCTOR_FIRST_NAME"),
                          dataRow.Field<string>("INSTRUCTOR_LAST_NAME"),
                          dataRow.Field<int>("ID")
                          )).ToList();
                    if (lsRegisterModel.Count == 1)
                    {
                        objInstructorRegisterModel.m_iInstructorId = lsRegisterModel[0].m_iInstructorId;
                        result = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<InstructorRegisterModel> GetInstructorPostLoginDetails(int Id)
        {
            InstructorRegisterModel objResult = null;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetInstructorPostLoginDetailsAsync(Id);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<InstructorRegisterModel> lsRegisterModel = ds.Tables[0].AsEnumerable().Select(
                      dataRow => new InstructorRegisterModel(
                          dataRow.Field<int>("COURSE_COUNT"),
                          dataRow.Field<int>("ASSIGNMENT_COUNT"),
                          dataRow.Field<int>("TEST_COUNT"),
                          dataRow.Field<int>("ACTIVE_COURSES"),
                          0
                          )).ToList();
                    if (lsRegisterModel.Count == 1)
                    {
                        objResult = new InstructorRegisterModel();
                        objResult = lsRegisterModel[0];
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResult;
        }
        public async Task<InstructorRegisterModel> GetInstructorDetails(int Id)
        {
            InstructorRegisterModel objInstructorRegisterModel = null;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetInstructorDetailsAsync( Id);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<InstructorRegisterModel> lsRegisterModel = ds.Tables[0].AsEnumerable().Select(
                      dataRow => new InstructorRegisterModel(
                          dataRow.Field<string>("INSTRUCTOR_FIRST_NAME"),
                          dataRow.Field<string>("INSTRUCTOR_LAST_NAME"),
                          dataRow.Field<string>("EMAIL"),
                          dataRow.Field<string>("PHONE_NO"),
                          dataRow.Field<string>("ADDRESS_LINE_1"),
                          dataRow.Field<string>("ADDRESS_LINE_2"),
                          dataRow.Field<string>("CITY_NAME"),
                          dataRow.Field<string>("STATE_NAME"),
                          dataRow.Field<string>("PIN_CODE"),
                          dataRow.Field<string>("GENDER"),
                          dataRow.Field<string>("SCHOOL_NAME"),
                          dataRow.Field<DateTime?>("LAST_UPDATED"),
                          dataRow.Field<DateTime>("ROW_INSERTION_DATETIME")
                          )).ToList();
                    if (lsRegisterModel.Count == 1)
                    {
                        objInstructorRegisterModel = lsRegisterModel[0];
                        
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objInstructorRegisterModel;
        }
        public async Task<bool> UpdateInstructorDetails(InstructorRegisterModel objInstructorRegisterModal)
        {
            bool result = false;
            try
            {
                result =await objCPDataService.UpdateInstructorDetailsAsync(objInstructorRegisterModal.m_strFirstName, objInstructorRegisterModal.m_strLastName,
                                                      objInstructorRegisterModal.m_strPhoneNo,objInstructorRegisterModal.m_strGender,objInstructorRegisterModal.m_strAddressLine1,
                                                      objInstructorRegisterModal.m_strAddressLine2,objInstructorRegisterModal.m_iCityid,
                                                      objInstructorRegisterModal.m_iStateId,objInstructorRegisterModal.m_strPinCode
                                                      ,objInstructorRegisterModal.m_iInstructorId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdatePassword(InstructorRegisterModel objInstructorRegisterModel)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateInstructorPasswordAsync(objInstructorRegisterModel.m_strPassword, objInstructorRegisterModel.m_iInstructorId);
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> AddMcqTestQuestion(McqQuestion objMcqQuestion)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertNewMcqTestQuestionAsync(objMcqQuestion.m_llTestId, objMcqQuestion.m_strQuestionStatement, objMcqQuestion.m_strOption1, objMcqQuestion.m_strOption2,
                    objMcqQuestion.m_strOption3, objMcqQuestion.m_strOption4, objMcqQuestion.m_iCorrectOption, objMcqQuestion.m_iTimeInSeconds, objMcqQuestion.m_iMarks);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AddMcqTestQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }

    }
}