using CPDataService.CLoggers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CPDataService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class CpDataService : ICpDataService
    {
        SqlConnection m_con;
        SqlCommand m_command;
        SqlDataReader m_reader;
        string m_ConnectionString;
        SqlTransaction m_transaction;
        StringBuilder m_strLogMessage = new StringBuilder();
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        public void InitDB()
        {
            try
            {

                m_ConnectionString = ConfigurationManager.ConnectionStrings["RegistrationConnString"].ToString();
                if (String.IsNullOrEmpty(m_ConnectionString))
                {
                    m_strLogMessage.Append("Connection String is not present to connect to DB");
                    m_strLogMessage.Append("Default Connection String is:" + "");
                    CpLogger.Info(m_strLogMessage);
                }
                else
                {
                    // m_log.Info("Connection String to connect to DB is:" + m_ConnectionString);
                }
                m_con = new SqlConnection(m_ConnectionString);

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);

            }
        }
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        public bool RegisterNewUser(string FirstName,string LastName,string PhoneNo,string Email,string Password)
        {
            bool result = false;
            string strCurrentMethodName = "RegisterNewUser";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewUserDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strFirstName", SqlDbType.VarChar,250).Value = FirstName;
                m_command.Parameters.Add("@strLastName", SqlDbType.VarChar, 250).Value = LastName;
                m_command.Parameters.Add("@strPhone", SqlDbType.VarChar, 10).Value = PhoneNo;
                m_command.Parameters.Add("@strEmail", SqlDbType.VarChar, 250).Value = Email;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 250).Value = Password;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet ValidateLoginDetails(string Email, string Password)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "ValidateLoginDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spValidateStudentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strEmail", SqlDbType.VarChar, 250).Value = Email;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 250).Value = Password;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorDetails(int Id)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = Id;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorTestDetails(int Id)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorTestDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorTestDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = Id;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorAssignmentDetails(int Id)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorAssignmentDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorAssignmentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = Id;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorActivityDetails(int Id)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorActivityDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorActivityDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = Id;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }

        public bool RegisterNewInstructor(string FirstName, string LastName, string PhoneNo, string Email, string Password)
        {
            bool result = false;
            string strCurrentMethodName = "RegisterNewInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_RegisterNewInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strFirstName", SqlDbType.VarChar, 250).Value = FirstName;
                m_command.Parameters.Add("@strLastName", SqlDbType.VarChar, 250).Value = LastName;
                m_command.Parameters.Add("@strPhoneNo", SqlDbType.VarChar, 10).Value = PhoneNo;
                m_command.Parameters.Add("@strEmail", SqlDbType.VarChar, 250).Value = Email;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 250).Value = Password;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateInstructorDetails(string FirstName, string LastName, string PhoneNo, string Gender,string AddressLine1,string AddressLine2,
                                          int CityId,int StateId,string PinCode,int InstructorId)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateInstructorDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateInstructorDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strFirstName", SqlDbType.VarChar, 250).Value = FirstName;
                m_command.Parameters.Add("@strLastName", SqlDbType.VarChar, 250).Value = LastName;
                m_command.Parameters.Add("@strPhoneNo", SqlDbType.VarChar, 10).Value = PhoneNo;
                m_command.Parameters.Add("@charGender", SqlDbType.VarChar, 1).Value = Gender;
                m_command.Parameters.Add("@strAddressLine1", SqlDbType.VarChar, 100).Value = AddressLine1;
                m_command.Parameters.Add("@strAddressLine2", SqlDbType.VarChar, 100).Value = AddressLine2;
                m_command.Parameters.Add("@iCityId", SqlDbType.Int).Value = CityId;
                m_command.Parameters.Add("@iStateId", SqlDbType.Int).Value = StateId;
                m_command.Parameters.Add("@strPinCode", SqlDbType.VarChar, 6).Value = PinCode;
                m_command.Parameters.Add("iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateInstructorPassword(string Password, int InstructorId)
        {
            bool result = false;
            string strCurrentMethodName = "RegisterNewInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateInstructorDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 250).Value = Password;
                m_command.Parameters.Add("iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet ValidateInstructorLoginDetails(string Email, string Password)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "ValidateLoginDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_ValidateInstructorLoginDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strEmail", SqlDbType.VarChar, 250).Value = Email;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 250).Value = Password;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
                m_strLogMessage.Append("\n ---------------------------Funtion finished--------------------------------------");

                CpLogger.Info(m_strLogMessage);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorPostLoginDetails(int Id)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorPostLoginDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInstructorPostLoginDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = Id;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
                m_strLogMessage.Append("\n ---------------------------Funtion finished--------------------------------------");
                CpLogger.Info(m_strLogMessage);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllCountryDetails()
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllCountryDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllCountryDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllStateDetailsOfCountry(int CountryId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllStateDetailsOfCountry";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllStatesOfCountry", m_con);
                m_command.Parameters.Add("@iCountryId", SqlDbType.Int).Value = CountryId;
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllCityDetailsOfState(int StateId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllCityDetailsOfState";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllCitiesOfState", m_con);
                m_command.Parameters.Add("@iStateId", SqlDbType.Int).Value = StateId;
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertNewSchoolDetails(string SchoolName, string AddressLine1, string AddressLine2, int CityId, int PinCode,string SchoolUserId,
                                          string Password,string PhoneNo,string Email)
        {
            bool result = false;
            string strCurrentMethodName = "RegisterNewInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewSchoolDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strName", SqlDbType.VarChar, 250).Value =SchoolName;
                m_command.Parameters.Add("@strAddressLine1", SqlDbType.VarChar, 250).Value = AddressLine1;
                m_command.Parameters.Add("@strAddressLine2", SqlDbType.VarChar, 250).Value = AddressLine2;
                m_command.Parameters.Add("@iCityId", SqlDbType.Int).Value = CityId;
                m_command.Parameters.Add("@iPinCode", SqlDbType.Int).Value = PinCode;
                m_command.Parameters.Add("@strSchoolUserId", SqlDbType.VarChar, 250).Value = SchoolUserId;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 100).Value = Password;
                m_command.Parameters.Add("@strPhoneNo", SqlDbType.VarChar, 10).Value = PhoneNo;
                m_command.Parameters.Add("@strEmail", SqlDbType.VarChar, 1100).Value = Email;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet CheckIsSchoolUserIdAlreadyExists(string SchoolUserIdToCheck)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckIsSchoolUserIdAlreadyExists";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckIsSchoolIdExists", m_con);
                m_command.Parameters.Add("@strSchoolUserId", SqlDbType.VarChar,100).Value = SchoolUserIdToCheck;
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet ValidateSchoolLoginDetails(string Email, string Password)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "ValidateSchoolLoginDetails";
            try
            {
                InitDB();
                if (m_con != null)
                    m_command = new SqlCommand("Cp_spValidateSchoolLogIn", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strUsername", SqlDbType.VarChar, 250).Value = Email;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 250).Value = Password;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        //Cp_spInsertNewSchoolDetails

        public bool InsertNewCourse(string CourseName, string CourseDescription, string InstructorId,ref long CourseId)
        {
            bool result = false;
            string strCurrentMethodName = "InertNewCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewCourseDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strCourseName", SqlDbType.VarChar, 250).Value = CourseName;
                m_command.Parameters.Add("@strCousreDescription", SqlDbType.VarChar, 500).Value = CourseDescription;
                m_command.Parameters.Add("@strInstructorUserName", SqlDbType.VarChar,250).Value = InstructorId;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    CourseId = (long)(m_command.Parameters["@llCourseId"].Value);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InertNewCourseIndex(string IndexName, string IndexDescription, long CourseId,ref long IndexId)
        {
            bool result = false;
            string strCurrentMethodName = "InertNewCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertCourseIndex", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strIndexName", SqlDbType.VarChar, 250).Value = IndexName;
                m_command.Parameters.Add("@strIndexDescription", SqlDbType.VarChar, 500).Value = IndexDescription;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    IndexId = (long)m_command.Parameters["@llIndexId"].Value;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewTopic(string TopicName, string TopicDescription, string FileUploadPath, short FileUploadTypeId, long IndexId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewTopic";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewTopic", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strTopicName", SqlDbType.VarChar, 100).Value = TopicName;
                m_command.Parameters.Add("@strTopicDescription", SqlDbType.VarChar, 500).Value = TopicDescription;
                if (FileUploadPath == null)
                {
                    m_command.Parameters.Add("@strFileUploadPath", SqlDbType.VarChar, 100).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@strFileUploadPath", SqlDbType.VarChar, 100).Value = FileUploadPath;
                }
                if(FileUploadTypeId==0)
                {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = DBNull.Value;
                }
                else {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = FileUploadTypeId;
                }
                
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Value = IndexId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewAssignment(string AssignmentName, string FilePath, short FileTypeId,short AssignmentType,string AssignmentDescription,ref long AssignmentId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewAssignment", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strAssignmentName", SqlDbType.VarChar, 250).Value = AssignmentName;
                if(FilePath!=null&&FilePath.Equals(string.Empty))
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = FilePath;
                }
                else
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = DBNull.Value;
                }
                
                m_command.Parameters.Add("@iAssignmentType", SqlDbType.TinyInt).Value = AssignmentType;
                if(FileTypeId==0)
                {
                    m_command.Parameters.Add("@iFileUploadTypeId", SqlDbType.TinyInt).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@iFileUploadTypeId", SqlDbType.TinyInt).Value = FileTypeId;
                }
                if(AssignmentDescription!=null)
                {
                    m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar, 500).Value = AssignmentDescription;
                }
                else
                {
                    m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar, 500).Value = DBNull.Value;
                }
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    AssignmentId = (long)m_command.Parameters["@llAssignmentId"].Value;
                    result = true;

                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewTest(string TestName,string TestDescription, string FilePath, short FileTypeId,short TestType,ref long TestId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strTestName", SqlDbType.VarChar, 250).Value = TestName;
                m_command.Parameters.Add("@strTestDescription", SqlDbType.VarChar, 250).Value = TestDescription;
                if (FilePath != null && FilePath != string.Empty)
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = FilePath;
                }
                else
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = DBNull.Value;
                }
                if(FileTypeId==0)
                {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.Int).Value = FileTypeId;
                }
               
                m_command.Parameters.Add("@iTestType", SqlDbType.TinyInt).Value = TestType;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    TestId = (long)m_command.Parameters["@llTestId"].Value;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewMcqAssignment(long AssignmentId,string QuestionStatement,string Option1,string Option2, string Option3, string Option4,short CorrectOption)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewMcqAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewMcqAssignmentQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@strQuestionStatement", SqlDbType.VarChar, 250).Value = QuestionStatement;
                m_command.Parameters.Add("@strOption1", SqlDbType.VarChar, 100).Value = Option1;
                m_command.Parameters.Add("@strOption2", SqlDbType.VarChar, 100).Value = Option2;
                m_command.Parameters.Add("@strOption3", SqlDbType.VarChar, 100).Value = Option3;
                m_command.Parameters.Add("@strOption4", SqlDbType.VarChar, 100).Value = Option4;
                m_command.Parameters.Add("@iCorrectOption", SqlDbType.TinyInt).Value = CorrectOption;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                { 
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewMcqTestQuestion(long TestId, string QuestionStatement, string Option1, string Option2, string Option3, string Option4, short CorrectOption,
                                int TimeForQuestionInSeconds,int Marks)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewMcqTestQuestion";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewMcqTestQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@strQuestionStatement", SqlDbType.VarChar, 250).Value = QuestionStatement;
                m_command.Parameters.Add("@strOption1", SqlDbType.VarChar, 100).Value = Option1;
                m_command.Parameters.Add("@strOption2", SqlDbType.VarChar, 100).Value = Option2;
                m_command.Parameters.Add("@strOption3", SqlDbType.VarChar, 100).Value = Option3;
                m_command.Parameters.Add("@strOption4", SqlDbType.VarChar, 100).Value = Option4;
                m_command.Parameters.Add("@iCorrectOption", SqlDbType.TinyInt).Value = CorrectOption;
                m_command.Parameters.Add("@iMarks", SqlDbType.Int).Value = Marks;
                m_command.Parameters.Add("@iTimeInSeconds", SqlDbType.Int).Value = TimeForQuestionInSeconds;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertAssignmentIdToIndex(long AssignmentId,long IndexId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertAssignmentIdToIndex";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertAssignmentIdToIndex", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Value = IndexId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertTestIdToIndex(long TestId, long IndexId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertTestIdToIndex";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertTestIdToIndex", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTest", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Value = IndexId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertActivityForInstructor(int InstructorId, string ActivityMessgae)
        {
            bool result = false;
            string strCurrentMethodName = "InsertActivityForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertActivityForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llInstructorId", SqlDbType.Int).Value = InstructorId;
                m_command.Parameters.Add("@strActivityMessage", SqlDbType.VarChar,1000).Value = ActivityMessgae;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllCourse(long InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllCourseDetailsForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llInstructorId", SqlDbType.BigInt).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool GetInstructorIdFromUserId(string InstructorId,ref long Id)
        {
            bool result = false;
            string strCurrentMethodName = "GetInstructorIdFromUserId";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetinstructorIdFromUserId", m_con);
                m_command.CommandType = CommandType.StoredProcedure;
                m_command.Parameters.Add("@llInstructorUserName", SqlDbType.VarChar, 250).Value = InstructorId;
                m_command.Parameters.Add("@llIstructorId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                m_command.ExecuteNonQuery();
                Id = (long)m_command.Parameters["@llIstructorId"].Value;
                if(Id>0)
                {
                    result = true;
                }
                
            }
            catch(Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetIndexDetailsOfCourse(long CourseId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetCourseIndexDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAssignmentDetails(long AssignmentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAssignmentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetMcqAssignmentDetails(long AssignmentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetMcqAssignmentDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetMcqAssignmentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetCourseDetails(long CourseId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetCourseDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetCourseDetail", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertNewIndependentAssignment(int InstructorId,string AssignmentName, string AssignmentDescription,string FilePath, short FileTypeId, short AssignmentType, ref long AssignmentId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewIndependentAssignment", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strAssignmentName", SqlDbType.VarChar, 250).Value = AssignmentName;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                if (AssignmentDescription != null && !AssignmentDescription.Equals(string.Empty))
                {
                    m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar, 500).Value = AssignmentDescription;
                }
                else
                {
                    m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar, 500).Value = DBNull.Value;
                }
                if (FilePath != null && FilePath.Equals(string.Empty))
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = FilePath;
                }
                else
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = DBNull.Value;
                }

                m_command.Parameters.Add("@iAssignmentType", SqlDbType.TinyInt).Value = AssignmentType;
                if (FileTypeId == 0)
                {
                    m_command.Parameters.Add("@iFileUploadTypeId", SqlDbType.TinyInt).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@iFileUploadTypeId", SqlDbType.TinyInt).Value = FileTypeId;
                }
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    AssignmentId = (long)m_command.Parameters["@llAssignmentId"].Value;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewIndependentTest(int InstructorId, string TestName, string TestDescription, string FilePath, short FileTypeId, short TestType, ref long TestId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewIndependentTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strTestName", SqlDbType.VarChar, 250).Value = TestName;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                if (TestDescription != null && !TestDescription.Equals(string.Empty))
                {
                    m_command.Parameters.Add("@strTestDescription", SqlDbType.VarChar, 500).Value = TestDescription;
                }
                else
                {
                    m_command.Parameters.Add("@strTestDescription", SqlDbType.VarChar, 500).Value = DBNull.Value;
                }
                if (FilePath != null && FilePath.Equals(string.Empty))
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = FilePath;
                }
                else
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = DBNull.Value;
                }

                m_command.Parameters.Add("@iTestType", SqlDbType.TinyInt).Value = TestType;
                if (FileTypeId == 0)
                {
                    m_command.Parameters.Add("@iFileUploadTypeId", SqlDbType.TinyInt).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@iFileUploadTypeId", SqlDbType.TinyInt).Value = FileTypeId;
                }
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    TestId = (long)m_command.Parameters["@llTestId"].Value;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteCourse(long CourseId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool ActivateCourse(long CourseId)
        {
            bool result = false;
            string strCurrentMethodName = "ActivateCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spActivateCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateCourseDetails(long CourseId,string CourseDescription)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateCourseDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateCourseDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@strCourseDescription", SqlDbType.VarChar, 500).Value = CourseDescription;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateCourseIndexDetails(long IndexId, string IndexName,string IndexDescription)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateCourseIndexDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateCourseIndexDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iIndexId", SqlDbType.BigInt).Value = IndexId;
                m_command.Parameters.Add("@strIndexName", SqlDbType.VarChar, 500).Value = IndexName;
                m_command.Parameters.Add("@strIndexDescription", SqlDbType.VarChar, 500).Value = IndexDescription;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }

        public bool UpdateIndexTopicDetails(long TopicId, string TopicName, string TopicDescription,string FilePath,byte FileType)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateCourseIndexDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateIndexTopic", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iTopicId", SqlDbType.BigInt).Value = TopicId;
                m_command.Parameters.Add("@strTopicName", SqlDbType.VarChar, 100).Value = TopicName;
                m_command.Parameters.Add("@strTopicDescription", SqlDbType.VarChar, 250).Value = TopicDescription;
                m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 100).Value = FilePath;
                m_command.Parameters.Add("@iFileType", SqlDbType.TinyInt).Value = FileType;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateAssignmentDetails(long AssignmentId, string AssignmentName, string AssignmentDescription,string FilePath,byte FileType)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateCourseIndexDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateAssignmentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@strAssignmentName", SqlDbType.VarChar, 250).Value = AssignmentName;
                if (AssignmentDescription != null)
                {
                    m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar, 500).Value = AssignmentDescription;
                }
                else
                {
                    m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar, 500).Value = DBNull.Value;
                }
                if (FilePath != null&&FilePath!=string.Empty)
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = FilePath;
                }
                else
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = DBNull.Value;
                }
                if(FileType!=null&&FileType!=0)
                {
                    m_command.Parameters.Add("@iFileType", SqlDbType.TinyInt).Value = FileType;
                }
                else
                {
                    m_command.Parameters.Add("@iFileType", SqlDbType.TinyInt).Value = DBNull.Value;
                }
               
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateTestDetails(long TestId, string TestName, string TestDescription, string FilePath, byte FileType)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateTestDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateTestDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@strTestName", SqlDbType.VarChar, 250).Value = TestName;
                m_command.Parameters.Add("@strTestDescription", SqlDbType.VarChar, 500).Value = TestDescription;
                //m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = FilePath;
                //m_command.Parameters.Add("@iFileType", SqlDbType.TinyInt).Value = FileType;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteTest(long TestId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteTestOfCourse(long TestId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteTestOfCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteCourseTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteIndepenetTest(long TestId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteIndepenetTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteIndependentTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool ActivateAssignment(long AssignmentId,string ShareCode,string TinyUrl)
        {
            bool result = false;
            string strCurrentMethodName = "ActivateAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spActivateAssignment", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@strShareCode", SqlDbType.VarChar,10).Value = ShareCode;
                m_command.Parameters.Add("@strTinyUrl", SqlDbType.VarChar, 100).Value = TinyUrl;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteAssignmentOfCourse(long AssignmentId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteAssignmentOfCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteMcqQuestionOfAssignment(long QuestionId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteMcqQuestionOfAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteMcqAssignmentQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llQuestionId", SqlDbType.BigInt).Value = QuestionId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteIndexTopic(long TopicId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteIndeXTopic";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteTopic", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTopicId", SqlDbType.BigInt).Value = TopicId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateIndexTopic(long TopicId,string TopicName,string TopicDescription,string FilePathMapToServer,byte? FileTypeId)
        {
            bool result = false;
            string strCurrentMethodName = "Cp_spUpdateTopicDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateTopicDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTopicId", SqlDbType.BigInt).Value = TopicId;
                m_command.Parameters.Add("@strTopicName", SqlDbType.VarChar, 256).Value = TopicName;
                m_command.Parameters.Add("@strTopicDescription", SqlDbType.VarChar, 4000).Value = TopicDescription;
                if (FilePathMapToServer != null&&FilePathMapToServer!=string.Empty)
                {
                    m_command.Parameters.Add("@strFilePathMapToServer", SqlDbType.VarChar, 100).Value = FilePathMapToServer;
                    
                }
                else
                {
                    m_command.Parameters.Add("@strFilePathMapToServer", SqlDbType.VarChar, 100).Value = DBNull.Value; 
                }
                if(FileTypeId!=null)
                {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = FileTypeId;
                }
                else
                {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = DBNull.Value;
                }
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateMcqQuestionOfAssignment(long QuestionId,string QuestionStatement,string Option1,string Option2,string Option3,string Option4,byte CorrectOption)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateMcqQuestionOfAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateAssignmentMcqQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strQuestionStatement", SqlDbType.VarChar,1000).Value = QuestionStatement;
                m_command.Parameters.Add("@llQuestionId", SqlDbType.BigInt).Value = QuestionId;
                m_command.Parameters.Add("@strOption1", SqlDbType.VarChar, 256).Value = Option1;
                m_command.Parameters.Add("@strOption2", SqlDbType.VarChar, 256).Value = Option2;
                m_command.Parameters.Add("@strOption3", SqlDbType.VarChar, 256).Value = Option3;
                m_command.Parameters.Add("@strOption4", SqlDbType.VarChar, 256).Value = Option4;
                m_command.Parameters.Add("@iCorrectOption", SqlDbType.TinyInt).Value = CorrectOption;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool ActivateTest(long TestId,string ShareCode,string TinyUrl)
        {
            bool result = false;
            string strCurrentMethodName = "ActivateTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spActivateTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@strShareCode", SqlDbType.VarChar,10).Value = ShareCode;
                m_command.Parameters.Add("@strTinyUrl", SqlDbType.VarChar,100).Value = TinyUrl;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertContactFormDetails(string Name,string Email,string PhoneNo,string Subject,string Message)
        {
            bool result = false;
            string strCurrentMethodName = "InsertContactFormDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertContactRequest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strName", SqlDbType.VarChar,200).Value = Name;
                m_command.Parameters.Add("@strEmail", SqlDbType.VarChar,200).Value = Email;
                m_command.Parameters.Add("@strPhoneNo", SqlDbType.VarChar,10).Value = PhoneNo;
                m_command.Parameters.Add("@strSubject", SqlDbType.VarChar).Value = Subject;
                m_command.Parameters.Add("@strMessage", SqlDbType.VarChar).Value = Message;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetIndexTopicDetails(long IndexId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetIndexTopicDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetIndexTopicDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llIndexid", SqlDbType.BigInt).Value = IndexId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetIndexDetails(long IndexId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetIndexDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetIndexDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Value = IndexId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetMcqTestDetails(long TestId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetMcqTestDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetMcqTestDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetMcqtestQuestionDetails(long TestId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetMcqtestQuestionDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllQuestionsOfMcqTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }

        public bool DeleteIndex(long IndexId)
        {
            bool result= false;
            string strCurrentMethodName = "DeleteIndex";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteCourseIndex", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Value = IndexId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteInpependentAssignment(long AssignmentId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteInpependentAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteIndependentAssignment", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateAssignmentDetails(long llAssignmentId,string AssignmentName,string AAssignmentDescription)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateAssignmentDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateCourseAssignmentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = llAssignmentId;
                m_command.Parameters.Add("@strAssignmentName", SqlDbType.VarChar,250).Value = AssignmentName;
                m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar,500).Value = AAssignmentDescription;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateFullCourseDetails(long CourseId, string CourseName, string CourseDescription)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateAssignmentDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateFullCourseDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@strCourseName", SqlDbType.VarChar, 250).Value = CourseName;
                m_command.Parameters.Add("@strCourseDescription", SqlDbType.VarChar, 500).Value = CourseDescription;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteMcqQuestion(long QuestionId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteMcqQuestion";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteMcqQuestion", m_con);
                m_command.Parameters.Add("@llQuestionId", SqlDbType.BigInt).Value = QuestionId;
                m_command.CommandType = System.Data.CommandType.StoredProcedure;               
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateMcqTestDetails(long TestId, string TestName, string TestDescroption)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateMcqTestDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateMcqTestDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@strTestName", SqlDbType.VarChar, 250).Value = TestName;
                m_command.Parameters.Add("@strTestDescription", SqlDbType.VarChar, 250).Value = TestDescroption;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateMcqQuestionDetails(long Questionid, string QuestionStatement, string Option1,string Option2,string Option3,string Option4,
                 byte CorrectOption,int iTimeForQuestion,int iMarksForQuestion)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateMcqQuestionDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateMcqTestQuestionDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strQuestionStatement", SqlDbType.VarChar,1000).Value = QuestionStatement;
                m_command.Parameters.Add("@strOption1", SqlDbType.VarChar, 256).Value = Option1;
                m_command.Parameters.Add("@strOption2", SqlDbType.VarChar, 256).Value = Option2;
                m_command.Parameters.Add("@strOption3", SqlDbType.VarChar, 256).Value = Option3;
                m_command.Parameters.Add("@strOption4", SqlDbType.VarChar, 256).Value = Option4;
                m_command.Parameters.Add("@iCorrectOption", SqlDbType.TinyInt).Value = CorrectOption;
                m_command.Parameters.Add("@iTimeForQuestion", SqlDbType.Int).Value = iTimeForQuestion;
                m_command.Parameters.Add("@iMarksForQuestion", SqlDbType.Int).Value = @iMarksForQuestion;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteMcqTest(long TestId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteMcqTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteMcqTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewTestToCourse(string TestName,string TestDescription,byte TestTypeId,string FilePath,byte? FileTypeId,
                    long CourseId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewTestToCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewTestToCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strTestName", SqlDbType.VarChar,250).Value = TestName;
                m_command.Parameters.Add("@strTestDescription", SqlDbType.VarChar, 250).Value = TestDescription;
                if (FilePath == null)
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value=FilePath;
                }
                if (FileTypeId == null || FileTypeId == 0)
                {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = FileTypeId;
                }
                m_command.Parameters.Add("@iTestType", SqlDbType.TinyInt).Value = TestTypeId;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewAssignmentToCourse(string AssignmentName, string AssignmentDescription, byte AssignmentTypeId, string FilePath, byte? FileTypeId,
                  long CourseId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewAssignmentToCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewAssignmentToCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strAssignmentName", SqlDbType.VarChar, 250).Value = AssignmentName;
                m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar, 250).Value = AssignmentDescription;
                if (FilePath != null && FilePath != string.Empty) { m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = FilePath; }
                else { m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = DBNull.Value; }
                if (FileTypeId != null && FileTypeId != 0) { m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = FileTypeId; }
                else { m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = DBNull.Value; ; }
                m_command.Parameters.Add("@iAssignmentType", SqlDbType.TinyInt).Value = AssignmentTypeId;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteMcqTestQuestion(long QuestionId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteMcqTestQuestion";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteMcqQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llQuestionId", SqlDbType.VarChar, 250).Value = QuestionId;
             
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllAssignmentsForCourse(long CourseId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllAssignmentsForCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAssignmentsOfCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.VarChar, 250).Value = CourseId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetTestsOfCourse(long CourseId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetTestsOfCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetTestOfCourses", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllQuestionsOfSubjectiveAssignment(long AssignmentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllQuestionsOfSubjectiveAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSubjectiveAssignmentQuestions", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertSubjectiveAssignmentQuestion(long AssignmentId,string QuestionStatement,string Hint)
        {
            bool result = false;
            string strCurrentMethodName = "InsertSubjectiveAssignmentQuestion";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertSubjectiveAssignmentQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAsssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@strQuestionStatement", SqlDbType.VarChar, 4000).Value = QuestionStatement;
                m_command.Parameters.Add("@strQuestionHint", SqlDbType.VarChar, 4000).Value = Hint;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateSubjectiveAssignmentQuestion(long QuestionId, string QuestionStatement, string Hint)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateSubjectiveAssignmentQuestion";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateSubjectiveAssignmentQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llQuestionId", SqlDbType.BigInt).Value = QuestionId;
                m_command.Parameters.Add("@strQuestionStatement", SqlDbType.VarChar, 4000).Value = QuestionStatement;
                m_command.Parameters.Add("@strQuestionHint", SqlDbType.VarChar, 4000).Value = Hint;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteSubjectiveAssignmentOfCourse(long AssignmentId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteSubjectiveAssignmentOfCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteSubectiveAssignmentOfCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteSubjectiveAssignmentQuestion(long QuestionId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteSubjectiveAssignmentOfCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteSubjectiveAssignmentQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llQuestionId", SqlDbType.BigInt).Value = QuestionId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool RegisterNewStudent(string FirstName,string LastName,string UserId,string HashedPassword,string PhoneNo)
        {
            bool result = false;
            string strCurrentMethodName = "RegisterNewStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strStudentFirstName", SqlDbType.VarChar,250).Value = FirstName;
                m_command.Parameters.Add("@strStudentLastName", SqlDbType.VarChar, 250).Value = LastName;
                m_command.Parameters.Add("@strSudentUserId", SqlDbType.VarChar, 250).Value = UserId;
                m_command.Parameters.Add("@strHashedPassword", SqlDbType.VarChar, 100).Value = HashedPassword;
                m_command.Parameters.Add("@strPhoneNo", SqlDbType.VarChar, 20).Value = PhoneNo;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool ValidateStudentLogin(string UserId, string HashedPassword,ref long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "ValidateStudentLogin";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spValidateStudentLogin", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strUserId", SqlDbType.VarChar, 250).Value = UserId;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 100).Value = HashedPassword;
                m_command.Parameters.Add("@bIsLoggedIn", SqlDbType.Bit).Direction = ParameterDirection.Output;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = (bool)m_command.Parameters["@bIsLoggedIn"].Value;
                    StudentId = (long)m_command.Parameters["@llStudentId"].Value;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertActivityForStudent(string ActivityMessage,long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertActivityForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewActivity", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strActivityMessage", SqlDbType.VarChar, 250).Value = ActivityMessage;
                m_command.Parameters.Add("@strActivityMessage", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool JoinStudentToCourse(long CourseId, long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "JoinStudentToCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spJoinCourseToStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet SearchForCourse(string SerachString, int MaxRowToReturn,int NoOfRowsFetch,int SortType)
        {
            DataSet sDS=new DataSet();
            string strCurrentMethodName = "SerchForCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar,100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_command.Parameters.Add("@iNoOfRowsFetched", SqlDbType.Int).Value = NoOfRowsFetch;
                m_command.Parameters.Add("@iSortType", SqlDbType.Int).Value = SortType;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet SearchForCourseForStudent(string SerachString, int MaxRowToReturn, int NoOfRowsFetch, int SortType,long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchForCourseForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForCourseForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar, 100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_command.Parameters.Add("@iNoOfRowsFetched", SqlDbType.Int).Value = NoOfRowsFetch;
                m_command.Parameters.Add("@iSortType", SqlDbType.Int).Value = SortType;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet SearchForCourseOfInstructor(string SerachString, int MaxRowToReturn,int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchForCourseOfInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForCourseOfInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar, 100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet SearchForInstructor(string SerachString, int MaxRowToReturn)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar, 100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet SearchForAssignment(string SerachString, int MaxRowToReturn)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchForAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForAssignment", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar, 100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet SearchForAssignmentOfInstructor(string SerachString, int MaxRowToReturn,int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchForAssignmentOfInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForAssignmentOfInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar, 100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet SearchForTest(string SerachString, int MaxRowToReturn)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchForTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar, 100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet SearchForTestOfInstructor(string SerachString, int MaxRowToReturn,int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchForTestOfInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForTestOfInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar, 100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetStudentDetails(long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetStudentDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetStudentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetJoinedCoursesForStudent(long StudentId,string SearchString,int MaxRowCountToReturn)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetJoinedCoursesForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetJOinedCoursesForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar,100).Value = SearchString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowCountToReturn;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool GetStudentIdFromUserId(string UserId, ref long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "GetStudentIdFromUserId";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetStudentIdFromUserId", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strUserId", SqlDbType.VarChar,250).Value = UserId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
                if(result)
                {
                    StudentId = (long)m_command.Parameters["@llStudentId"].Value;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateStudentDetails(string FirstName,string LastName,string AddressLine1,string AddressLine2,string PinCode,
                                          int StateId,int CityId,string Gender)
        {
            bool result = false;
            string strCurrentMethodName = "GetStudentIdFromUserId";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateStudentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strFirstName", SqlDbType.VarChar, 250).Value = FirstName;
                m_command.Parameters.Add("@strLastName", SqlDbType.VarChar, 250).Value = LastName;
                m_command.Parameters.Add("@strAddressLine1", SqlDbType.VarChar, 250).Value = AddressLine1;
                m_command.Parameters.Add("@strAddressLine2", SqlDbType.VarChar, 250).Value = AddressLine2;
                m_command.Parameters.Add("@strPineCode", SqlDbType.VarChar,6).Value = PinCode;
                m_command.Parameters.Add("@iStateId", SqlDbType.Int).Value = StateId;
                m_command.Parameters.Add("@iCityId", SqlDbType.Int).Value = CityId;
                m_command.Parameters.Add("@iGender", SqlDbType.VarChar,10).Value = Gender;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateStudentPassword(long StudentId,string OldHashedPassword,string NewHashedPassword)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateStudentPassword";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdatePasswordForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strHasedPasswordOld", SqlDbType.VarChar, 100).Value = OldHashedPassword;
                m_command.Parameters.Add("@strNewPasswordPassword", SqlDbType.VarChar, 100).Value = NewHashedPassword;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool JoinStudentToInstructor(long StudentId,int InstructorId)
        {
            bool result = false;
            string strCurrentMethodName = "JoinStudentToInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spJoinStudentToInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllJoinedInstructorForStudent(long StudentId, string SearchString,int MaxRowCount)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllJoinedInstructorForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllJoinedInstructorsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar,100).Value = SearchString;
                m_command.Parameters.Add("@iMaxRowToReturn", SqlDbType.Int).Value = MaxRowCount;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllAssignmentSubmissionsForStudent(long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllAssignmentSubmissionsForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllAssignmentSubmissionsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertAssignmentResponse(long StudentId, long AssignmentId, DateTime AssignmentStartTime, DateTime AssignmentFinishTime, string Response,
                     int PercentageScore,int TotalNoOfQuestions,ref long SubmissionId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertAssignmentResponse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertStudentAssignmentResponse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llAssignmentID", SqlDbType.Int).Value = AssignmentId;
                m_command.Parameters.Add("@dtAssignmentStartTime", SqlDbType.DateTime).Value = AssignmentStartTime;
                m_command.Parameters.Add("@dtAssignmentFinishTime", SqlDbType.DateTime).Value = AssignmentFinishTime;
                m_command.Parameters.Add("@strAssignmentResponse", SqlDbType.VarChar,4000).Value = Response;
                m_command.Parameters.Add("@iAssignmentPercentageScore", SqlDbType.Int).Value = PercentageScore;
                m_command.Parameters.Add("@iTotalNoOfQuestions", SqlDbType.Int).Value = TotalNoOfQuestions;
                m_command.Parameters.Add("@llSubmissionId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if(m_command.ExecuteNonQuery()>0)
                {
                    result = true;
                    SubmissionId = (long)m_command.Parameters["@llSubmissionId"].Value;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertAssignmentFeedback(long SubmissionId, string FeedBack,int Rating)
        {
            bool result = false;
            string strCurrentMethodName = "InsertAssignmentFeedback";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertAssignmentFeedback", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llSubmissionID", SqlDbType.BigInt).Value = SubmissionId;
                m_command.Parameters.Add("@strFeedBack", SqlDbType.VarChar,4000).Value = FeedBack;
                m_command.Parameters.Add("@iRating", SqlDbType.Int).Value = Rating;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAssignmentResponse(long SubmissionId,long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAssignmentResponse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAssignmentResponse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llSubmissionID", SqlDbType.BigInt).Value = SubmissionId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertTestResponse(long StudentId, long TestId, DateTime TestStartTime, DateTime TestFinishTime, string Response,
                    int PercentageScore, int TotalNoOfQuestions, ref long SubmissionId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertAssignmentResponse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertStudentTestResponse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llTestId", SqlDbType.Int).Value = TestId;
                m_command.Parameters.Add("@dtTestStartTime", SqlDbType.DateTime).Value = TestStartTime;
                m_command.Parameters.Add("@dtTestFinishTime", SqlDbType.DateTime).Value = TestFinishTime;
                m_command.Parameters.Add("@strTestResponse", SqlDbType.VarChar, 4000).Value = Response;
                m_command.Parameters.Add("@iTestPercentageScore", SqlDbType.Int).Value = PercentageScore;
                m_command.Parameters.Add("@iTotalNoOfQuestions", SqlDbType.Int).Value = TotalNoOfQuestions;
                m_command.Parameters.Add("@llSubmissionId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                    SubmissionId = (long)m_command.Parameters["@llSubmissionId"].Value;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetTestResponse(long SubmissionId,long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetTestResponse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetTestResponse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llSubmissionID", SqlDbType.BigInt).Value = SubmissionId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllTestSubmissionsForStudent(long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllTestSubmissionsForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllTestSubmissionsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetStudentHomeDetails(long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetStudentHomeDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetStudentHomeDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetStudentJoinedToCourse(long CourseId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetStudentJoinedToCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllStudentsJoinedToCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllAssignmentsSubmissions(long AssignmentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllAssignmentsSubmissions";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllAssignmentSubissions", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllTestSubmissions(long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllTestSubmissions";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllTestSubissions", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllStudentsJoinedToInstructor(int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllStudentsJoinedToInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllStudentsJoinedToInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.BigInt).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertNewCourseV2(string CourseName,string CourseDescription,int InstructorId,string AboutCourse,string CourseImagePath,ref long CourseId)
        {
            bool result = false;
            string strCurrentMethodName = "insertNewCourseV2";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewCourseV2", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.BigInt).Value = InstructorId;
                m_command.Parameters.Add("@strCourseName", SqlDbType.VarChar,250).Value = CourseName;
                m_command.Parameters.Add("@strCourseDescription", SqlDbType.VarChar, 1000).Value = CourseDescription;
                m_command.Parameters.Add("@strAboutCourse", SqlDbType.VarChar, 8000).Value = AboutCourse;
                m_command.Parameters.Add("@strCourseImagePath", SqlDbType.VarChar, 256).Value = CourseImagePath;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                    CourseId = (long)m_command.Parameters["@llCourseId"].Value;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewIndexToV2Course(long CourseId,string IndexName,string IndexContetHtml,ref long IndexId)
        {
            bool result = false;
            string strCurrentMethodName = "insertNewCourseV2";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewCourseV2", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@strIndexName", SqlDbType.VarChar, 250).Value = IndexName;
                m_command.Parameters.Add("@strIndexDescription", SqlDbType.VarChar, 8000).Value = IndexContetHtml;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                    IndexId = (long)m_command.Parameters["IndexId"].Value;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewTopicToV2Course(long IndexId,string TopicName,string TopicHtml)
        {
            bool result = false;
            string strCurrentMethodName = "insertNewCourseV2";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewCourseV2", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Value = IndexId;
                m_command.Parameters.Add("@strTopicName", SqlDbType.VarChar, 250).Value = TopicName;
                m_command.Parameters.Add("@strTopicContentHtml", SqlDbType.VarChar, 8000).Value = TopicHtml;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewAlertForInstructor(int InstructorId,string AlertMessage,int AlertTypeId,long? EffectiveContentid,long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewAlertForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewAlertForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.BigInt).Value = InstructorId;
                m_command.Parameters.Add("@strAlertMessage", SqlDbType.VarChar, 250).Value = AlertMessage;
                m_command.Parameters.Add("@iAlertTypeId", SqlDbType.Int).Value = AlertTypeId;
                if (EffectiveContentid == null)
                {
                    m_command.Parameters.Add("@llEffectedId", SqlDbType.BigInt).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@llEffectedId", SqlDbType.BigInt).Value = EffectiveContentid;
                }
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllAlertForInstructor(int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllAlertForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorAlertMessage", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.BigInt).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool GetInstructorIdByCourseId(long CourseId,ref int InstructorId)
        {
            bool result = false;
            string strCurrentMethodName = "GetInstructorIdByCourseId";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorIdByCourseId", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseID", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Direction = ParameterDirection.Output;
                m_con.Open();
                m_command.ExecuteNonQuery();
                InstructorId = (int)m_command.Parameters["@iInstructorId"].Value;
                result = true;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool GetInstructorIdByTestId(long TestId, ref int InstructorId)
        {
            bool result = false;
            string strCurrentMethodName = "GetInstructorIdByTestId";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorIdByTestId", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestid", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Direction = ParameterDirection.Output;
                m_con.Open();
                m_command.ExecuteNonQuery();
                InstructorId = (int)m_command.Parameters["@iInstructorId"].Value;
                result = true;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool GetInstructorIdByAssignmentId(long AssignmentId, ref int InstructorId)
        {
            bool result = false;
            string strCurrentMethodName = "GetInstructorIdByAssignmentId";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorIdByAssignmentid", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Direction = ParameterDirection.Output;
                m_con.Open();
                m_command.ExecuteNonQuery();
                InstructorId = (int)m_command.Parameters["@iInstructorId"].Value;
                result = true;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool MarkAlertReadForInstructor(long AlertId)
        {
            bool result = false;
            string strCurrentMethodName = "MarkAlertReadForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spMarkAlertReadForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAlertId", SqlDbType.BigInt).Value = AlertId;
                m_con.Open();
                if (m_command.ExecuteNonQuery()>0)
                {
                    result = true;
                }
               
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet CheckStudentHasJoinedTheCourse(long StudentId,long CourseId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckStudentHasJoinedTheCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckStudentHasJoinedTheCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckStudentHasSubmittedTheTest(long StudentId, long TestId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckStudentHasSubmittedTheTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spStudnetHasAlreadySubmittedTheTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckStudentHasSubmittedTheAssignment(long StudentId, long AssignmentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckStudentHasSubmittedTheAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckStudnetHasAlreadySubmittedTheAssignment", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckTestResponseIdExistsForStudent(long StudentId, long SubmissionId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckTestResponseIdExistsForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckTestResponseIdExistsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llSubmissionId", SqlDbType.BigInt).Value = SubmissionId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckAssignmentResponseIdExistsForStudent(long StudentId, long SubmissionId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckTestResponseIdExistsForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckAssignmentResponseIdExistsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llSubmissionId", SqlDbType.BigInt).Value = SubmissionId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorProfileDetails(int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorProfileDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorProfileDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.BigInt).Value = InstructorId;
                
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckTestAccess(long TestId,string AccessCode)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckTestAccess";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckTestAccess", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@strAccessCode", SqlDbType.VarChar,10).Value = AccessCode;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckAssignmentAccess(long AssignmentId, string AccessCode)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckAssignmentAccess";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckAssignmentAccess", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@strAccessCode", SqlDbType.VarChar, 10).Value = AccessCode;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetIndependentAssignmentDetails(long AssignmentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetIndependentAssignmentDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetIndependentAssignmentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetIndependentTestDetails(long TestId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetIndependentTestDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetIndependetMcqTestDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;

                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
    }
}