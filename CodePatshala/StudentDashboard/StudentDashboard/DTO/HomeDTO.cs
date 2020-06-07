using StudentDashboard.HttpRequest;
using StudentDashboard.HttpResponse;
using StudentDashboard.Models;
using StudentDashboard.Models.Alert;
using StudentDashboard.Models.Course;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace StudentDashboard.DTO
{
    public class HomeDTO
    {
        CPDataService.CpDataServiceClient objCPDataService;
        StringBuilder m_strLogMessage = new StringBuilder();
        public HomeDTO()
        {
            objCPDataService = new CPDataService.CpDataServiceClient();
        }
        public bool RegisterNewUser(StaeModel objRegisterModel)
        {
            bool result = false;
            try
            {
                result = objCPDataService.RegisterNewUser(objRegisterModel.strFisrtName, objRegisterModel.strLastName, objRegisterModel.strPhoneNo, objRegisterModel.strEmail, objRegisterModel.strPassword);

            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewUser", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool ValidateLogineDetails(StaeModel objRegisterModel)
        {
            bool result = false;
            try
            {
                DataSet ds = new DataSet();
               
                ds = objCPDataService.ValidateLoginDetails(objRegisterModel.strEmail, objRegisterModel.strPassword);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<StaeModel> lsRegisterModel = ds.Tables[0].AsEnumerable().Select(
                      dataRow => new StaeModel(
                          dataRow.Field<string>("FIRST_NAME"),
                          dataRow.Field<string>("LAST_NAME")
                          )).ToList();
                    if(lsRegisterModel.Count==1)
                    {
                        objRegisterModel.strFisrtName = lsRegisterModel[0].strFisrtName;
                        objRegisterModel.strLastName = lsRegisterModel[0].strLastName;
                        result = true;
                    }
                }
               
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ValidateLogineDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool GetStudentDetails(int StudentId)
        {
            bool result = false;
            try
            {
                //    result = objCPDataService.RegisterNewUser(objRegisterModel.strFisrtName, objRegisterModel.strLastName, objRegisterModel.strPhoneNo, objRegisterModel.strEmail, objRegisterModel.strPassword);
                //}
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetStudentDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewAssignment(AssignmentModel objAssignmentModel)
        {
            bool result = false;
            try
            {
                long AssignmentId=0;
                result = objCPDataService.InsertNewAssignment(objAssignmentModel.m_strAssignmentName,objAssignmentModel.m_strFileUploadPath,objAssignmentModel.m_iFileUploadTypeId,objAssignmentModel.m_iAssignmentType,objAssignmentModel.m_strAssignmentDescription,ref AssignmentId);
                objAssignmentModel.m_llAssignemntId = AssignmentId;
            }

            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewIndex(IndexModel objIndexModel)
        {
            bool result = false;
            try
            {
                long IndexId=0;
                result = objCPDataService.InertNewCourseIndex(objIndexModel.m_strIndexName,objIndexModel.m_strIndexDescription,objIndexModel.m_llCourseId,
                                                              ref IndexId);
                if(IndexId!=0)
                {
                    objIndexModel.m_llIndexId = IndexId;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewTest(TestModel objTestModel)
        {
            bool result = false;
            try
            {
                long TestId = 0;
                result = objCPDataService.InsertNewTest(objTestModel.m_strTestName,objTestModel.m_strTestDescription,objTestModel.m_strFilePath,objTestModel.m_iFileTypeId,objTestModel.m_iTestType,ref TestId);
                objTestModel.m_llTestId = TestId;
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewTopic(TopicModel objTopicModel )
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertNewTopic(objTopicModel.m_strTopicName, objTopicModel.m_strTopicDescription, 
                                     objTopicModel.m_strFilePath, objTopicModel.m_iFileUploadTypeId,objTopicModel.m_llIndexId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewTopic", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewCourse(CourseModel objCourseModel)
        {
            bool result = false;
            try
            {
                long CourseId = 0;
                result = objCPDataService.InsertNewCourse(objCourseModel.m_strCourseName,objCourseModel.m_strCourseDescription,
                                                       objCourseModel.m_strInstructorUserName, ref CourseId);
                if(CourseId!=-1)
                {
                    objCourseModel.m_llCourseId = CourseId;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewMcqAssignmentQuestion(McqQuestion objMcqQuestion)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertNewMcqAssignment(objMcqQuestion.m_llAssignmentId, objMcqQuestion.m_strQuestionStatement,
                                          objMcqQuestion.m_strOption1,objMcqQuestion.m_strOption2,objMcqQuestion.m_strOption3,objMcqQuestion.m_strOption4,
                                          objMcqQuestion.m_iCorrectOption);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewMcqAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewMcqTestQuestion(McqQuestion objMcqQuestion)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertNewMcqTestQuestion(objMcqQuestion.m_llTestId, objMcqQuestion.m_strQuestionStatement,
                                          objMcqQuestion.m_strOption1, objMcqQuestion.m_strOption2, objMcqQuestion.m_strOption3, objMcqQuestion.m_strOption4,
                                          objMcqQuestion.m_iCorrectOption,objMcqQuestion.m_iTimeInSeconds,objMcqQuestion.m_iMarks);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewMcqTestQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertAssignmentIdToIndex(long AssignmentId,long IndexId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertAssignmentIdToIndex(AssignmentId,IndexId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignmentIdToIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertTestIdToIndex(long Test, long IndexId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertTestIdToIndex(Test, IndexId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertTestIdToIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }

        public bool GetAllCourseDetailsForInstructor(long InstructorId, ref List<CourseDetailsModel> lsCourseModel)
        {
            bool result = false;
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetAllCourse(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsCourseModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CourseDetailsModel(
                         dataRow.Field<long>("COURSE_ID"),
                         dataRow.Field<string>("COURSE_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("TOTAL_INDEXES"),
                          dataRow.Field<string>("COURSE_STATUS_NAME"),
                           dataRow.Field<int>("NO_OF_STUDENTS_JOINED")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllCourseDetailsForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public List<SubjectiveQuestion> GetAllQuestionsOfSubjectiveAssignment(long AssignmentId)
        {
            List<SubjectiveQuestion> lsSubjectiveQuestion = null;
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetAllQuestionsOfSubjectiveAssignment(AssignmentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsSubjectiveQuestion = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new SubjectiveQuestion(
                         AssignmentId,
                         dataRow.Field<long>("QUESTION_ID"),
                         dataRow.Field<string>("QUESTION_STATEMENT"),
                         dataRow.Field<string>("QUESTION_HINT"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DATETIME").ToString("d MMM yyyy")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllCourseDetailsForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsSubjectiveQuestion;
        }
        public  bool GetInstructorIdFromUserId(string InstructorId, ref long Id)
        {
            bool result = true;
            try
            {
                result = objCPDataService.GetInstructorIdFromUserId(InstructorId, ref Id);
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorIdFromUserId", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public TestModel GetTestDetails(long TestId)
        {
            TestModel objTestModel = null;
            try
            {
                DataSet ds = objCPDataService.GetMcqTestDetails(TestId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objTestModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new TestModel(
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<string>("TEST_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int?>("TOTAL_NO_OF_QUESTIONS"),
                         dataRow.Field<int?>("TOTAL_MARKS"),
                         dataRow.Field<int?>("TOTAL_ALLOWED_TIME")
                         )).ToList()[0];
                }

            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objTestModel;
        }
        public List<McqQuestion> GetMcqTestQuestions(long TestId)
        {
            List<McqQuestion> lsMcqQuestion = null;
            try
            {
                DataSet ds = objCPDataService.GetMcqtestQuestionDetails(TestId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsMcqQuestion = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new McqQuestion(
                         dataRow.Field<long>("ID"),
                         dataRow.Field<string>("QUESTION_STATEMENT"),
                         dataRow.Field<string>("OPTION1"),
                         dataRow.Field<string>("OPTION2"),
                         dataRow.Field<string>("OPTION3"),
                         dataRow.Field<string>("OPTION4"),
                         dataRow.Field<byte>("CORRECT_ANSWER"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("MARKS"),
                         dataRow.Field<int>("TIME_FOR_QUESTION_IN_SECONDS")
                         )).ToList();
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetMcqTestQuestions", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsMcqQuestion;
        }

        public List<CourseIndexDetails> GetCourseIndexDetails(long CourseId)
        {
            List<CourseIndexDetails> lsIndexDetails = new List<CourseIndexDetails>();
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetIndexDetailsOfCourse(CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsIndexDetails = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CourseIndexDetails(
                         dataRow.Field<string>("INDEX_NAME"),
                         dataRow.Field<string>("INDEX_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<String>("ASSIGNMENT_NAME"),
                         dataRow.Field<byte?>("ASSIGNMENT_TYPE"),
                         dataRow.Field<String>("TEST_NAME"),
                         dataRow.Field<byte?>("TEST_TYPE"),
                         dataRow.Field<int>("TOPIC_COUNT"),
                         dataRow.Field<long>("INDEX_ID"),
                         dataRow.Field<long?>("ASSIGNMENT_ID"),
                         dataRow.Field<long?>("TEST_ID")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsIndexDetails;
        }
        public GetCourseDetailsApiResponse GetCourseDetails(long CourseId)
        {
            List<GetCourseDetailsApiResponse> lsGetCourseDetailsApiResponse = null;
            GetCourseDetailsApiResponse objGetCourseDetailsApiResponse = null;
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetCourseDetails(CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsGetCourseDetailsApiResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new GetCourseDetailsApiResponse(
                         dataRow.Field<string>("COURSE_NAME"),
                         dataRow.Field<string>("COURSE_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DETETIME").ToString("d MMM yyyy")
                         )).ToList();
                }
                if(lsGetCourseDetailsApiResponse!=null&&lsGetCourseDetailsApiResponse.Count>0)
                {
                    objGetCourseDetailsApiResponse = lsGetCourseDetailsApiResponse[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objGetCourseDetailsApiResponse;
        }


       
        public bool UpdateTestDetails(TestModel objTestModel)
        {
            bool result = false;
            try
            {
                result = objCPDataService.UpdateTestDetails(objTestModel.m_llTestId,objTestModel.m_strTestName,objTestModel.m_strTestDescription,
                                                           objTestModel.m_strFilePath,(byte)objTestModel.m_iFileTypeId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool UpdateAssignmentDetails(AssignmentModel objAssignmentModel)
        {
            bool result = false;
            try
            {
                result = objCPDataService.UpdateAssignmentDetails(objAssignmentModel.m_llAssignemntId,objAssignmentModel.m_strAssignmentName,
                                          objAssignmentModel.m_strAssignmentDescription,objAssignmentModel.m_strFileUploadPath,(byte)objAssignmentModel.m_iFileUploadTypeId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateAssignmentDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool UpdateIndexTopicDetails(TopicModel objTopicModel)
        {
            bool result = false;
            try
            {
                result = objCPDataService.UpdateIndexTopicDetails(objTopicModel.m_llTopicId,objTopicModel.m_strTopicName,objTopicModel.m_strTopicDescription,
                                                                  objTopicModel.m_strFilePath,(byte)objTopicModel.m_iFileUploadTypeId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateIndexTopicDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool UpdateCourseIndexDetails(IndexModel objIndexModel)
        {
            bool result = false;
            try
            {
                result = objCPDataService.UpdateCourseIndexDetails(objIndexModel.m_llIndexId,objIndexModel.m_strIndexName,objIndexModel.m_strIndexDescription);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool UpdateCourseDetails(CourseModel objCourseModel)
        {
            bool result = false;
            try
            {
                result = objCPDataService.UpdateCourseDetails(objCourseModel.m_llCourseId,objCourseModel.m_strCourseDescription);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool ActivateCourse(long CourseId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.ActivateCourse(CourseId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ActivateCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool DeleteCourse(long CourseId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.DeleteCourse(CourseId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertActivityForInstructor(int InstructorId, string ActivityMessage)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertActivityForInstructor(InstructorId, ActivityMessage);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertTestIdToIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewIndependentAssignment(AssignmentModel objAssignmentModel)
        {
            bool result = false;
            try
            {
                long AssignmentId = 0;
                result = objCPDataService.InsertNewIndependentAssignment(objAssignmentModel.m_iInstructorId, objAssignmentModel.m_strAssignmentName,objAssignmentModel.m_strAssignmentDescription,objAssignmentModel.m_strFileUploadPath
                                ,objAssignmentModel.m_iFileUploadTypeId,objAssignmentModel.m_iAssignmentType,ref AssignmentId);
                if(result)
                {
                    objAssignmentModel.m_llAssignemntId = AssignmentId;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertTestIdToIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewIndependentTest(TestModel objTestModel)
        {
            bool result = false;
            try
            {
                long TestId = 0;
                result = objCPDataService.InsertNewIndependentTest(objTestModel.m_iInstructorId, objTestModel.m_strTestName,objTestModel.m_strTestDescription,
                    objTestModel.m_strFilePath,objTestModel.m_iFileTypeId,objTestModel.m_iTestType,ref TestId);
                if (result)
                {
                    objTestModel.m_llTestId = TestId;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewIndependentTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public List<AssignmentDetailsModel> GetAssignmentForInstructor(int InstructorId)
        {
            List<AssignmentDetailsModel> lsAssignmentDetailsModel = null;
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetInstructorAssignmentDetails(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsAssignmentDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AssignmentDetailsModel(
                         dataRow.Field<long>("ASSIGNMENT_ID"),
                         dataRow.Field<string>("ASSIGNMENT_NAME"),
                         dataRow.Field<string>("ASSIGNMENT_DESCRIPTION"),
                         dataRow.Field<byte>("ASSIGNMENT_TYPE"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_QUESTIONS"),
                          dataRow.Field<int>("NO_OF_SUBJECTIVE_QUESTIONS"),
                           dataRow.Field<int>("NO_OF_SUBMISSIONS")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAssignmentForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentDetailsModel;
        }
        public List<TestDetailsModel> GetInstructorTestDetails(int InstructorId)
        {
            List<TestDetailsModel> lsTestDetailsModel = null;
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetInstructorTestDetails(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsTestDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new TestDetailsModel(
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<string>("TEST_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_QUESTIONS"),
                          dataRow.Field<byte>("TEST_TYPE"),
                          dataRow.Field<int>("NO_OF_SUBMISSIONS")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsTestDetailsModel;
        }
        public List<TopicModel> GetIndexTopicDetails(long TopicId)
        {
            List<TopicModel> lsTopicModel = null;
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetIndexTopicDetails(TopicId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsTopicModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new TopicModel(
                         dataRow.Field<long>("ID"),
                         dataRow.Field<string>("TOPIC_NAME"),
                         dataRow.Field<string>("TOPIC_DESCRIPTION"),
                         dataRow.Field<string>("FILE_PATH_MAP_TO_SERVER"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROw_UODATION_DATETIME").ToString("d MMM yyyy")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsTopicModel;
        }
        public IndexModel  GetIndexDetails(long TopicId)
        {
            IndexModel objIndexModel = null;
            try
            {
                List<IndexModel> lsIndexModel=null;
                DataSet ds = new DataSet();
                ds = objCPDataService.GetIndexDetails(TopicId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsIndexModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new IndexModel(
                         dataRow.Field<long>("ID"),
                         dataRow.Field<string>("INDEX_NAME"),
                         dataRow.Field<string>("INDEX_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROw_UPDATION_DATETIME").ToString("d MMM yyyy")
                         )).ToList();
                }
                if(lsIndexModel != null&&lsIndexModel.Count>0)
                {
                    objIndexModel = lsIndexModel[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objIndexModel;
        }
        public AssignmentModel GetAssignmentDetails(long AssignmentId)
        {
            AssignmentModel objAssignmentModel = null;
            try
            {
                List<AssignmentModel> lsAssignmentModel = null;
                DataSet ds = new DataSet();
                ds = objCPDataService.GetAssignmentDetails(AssignmentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsAssignmentModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AssignmentModel(
                         dataRow.Field<string>("ASSIGNMENT_NAME"),
                         dataRow.Field<string>("ASSIGNMENT_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROw_UPDATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<byte>("ASSIGNMENT_TYPE")
                         )).ToList();
                }
                if (lsAssignmentModel != null && lsAssignmentModel.Count > 0)
                {
                    objAssignmentModel = lsAssignmentModel[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objAssignmentModel;
        }


        public List<ActivityModal> GetInstructorActivityDetails(int InstructorId)
        {
            List<ActivityModal> lsActivityModal = null;
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetInstructorActivityDetails(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsActivityModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ActivityModal(
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<string>("ACTIVITY_MESSAGE")
                         
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsActivityModal;
        }
        public List<McqQuestion> GetMcqQuestionDetails(long AssignmentId)
        {
            List<McqQuestion> lsMcqQuestions = null;
            try
            {
                DataSet ds = objCPDataService.GetMcqAssignmentDetails(AssignmentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsMcqQuestions = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new McqQuestion(
                         dataRow.Field<long>("ID"),
                         dataRow.Field<string>("QUESTION_STATEMENT"),
                         dataRow.Field<string>("OPTION1"),
                         dataRow.Field<string>("OPTION2"),
                         dataRow.Field<string>("OPTION3"),
                         dataRow.Field<string>("OPTION4"),
                         dataRow.Field<byte>("CORRECT_ANSWER"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DATETIME").ToString("d MMM yyyy")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetMcqQuestionDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsMcqQuestions;
        }
        public bool DeleteTest(long TestId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.DeleteTest(TestId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool DeleteTestOfCourse(long TestId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.DeleteTestOfCourse(TestId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteTestOfCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool ActivateAssignment(long Assignmentid)
        {
            bool result = false;
            try
            {
                result = objCPDataService.ActivateAssignment(Assignmentid);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ActivateAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool DeleteAssignment(long AssignmentId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.DeleteAssignmentOfCourse(AssignmentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool DeleteIndependentAssignment(long AssignmentId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.DeleteInpependentAssignment(AssignmentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteIndependentAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool DeleteIndependentTest(long TestId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.DeleteIndepenetTest(TestId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteIndependentTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }

        public bool ActivateTest(long TestId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.ActivateTest(TestId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ActivateTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InserContatUsRequest(ContactUsApiRequest objContactUsApiRequest)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertContactFormDetails(objContactUsApiRequest.m_strName,objContactUsApiRequest.m_strEmail,
                                        objContactUsApiRequest.m_strPhoneNo,objContactUsApiRequest.m_strSubject,objContactUsApiRequest.m_strMessage);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InserContatUsRequest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool DeleteMcqAssignmentQuestion(long QuestionId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.DeleteMcqQuestionOfAssignment(QuestionId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ActivateTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool UpdateMcqAssignmentQuestion(McqQuestion objMcqQuestion)
        {
            bool result = false;
            try
            {
                result = objCPDataService.UpdateMcqQuestionOfAssignment(objMcqQuestion.m_llQuestionId,objMcqQuestion.m_strQuestionStatement,
                                   objMcqQuestion.m_strOption1,objMcqQuestion.m_strOption2,objMcqQuestion.m_strOption3,objMcqQuestion.m_strOption4,(byte)objMcqQuestion.m_iCorrectOption);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ActivateTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool DeleteIndexTopic(long TopicId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.DeleteIndexTopic(TopicId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteIndexTopic", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool UpdateIndexTopic(TopicModel objTopicModel)
        {
            bool result = false;
            try
            {
                result = objCPDataService.UpdateIndexTopic(objTopicModel.m_llTopicId,objTopicModel.m_strTopicName,objTopicModel.m_strTopicDescription,
                                objTopicModel.m_strFilePath,objTopicModel.m_iFileUploadTypeIdNew);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ActivateTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool DeleteCourseIndex(long IndexId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.DeleteIndex(IndexId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteCourseIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool UpdateCourseIndex(IndexModel objIndexModel)
        {
            bool result = false;
            try
            {
                result = objCPDataService.UpdateCourseIndexDetails(objIndexModel.m_llIndexId,objIndexModel.m_strIndexName,objIndexModel.m_strIndexDescription);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteCourseIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool UpdateFullCourseDetails(CourseDetailsModel objCourse)
        {
            bool result = false;
            try
            {
                result = objCPDataService.UpdateFullCourseDetails(objCourse.m_llCourseid,objCourse.m_strCourseName,objCourse.m_strCourseDescription);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateFullCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool UpdateTestDetails(TestDetailsModel objTestDetails)
        {
            bool result = false;
            try
            {
                result = objCPDataService.UpdateTestDetails(objTestDetails.m_llTestId,objTestDetails.m_strTestName,objTestDetails.m_strTestDescription,"",0);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool AddMcqTestQuestion(McqQuestion objMcqQuestion)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertNewMcqTestQuestion(objMcqQuestion.m_llTestId, objMcqQuestion.m_strQuestionStatement, objMcqQuestion.m_strOption1, objMcqQuestion.m_strOption2,
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
        public bool UpdateMcqTestQuestion(McqQuestion objMcqQuestion)
        {
            bool result = false;
            try
            {
                result = objCPDataService.UpdateMcqQuestionDetails(objMcqQuestion.m_llTestId, objMcqQuestion.m_strQuestionStatement, objMcqQuestion.m_strOption1, objMcqQuestion.m_strOption2,
                    objMcqQuestion.m_strOption3, objMcqQuestion.m_strOption4, (byte)objMcqQuestion.m_iCorrectOption, objMcqQuestion.m_iTimeInSeconds, objMcqQuestion.m_iMarks);
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
        public bool DeleteMcqTestQuestion(long QuestionId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.DeleteMcqTestQuestion(QuestionId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteMcqTestQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewTestToCourse(TestModel objTestModel)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertNewTestToCourse(objTestModel.m_strTestName, objTestModel.m_strTestDescription, (byte)Constants.TestQuestionType.MCQ, null,null,
                    objTestModel.m_llCourseId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewTestToCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewAssignmentToCourse(AssignmentModel objAssignmentModel)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertNewAssignmentToCourse(objAssignmentModel.m_strAssignmentName,objAssignmentModel.m_strAssignmentDescription,(byte)objAssignmentModel.m_iAssignmentType,
                                              null,null,objAssignmentModel.m_llCourseId);
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewAssignmentToCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public List<BasicAssignmentDetails> GetAssignmentForCourse(long CourseId)
        {
            List<BasicAssignmentDetails> lsAssignmentDetailsModel = null;
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetAllAssignmentsForCourse(CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsAssignmentDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new BasicAssignmentDetails(
                         dataRow.Field<long>("ASSIGNMENT_ID"),
                         dataRow.Field<string>("ASSIGNMENT_NAME")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAssignmentForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentDetailsModel;
        }
        public List<BasicTestDetails> GetTestOfCourse(long CourseId)
        {
            List<BasicTestDetails> lsBasicTestDetails = null;
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetTestsOfCourse(CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsBasicTestDetails = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new BasicTestDetails(
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<string>("TEST_NAME")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAssignmentForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsBasicTestDetails;
        }
        public bool InsertSubjectiveAssignmentQuestion(SubjectiveQuestion objSubjectiveQuestion)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertSubjectiveAssignmentQuestion(objSubjectiveQuestion.m_llAsssignmentId,
                    objSubjectiveQuestion.m_strQuestionStatement, objSubjectiveQuestion.m_strHint);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertSubjectiveAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool UpdateSubjectiveAssignmentQuestion(SubjectiveQuestion objSubjectiveQuestion)
        {
            bool result = false;
            try
            {
                result = objCPDataService.UpdateSubjectiveAssignmentQuestion(objSubjectiveQuestion.m_llQuestionId,
                                 objSubjectiveQuestion.m_strQuestionStatement, objSubjectiveQuestion.m_strHint);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateSubjectiveAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool DeleteSubjectiveAssignmentQuestion(long QuestionId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.DeleteSubjectiveAssignmentQuestion(QuestionId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteSubjectiveAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool DeleteSubjectiveAssignmentOfCourse(long AssignmentId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.DeleteSubjectiveAssignmentOfCourse(AssignmentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteSubjectiveAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public List<AssignmentSubmissionResponseModal> GetAllSubmissionsOfAnAssignment(long AssignmentId)
        {
            List<AssignmentSubmissionResponseModal> lsAssignmentSubmissionResponseModal = new List<AssignmentSubmissionResponseModal>();
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetAllAssignmentsSubmissions(AssignmentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    //  public AssignmentSubmissionResponseModal(string StudentName,long SubmissionId,long StudentId,string SubmissionDate,int PercentageScore)
                    lsAssignmentSubmissionResponseModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AssignmentSubmissionResponseModal(
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<long>("SUBMISSION_ID"),
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("ASSIGNMANT_PERCENTAGE_SCORE")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentSubmissionResponseModal;
        }
        public List<AssignmentSubmissionResponseModal> GetAllTestSubmissions(long TestId)
        {
            List<AssignmentSubmissionResponseModal> lsAssignmentSubmissionResponseModal = new List<AssignmentSubmissionResponseModal>();
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetAllTestSubmissions(TestId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    //  public AssignmentSubmissionResponseModal(string StudentName,long SubmissionId,long StudentId,string SubmissionDate,int PercentageScore)
                    lsAssignmentSubmissionResponseModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AssignmentSubmissionResponseModal(
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<long>("SUBMISSION_ID"),
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("TEST_SCORE")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentSubmissionResponseModal;
        }
        public List<CoursesJoinedResponseModal> GetAllStudentsJoinedToCourse(long CourseId)
        {
            List<CoursesJoinedResponseModal> lsCoursesJoinedResponseModal = new List<CoursesJoinedResponseModal>();
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetStudentJoinedToCourse(CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    //  public AssignmentSubmissionResponseModal(string StudentName,long SubmissionId,long StudentId,string SubmissionDate,int PercentageScore)
                    lsCoursesJoinedResponseModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CoursesJoinedResponseModal(
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<long>("STUDENT_ID")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsCoursesJoinedResponseModal;
        }
        public List<CoursesJoinedResponseModal> GetAllStudentsJoinedToInstructor(int InstructorId)
        {
            List<CoursesJoinedResponseModal> lsCoursesJoinedResponseModal = new List<CoursesJoinedResponseModal>();
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetAllStudentsJoinedToInstructor(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    //  public AssignmentSubmissionResponseModal(string StudentName,long SubmissionId,long StudentId,string SubmissionDate,int PercentageScore)
                    lsCoursesJoinedResponseModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CoursesJoinedResponseModal(
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<long>("STUDENT_ID")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsCoursesJoinedResponseModal;
        }
        public bool InsertNewCourseV2(InsertCourseV2Request objInsertCourseV2Request)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertNewCourseV2(objInsertCourseV2Request.m_strCourseName, objInsertCourseV2Request.m_strCourseDescription,
                             objInsertCourseV2Request.m_iInstructorId,objInsertCourseV2Request.m_strAboutCourse,objInsertCourseV2Request.m_strCourseDescription,
                             ref objInsertCourseV2Request.m_llCourseId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteSubjectiveAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewAlertForInstructor(int InstructorId, string AlertMessage,int AlertTypeId,long StudentId,long? EffectiveId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertNewAlertForInstructor(InstructorId, AlertMessage, AlertTypeId, EffectiveId, StudentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewAlertForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public List<AlertDetailsModal> GetAllAlertOfInstructor(int InstructorId)
        {
            List<AlertDetailsModal> lsAlertDetailsModal = new List<AlertDetailsModal>();
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetAllAlertForInstructor(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    //  public AssignmentSubmissionResponseModal(string StudentName,long SubmissionId,long StudentId,string SubmissionDate,int PercentageScore)
                    lsAlertDetailsModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AlertDetailsModal(
                         dataRow.Field<long>("ID"),
                          dataRow.Field<string>("ALERT_MESSAGE"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<string>("ALERT_TYPE_IOCN")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAlertDetailsModal;
        }
        public bool GetInstructorIdByCourseId(ref int InstructorId,long CourseId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.GetInstructorIdByCourseId(CourseId,ref InstructorId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewAlertForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool GetInstructorIdByAssignmentId(ref int InstructorId, long AssignmentId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.GetInstructorIdByAssignmentId(AssignmentId, ref InstructorId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorIdByAssignmentId", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool GetInstructorIdByTestId(ref int InstructorId, long TestId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.GetInstructorIdByTestId(TestId, ref InstructorId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorIdByTestId", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public List<AssignmentDetailsModel> SearchForAssignmentOfInstructor(string SearchString, int MaxRowToReturn,int InstructorId)
        {
            List<AssignmentDetailsModel> lsAssignmentDetailsModel = null;
            try
            {
                DataSet ds = objCPDataService.SearchForAssignmentOfInstructor(SearchString, MaxRowToReturn, InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsAssignmentDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AssignmentDetailsModel(
                         dataRow.Field<long>("ASSIGNMENT_ID"),
                         dataRow.Field<string>("ASSIGNMENT_NAME"),
                         dataRow.Field<string>("ASSIGNMENT_DESCRIPTION"),
                         dataRow.Field<byte>("ASSIGNMENT_TYPE"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_QUESTIONS"),
                          dataRow.Field<int>("NO_OF_SUBJECTIVE_QUESTIONS")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentDetailsModel;
        }
        public List<TestDetailsModel> SearchForTestOfInstructor(string SearchString, int MaxRowToReturn,int InstructorId)
        {
            List<TestDetailsModel> lsTestDetailsModel = null;
            try
            {
                DataSet ds = objCPDataService.SearchForTestOfInstructor(SearchString, MaxRowToReturn, InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsTestDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new TestDetailsModel(
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<string>("TEST_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_QUESTIONS"),
                         dataRow.Field<byte>("TEST_TYPE")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsTestDetailsModel;
        }
        public List<CourseDetailsModel> SearchForCourseOfInstructor(string SearchString, int MaxRowToReturn,int InstructorId)
        {
            List<CourseDetailsModel> lsCourseDetailsModel = null;
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.SearchForCourseOfInstructor(SearchString, MaxRowToReturn, InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsCourseDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CourseDetailsModel(
                         dataRow.Field<long>("COURSE_ID"),
                         dataRow.Field<string>("COURSE_NAME"),
                         dataRow.Field<string>("COURSE_DESCRIPTION"),
                         dataRow.Field<DateTime>("COURSE_ACTIVATION_DATETIME").ToString("d MMM yyyy")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsCourseDetailsModel;
        }
    }
}
 