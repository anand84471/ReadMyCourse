using StudentDashboard.DTO;
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

namespace StudentDashboard.ServiceLayer
{
    public class HomeService
    {
        HomeDTO objHomeDTO;
        StringBuilder m_strLogMessage = new StringBuilder();
        ActivityManager objActivityManager;
        public HomeService()
        {
            objHomeDTO = new HomeDTO();
            objActivityManager = new ActivityManager();
        }
        public bool RegisterNewUser(StaeModel objRegisterModel )
        {
            bool result = false;
            try
            {
                string EncryptedPassword=SHA256Encryption.ComputeSha256Hash(objRegisterModel.strPassword);
                objRegisterModel.strPassword = EncryptedPassword;
                result =objHomeDTO.RegisterNewUser(objRegisterModel);
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
        public bool ValidateLoginDetails(StaeModel objRegisterModel)
        {
            bool result = false;
            try
            {
                objRegisterModel.strPassword = SHA256Encryption.ComputeSha256Hash(objRegisterModel.strPassword);
                result = objHomeDTO.ValidateLogineDetails(objRegisterModel);
                if(result==false)
                {
                    m_strLogMessage.AppendFormat("Invalid login attemt userId={0}", objRegisterModel.strEmail);
                    MainLogger.Error(m_strLogMessage);
                }

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
        public List<CourseDetailsModel> GetAllCourseDetailsForInstructor(string InstructoruserName)
        {
            List<CourseDetailsModel> lsCouseModel = null;
            try
            {
                long IstructorId = 0;
                if(objHomeDTO.GetInstructorIdFromUserId(InstructoruserName, ref IstructorId))
                {
                    objHomeDTO.GetAllCourseDetailsForInstructor(IstructorId, ref lsCouseModel);
                }
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllCourseDetailsForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsCouseModel;
        }
        public bool DeleteCourse(long CourseId)
        {
            bool result = false;
            try
            {
                result = objHomeDTO.DeleteCourse(CourseId);
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteCourse", Ex.ToString());
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
                result = objHomeDTO.DeleteMcqAssignmentQuestion(QuestionId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteMcqAssignmentQuestion", Ex.ToString());
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
                result = objHomeDTO.UpdateMcqAssignmentQuestion(objMcqQuestion);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateMcqAssignmentQuestion", Ex.ToString());
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
                result = objHomeDTO.ActivateCourse(CourseId);
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

        public GetCourseDetailsApiResponse GetCourseDetails(long CourseId)
        {
            GetCourseDetailsApiResponse objGetCourseDetailsApiResponse = null;
            try
            {
                objGetCourseDetailsApiResponse = objHomeDTO.GetCourseDetails(CourseId);
                if(objGetCourseDetailsApiResponse!=null)
                {
                    objGetCourseDetailsApiResponse.m_lsIndexes = objHomeDTO.GetCourseIndexDetails(CourseId);
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
        public TestModel GetTestDetails(long TestId)
        {
            TestModel objTestModel = null;
            try
            {
                objTestModel = objHomeDTO.GetTestDetails(TestId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objTestModel;
        }
        public bool GetCourseDetails(int Id)
        {
            bool result = false;
            try
            {

            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewTopic(TopicModel objTopicModel)
        {
            bool result = false;
            try
            {
                result = objHomeDTO.InsertNewTopic(objTopicModel);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertTopics(IndexModel objIndexModel)
        {
            bool result = false;
            int SuccessFullyInsertedTopics = 0;
            try
            {
                foreach (var objTopicModel in objIndexModel.m_lsTopicModel)
                {
                    objTopicModel.m_llIndexId = objIndexModel.m_llIndexId;
                    if(objHomeDTO.InsertNewTopic(objTopicModel))
                    {
                        SuccessFullyInsertedTopics++;
                    }
                }
                if(SuccessFullyInsertedTopics== objIndexModel.m_lsTopicModel.Count)
                {
                    result = true;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertTopics", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public AboutCourseResponse GetAboutCourse(int CourseId)
        {
            AboutCourseResponse objAboutCourseResponse = new AboutCourseResponse();
            try
            {
                GetCourseDetailsApiResponse objGetCourseDetailsApiResponse=objHomeDTO.GetCourseDetails(CourseId);
                List<CourseIndexDetails> lsCourseIndexDetails= objHomeDTO.GetCourseIndexDetails(CourseId);
                if(lsCourseIndexDetails!=null&& objGetCourseDetailsApiResponse!=null)
                {
                    objAboutCourseResponse = new AboutCourseResponse(objGetCourseDetailsApiResponse.m_strCourseName,objGetCourseDetailsApiResponse.m_strCourseDescription,
                                        objGetCourseDetailsApiResponse.m_strCourseCreationDate,objGetCourseDetailsApiResponse.m_strCourseUpdationDate);
                    foreach (var indexes in lsCourseIndexDetails)
                    {
                        objAboutCourseResponse.AddIndex(indexes.m_strIndexName, indexes.m_llIndexId);
                        if (indexes.m_llAssignmentId != null) { objAboutCourseResponse.AddAssignment(indexes.m_strAssignmentName, indexes.m_llAssignmentId); }
                        if (indexes.m_llTestId != null) { objAboutCourseResponse.AddTest(indexes.m_strTestName, indexes.m_llTestId); }
                        objAboutCourseResponse.IncremetTopicCount(indexes.m_iTotalNoOfTopic);
                    }
                    List<BasicAssignmentDetails> lsAssignmentDetails = GetAssignmentForCourse(CourseId);
                    if (lsAssignmentDetails != null && lsAssignmentDetails.Count > 0) { objAboutCourseResponse.m_lsAssignmentDetails.AddRange(lsAssignmentDetails); }
                    List<BasicTestDetails> lsBasicTestDetails = GetTestOfCourse(CourseId);
                    if (lsBasicTestDetails != null && lsBasicTestDetails.Count > 0) { objAboutCourseResponse.m_lsTestDetails.AddRange(lsBasicTestDetails); }
                    objAboutCourseResponse.SetCounts();
                    objAboutCourseResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objAboutCourseResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                
                
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertTopics", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objAboutCourseResponse;
        }
        public IndexModel GetIndexDetails(int id)
        {
            IndexModel objIndexModel = null;
            try
            {
                objIndexModel = objHomeDTO.GetIndexDetails(id);
                if(objIndexModel!=null)
                {
                    objIndexModel.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objIndexModel.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    objIndexModel.m_lsTopicModel = objHomeDTO.GetIndexTopicDetails(id);
                }
                else
                {
                    objIndexModel.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objIndexModel.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignment", Ex.ToString());
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
                objAssignmentModel = objHomeDTO.GetAssignmentDetails(AssignmentId);
                if (objAssignmentModel != null)
                {
                   switch(objAssignmentModel.m_iAssignmentType)
                    {
                        case (int)Constants.AssignmentQuestionType.MCQ:
                            {
                                objAssignmentModel.m_lsMcqQuestion = objHomeDTO.GetMcqQuestionDetails(AssignmentId);
                                break;
                            }
                        case (int)Constants.AssignmentQuestionType.SUBJECTIVE:
                            {
                                objAssignmentModel.m_lsSubjectiveQuestion = objHomeDTO.GetAllQuestionsOfSubjectiveAssignment(AssignmentId);
                                break;
                            }
                    }
                    objAssignmentModel.SetRemaningValues();
                }
                else
                {
                  
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objAssignmentModel;
        }
        public TestModel GetFullMcqTestDetails(long TestId)
        {
            TestModel objTestModel = null;
            try
            {
                objTestModel = objHomeDTO.GetTestDetails(TestId);
                if (objTestModel != null)
                {
                    objTestModel.m_lsMcqQuestion = objHomeDTO.GetMcqTestQuestions(TestId);  
                }
                else
                {

                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetFullTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objTestModel;
        }

        public bool InsertNewMcqQuestion(McqQuestion objMcqQuestion)
        {
            return objHomeDTO.InsertNewMcqTestQuestion(objMcqQuestion);
        }
        public bool InsertNewMcqAssignmentQuestion(McqQuestion objMcqQuestion)
        {
            return objHomeDTO.InsertNewMcqAssignmentQuestion(objMcqQuestion);
        }
        public bool InsertAssignment(AssignmentModel objAssignmentmodel)
        {
            bool result = false;
            try {
                if (objAssignmentmodel.m_strAssignmentType.Equals(Constants.ASSIGNMENT_TYPE_MCQ))
                {
                    objAssignmentmodel.m_iAssignmentType = (short)Constants.AssignmentQuestionType.MCQ;
                }
                else if (objAssignmentmodel.m_strAssignmentType.Equals(Constants.ASSIGNMENT_TYPE_SUBJECTIVE))
                {
                    objAssignmentmodel.m_iAssignmentType = (short)Constants.AssignmentQuestionType.SUBJECTIVE;
                }
                if (objHomeDTO.InsertNewAssignment(objAssignmentmodel))
                {
                    if (objHomeDTO.InsertAssignmentIdToIndex(objAssignmentmodel.m_llAssignemntId, objAssignmentmodel.m_llIndexId))
                    {
                        if (objAssignmentmodel.m_iAssignmentType == (short)Constants.AssignmentQuestionType.MCQ)
                        {
                            foreach (var Questions in objAssignmentmodel.m_lsMcqQuestion)
                            {
                                Questions.m_llAssignmentId = objAssignmentmodel.m_llAssignemntId;
                                result = objHomeDTO.InsertNewMcqAssignmentQuestion(Questions);
                            }
                        }
                        else if (objAssignmentmodel.m_iAssignmentType == (short)Constants.AssignmentQuestionType.SUBJECTIVE)
                        {
                            foreach (var Questions in objAssignmentmodel.m_lsSubjectiveQuestion)
                            {
                                Questions.m_llAsssignmentId = objAssignmentmodel.m_llAssignemntId;
                                result = objHomeDTO.InsertSubjectiveAssignmentQuestion(Questions);
                            }
                        }
                    }
                }
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewIndependentAssignment(AssignmentModel objAssignmentmodel)
        {
            bool result = false;
            try
            {
                if (objAssignmentmodel.m_strAssignmentType.Equals(Constants.ASSIGNMENT_TYPE_MCQ))
                {
                    objAssignmentmodel.m_iAssignmentType = (short)Constants.AssignmentQuestionType.MCQ;
                }
                else if (objAssignmentmodel.m_strAssignmentType.Equals(Constants.ASSIGNMENT_TYPE_SUBJECTIVE))
                {
                    objAssignmentmodel.m_iAssignmentType = (short)Constants.AssignmentQuestionType.SUBJECTIVE;
                }
                if (objHomeDTO.InsertNewIndependentAssignment(objAssignmentmodel))
                {
                    if (objAssignmentmodel.m_iAssignmentType == (short)Constants.AssignmentQuestionType.MCQ)
                    {
                        foreach (var Questions in objAssignmentmodel.m_lsMcqQuestion)
                        {
                            Questions.m_llAssignmentId = objAssignmentmodel.m_llAssignemntId;
                            result = objHomeDTO.InsertNewMcqAssignmentQuestion(Questions);
                        }
                    }
                    else if (objAssignmentmodel.m_iAssignmentType == (short)Constants.AssignmentQuestionType.SUBJECTIVE)
                    {
                        foreach (var Questions in objAssignmentmodel.m_lsSubjectiveQuestion)
                        {
                            Questions.m_llAsssignmentId = objAssignmentmodel.m_llAssignemntId;
                            result = objHomeDTO.InsertSubjectiveAssignmentQuestion(Questions);
                        }
                    }
                }
                if(result)
                {
                    InsertActivityForInstructor(objAssignmentmodel.m_iInstructorId, objActivityManager.CreateActivityMessageForinstructor(objAssignmentmodel.m_llAssignemntId,objAssignmentmodel.m_strAssignmentName, (int)Constants.ActivityType.ASSIGNMENT_CREATED));
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewIndependentAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertTest(TestModel objTestmodel)
        {
            bool result = false;
            try
            {
                if (objTestmodel.m_strTestType.Equals(Constants.TEST_TYPE_MCQ))
                {
                    objTestmodel.m_iTestType = (short)Constants.TestQuestionType.MCQ;
                }
                else if (objTestmodel.m_strTestType.Equals(Constants.TEST_TYPE_SUBJECTIVE))
                {
                    objTestmodel.m_iTestType = (short)Constants.TestQuestionType.SUBJECTIVE;
                }
                if (objHomeDTO.InsertNewTest(objTestmodel))
                {
                    if (objHomeDTO.InsertTestIdToIndex(objTestmodel.m_llTestId, objTestmodel.m_llIndexId))
                    {
                        if (objTestmodel.m_iTestType == (short)Constants.TestQuestionType.MCQ)
                        {
                            foreach (var Questions in objTestmodel.m_lsMcqQuestion)
                            {
                                Questions.m_llTestId = objTestmodel.m_llTestId;
                                result = objHomeDTO.InsertNewMcqTestQuestion(Questions);
                            }
                        }
                        else if (objTestmodel.m_iTestType == (short)Constants.TestQuestionType.SUBJECTIVE)
                        {
                            foreach (var Questions in objTestmodel.m_lsMcqQuestion)
                            {

                            }
                        }

                    }
                }
                if(result)
                {
                    InsertActivityForInstructor(objTestmodel.m_iInstructorId, objActivityManager.CreateActivityMessageForinstructor(objTestmodel.m_llTestId,objTestmodel.m_strTestName,(int)Constants.ActivityType.TEST_CREATED));
                }
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewIndependentTest(TestModel objTestmodel)
        {
            bool result = false;
            try
            {
                if (objTestmodel.m_strTestType.Equals(Constants.TEST_TYPE_MCQ))
                {
                    objTestmodel.m_iTestType = (short)Constants.TestQuestionType.MCQ;
                }
                else if (objTestmodel.m_strTestType.Equals(Constants.TEST_TYPE_SUBJECTIVE))
                {
                    objTestmodel.m_iTestType = (short)Constants.TestQuestionType.SUBJECTIVE;
                }
                if (objHomeDTO.InsertNewIndependentTest(objTestmodel))
                {
                  
                        if (objTestmodel.m_iTestType == (short)Constants.TestQuestionType.MCQ)
                        {
                            foreach (var Questions in objTestmodel.m_lsMcqQuestion)
                            {
                                Questions.m_llTestId = objTestmodel.m_llTestId;
                                result = objHomeDTO.InsertNewMcqTestQuestion(Questions);
                            }
                        }
                        else if (objTestmodel.m_iTestType == (short)Constants.TestQuestionType.SUBJECTIVE)
                        {
                            foreach (var Questions in objTestmodel.m_lsMcqQuestion)
                            {

                            }
                        }

                    
                }
                if(result)
                {
                    InsertActivityForInstructor(objTestmodel.m_iInstructorId, objActivityManager.CreateActivityMessageForinstructor(objTestmodel.m_llTestId, objTestmodel.m_strTestName, (int)Constants.ActivityType.TEST_CREATED));
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }

        public List<AssignmentDetailsModel> GetAssignmentForInstructor(int InstructorId)
        {
            return objHomeDTO.GetAssignmentForInstructor(InstructorId);
        }
        public List<TestDetailsModel> GetInstructorTestDetails(int InstructorId)
        {
            return objHomeDTO.GetInstructorTestDetails(InstructorId);
        }
        public bool InsertActivityForInstructor(int InstructorId, string ActivityMessage)
        {
            return objHomeDTO.InsertActivityForInstructor(InstructorId, ActivityMessage);
        }
        public bool InsertNewCourse(CourseModel objCourseModel)
        {
            bool result = objHomeDTO.InsertNewCourse(objCourseModel);
            if(result)
            {
                long InstructorId=-1;
                objHomeDTO.GetInstructorIdFromUserId(objCourseModel.m_strInstructorUserName,ref InstructorId);
                if (InstructorId != -1)
                {
                    InsertActivityForInstructor((int)InstructorId, objActivityManager.CreateActivityMessageForinstructor(objCourseModel.m_llCourseId, objCourseModel.m_strCourseName, (int)Constants.ActivityType.COURSE_CREATED));
                }
            }
            return result;
        }
        public List<ActivityModal> GetInstructorActivityDetails(int InstructorId)
        {
            return objHomeDTO.GetInstructorActivityDetails(InstructorId).OrderByDescending((x)=>x.m_dtDateTime).ToList();
        }
        public bool ActivateTest(long TestId)
        {
            return objHomeDTO.ActivateTest(TestId);
        }
        public bool DeleteTest(long TestId)
        {
            return objHomeDTO.DeleteTest(TestId);
        }
        public bool DeleteTestOfCourse(long TestId)
        {
            return objHomeDTO.DeleteTestOfCourse(TestId);
        }
        public bool ActivateAssignment(long AssignmentId)
        {
            return objHomeDTO.ActivateAssignment(AssignmentId);
        }
        public bool DeleteAssignment(long AssignmentId)
        {
            return objHomeDTO.DeleteAssignment(AssignmentId);
        }
        public bool DeleteIndependentAssignment(long AssignmentId)
        {
            return objHomeDTO.DeleteIndependentAssignment(AssignmentId);
        }
        public bool DeleteIndependentTest(long TestId)
        {
            return objHomeDTO.DeleteIndependentTest(TestId);
        }
        public bool InserContatUsRequest(ContactUsApiRequest objContactUsApiRequest)
        {
            return objHomeDTO.InserContatUsRequest(objContactUsApiRequest);
        }
        public bool DeleteIndexTopic(long TopicId)
        {
            return objHomeDTO.DeleteIndexTopic(TopicId);
        }
        public bool UpdateIndexTopic(TopicModel objTopicModel)
        {
            return objHomeDTO.UpdateIndexTopic(objTopicModel);
        }
        public bool DeleteIndex(long IndexId)
        {
            return objHomeDTO.DeleteCourseIndex(IndexId);
        }
        public bool UpdateAssignmentDetails(AssignmentModel objAssignmentDetailsModel)
        {
            return objHomeDTO.UpdateAssignmentDetails(objAssignmentDetailsModel);
        }
        public bool UpdateCourseIndex(IndexModel objIndexModel)
        {
            return objHomeDTO.UpdateCourseIndex(objIndexModel);
        }
        public bool UpdateFullCourseDetails(CourseDetailsModel objCourse)
        {
            return objHomeDTO.UpdateFullCourseDetails(objCourse);
        }
        public bool UpdateTestDetails(TestDetailsModel objTestDetails)
        {
            return objHomeDTO.UpdateTestDetails(objTestDetails);
        }
        public bool AddMcqTestQuestion(McqQuestion objMcqQuestion)
        {
            return objHomeDTO.AddMcqTestQuestion(objMcqQuestion);
        }
        public bool UpdateMcqTestQuestion(McqQuestion objMcqQuestion)
        {
            return objHomeDTO.UpdateMcqTestQuestion(objMcqQuestion);
        }
        public bool InsertNewSeperateAssignmentToCourse(AssignmentModel objAssignmentModel)
        {
            return objHomeDTO.InsertNewAssignmentToCourse(objAssignmentModel);
        }
        public bool DeleteMcqTestQuestion(long QuestionId)
        {
            return objHomeDTO.DeleteMcqTestQuestion(QuestionId);
        }
        public bool InsertNewTestToCourse(TestModel objTestModel)
        {
            return objHomeDTO.InsertNewTestToCourse(objTestModel);
        }
        public bool InsertNewAssignmentToCourse(AssignmentModel objAssignmentModel)
        {
            return objHomeDTO.InsertNewAssignmentToCourse(objAssignmentModel);
        }
        public List<BasicAssignmentDetails> GetAssignmentForCourse(long CourseId)
        {
            return objHomeDTO.GetAssignmentForCourse(CourseId);
        }
        public List<BasicTestDetails> GetTestOfCourse(long CourseId)
        {
            return objHomeDTO.GetTestOfCourse(CourseId);
        }
        public bool InsertSubjectiveAssignmentQuestion(SubjectiveQuestion objSubjectiveQuestion)
        {
            return objHomeDTO.InsertSubjectiveAssignmentQuestion(objSubjectiveQuestion);
        }
        public bool UpdateSubjectiveAssignmentQuestion(SubjectiveQuestion objSubjectiveQuestion)
        {
            return objHomeDTO.UpdateSubjectiveAssignmentQuestion(objSubjectiveQuestion);
        }
        public bool DeleteSubjectiveAssignmentQuestion(long QuestionId)
        {
            return objHomeDTO.DeleteSubjectiveAssignmentQuestion(QuestionId);
        }
        public bool DeleteSubjectiveAssignmentOfCourse(long AssignmentId)
        {
            return objHomeDTO.DeleteSubjectiveAssignmentOfCourse(AssignmentId);
        }
        public List<SubjectiveQuestion> GetAllQuestionsOfSubjectiveAssignment(long AssignmentId)
        {
            return objHomeDTO.GetAllQuestionsOfSubjectiveAssignment(AssignmentId);
        }
        public List<AssignmentSubmissionResponseModal> GetAllSubmissionsOfAnAssignment(long AssignmentId)
        {
            return objHomeDTO.GetAllSubmissionsOfAnAssignment(AssignmentId);
        }
        public List<AssignmentSubmissionResponseModal> GetAllTestSubmissions(long TestId)
        {
            return objHomeDTO.GetAllTestSubmissions(TestId);
        }
        public List<CoursesJoinedResponseModal> GetAllStudentsJoinedToCourse(long CourseId)
        {
            return objHomeDTO.GetAllStudentsJoinedToCourse(CourseId);
        }
        public List<CoursesJoinedResponseModal> GetAllStudentsJoinedToInstructor(int InstructorId)
        {
            return objHomeDTO.GetAllStudentsJoinedToInstructor(InstructorId);
        }
        public bool InsertNewCourseV2(InsertCourseV2Request objInsertCourseV2Request)
        {
            return objHomeDTO.InsertNewCourseV2(objInsertCourseV2Request);
        }
        public bool InsertNewAlertForInstructor(int InstructorId, string AlertMessage, int AlertTypeId, long StudentId, long? EffectiveId)
        {
            return objHomeDTO.InsertNewAlertForInstructor(InstructorId, AlertMessage, AlertTypeId, StudentId, EffectiveId);
        }
        public List<AlertDetailsModal> GetAllAlertOfInstructor(int InstructorId)
        {
            return objHomeDTO.GetAllAlertOfInstructor(InstructorId);
        }
        public bool GetInstructorIdByCourseId(ref int InstructorId, long CourseId)
        {
            return objHomeDTO.GetInstructorIdByCourseId(ref InstructorId, CourseId);
        }
        public bool GetInstructorIdByAssignmentId(ref int InstructorId, long AssignmentId)
        {
            return objHomeDTO.GetInstructorIdByAssignmentId(ref InstructorId, AssignmentId);
        }
        public bool GetInstructorIdByTestId(ref int InstructorId, long TestId)
        {
            return objHomeDTO.GetInstructorIdByTestId(ref InstructorId, TestId);
        }
        public List<AssignmentDetailsModel> SearchForAssignmentOfInstructor(string SearchString, int MaxRowToReturn, int InstructorId)
        {
            return objHomeDTO.SearchForAssignmentOfInstructor( SearchString,  MaxRowToReturn,  InstructorId);
        }
        public List<TestDetailsModel> SearchForTestOfInstructor(string SearchString, int MaxRowToReturn, int InstructorId)
        {
            return objHomeDTO.SearchForTestOfInstructor(SearchString, MaxRowToReturn, InstructorId);
        }
        public List<CourseDetailsModel> SearchForCourseOfInstructor(string SearchString, int MaxRowToReturn, int InstructorId)
        {
            return objHomeDTO.SearchForCourseOfInstructor(SearchString, MaxRowToReturn, InstructorId);
        }
        public InstructorSearchResponse GetInstructorSearchDetails(InstructorSearchRequest objInstructorSearchRequest)
        {
            InstructorSearchResponse objInstructorSearchResponse = null;
            try
            {
                objInstructorSearchResponse = new InstructorSearchResponse();
                objInstructorSearchResponse.m_lsAssignments = SearchForAssignmentOfInstructor(objInstructorSearchRequest.m_strSerachStraing,4,objInstructorSearchRequest.m_iInstructorId);
                objInstructorSearchResponse.m_lsCourses = SearchForCourseOfInstructor(objInstructorSearchRequest.m_strSerachStraing, 4, objInstructorSearchRequest.m_iInstructorId);
                objInstructorSearchResponse.m_lsTestDetails = SearchForTestOfInstructor(objInstructorSearchRequest.m_strSerachStraing, 4, objInstructorSearchRequest.m_iInstructorId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorSearchDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objInstructorSearchResponse;
        }
    }
}