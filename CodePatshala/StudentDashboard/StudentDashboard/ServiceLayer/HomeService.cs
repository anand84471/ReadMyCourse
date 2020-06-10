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
using System.Threading.Tasks;
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
        public async Task<bool> RegisterNewUser(StaeModel objRegisterModel )
        {
            bool result = false;
            try
            {
                string EncryptedPassword=SHA256Encryption.ComputeSha256Hash(objRegisterModel.strPassword);
                objRegisterModel.strPassword = EncryptedPassword;
                result =await objHomeDTO.RegisterNewUser(objRegisterModel);
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
        public async Task<bool> ValidateLoginDetails(StaeModel objRegisterModel)
        {
            bool result = false;
            try
            {
                objRegisterModel.strPassword = SHA256Encryption.ComputeSha256Hash(objRegisterModel.strPassword);
                result = await objHomeDTO.ValidateLogineDetails(objRegisterModel);
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
        public async Task<List<CourseDetailsModel>> GetAllCourseDetailsForInstructor(string InstructoruserName)
        {
            List<CourseDetailsModel> lsCouseModel = null;
            try
            {
                long IstructorId = 0;
                if(objHomeDTO.GetInstructorIdFromUserId(InstructoruserName, ref IstructorId))
                {
                    lsCouseModel=await objHomeDTO.GetAllCourseDetailsForInstructor(IstructorId);
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
        public async Task<bool> DeleteCourse(long CourseId)
        {
            bool result = false;
            try
            {
                result = await objHomeDTO.DeleteCourse(CourseId);
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
        public async Task<bool> DeleteMcqAssignmentQuestion(long QuestionId)
        {
            bool result = false;
            try
            {
                result = await objHomeDTO.DeleteMcqAssignmentQuestion(QuestionId);
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
        public async Task< bool> UpdateMcqAssignmentQuestion(McqQuestion objMcqQuestion)
        {
            bool result = false;
            try
            {
                result = await objHomeDTO.UpdateMcqAssignmentQuestion(objMcqQuestion);
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
        public  async Task<bool> ActivateCourse(long CourseId)
        {
            bool result = false;
            try
            {
                result = await objHomeDTO.ActivateCourse(CourseId);
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

        public async Task<GetCourseDetailsApiResponse> GetCourseDetails(long CourseId)
        {
            GetCourseDetailsApiResponse objGetCourseDetailsApiResponse = null;
            try
            {
                objGetCourseDetailsApiResponse = await objHomeDTO.GetCourseDetails(CourseId);
                if(objGetCourseDetailsApiResponse!=null)
                {
                    objGetCourseDetailsApiResponse.m_lsIndexes = await objHomeDTO.GetCourseIndexDetails(CourseId);
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
        public async Task<TestModel> GetTestDetails(long TestId)
        {
            TestModel objTestModel = null;
            try
            {
                objTestModel = await objHomeDTO.GetTestDetails(TestId);
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
        public async Task<bool> InsertNewTopic(TopicModel objTopicModel)
        {
            bool result = false;
            try
            {
                result = await objHomeDTO.InsertNewTopic(objTopicModel);
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
        public async Task<bool> InsertTopics(IndexModel objIndexModel)
        {
            bool result = false;
            int SuccessFullyInsertedTopics = 0;
            try
            {
                foreach (var objTopicModel in objIndexModel.m_lsTopicModel)
                {
                    objTopicModel.m_llIndexId = objIndexModel.m_llIndexId;
                    if(await objHomeDTO.InsertNewTopic(objTopicModel))
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
        public async Task<AboutCourseResponse> GetAboutCourse(int CourseId)
        {
            AboutCourseResponse objAboutCourseResponse = new AboutCourseResponse();
            try
            {
                GetCourseDetailsApiResponse objGetCourseDetailsApiResponse=await objHomeDTO.GetCourseDetails(CourseId);
                List<CourseIndexDetails> lsCourseIndexDetails= await objHomeDTO.GetCourseIndexDetails(CourseId);
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
                    List<BasicAssignmentDetails> lsAssignmentDetails =await  GetAssignmentForCourse(CourseId);
                    if (lsAssignmentDetails != null && lsAssignmentDetails.Count > 0) { objAboutCourseResponse.m_lsAssignmentDetails.AddRange(lsAssignmentDetails); }
                    List<BasicTestDetails> lsBasicTestDetails = await GetTestOfCourse(CourseId);
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
        public async Task<IndexModel> GetIndexDetails(int id)
        {
            IndexModel objIndexModel = null;
            try
            {
                objIndexModel = await objHomeDTO.GetIndexDetails(id);
                if(objIndexModel!=null)
                {
                    objIndexModel.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objIndexModel.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    objIndexModel.m_lsTopicModel = await objHomeDTO.GetIndexTopicDetails(id);
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
        public async Task<AssignmentModel> GetAssignmentDetails(long AssignmentId)
        {
            AssignmentModel objAssignmentModel = null;
            try
            {
                objAssignmentModel = await objHomeDTO.GetAssignmentDetails(AssignmentId);
                if (objAssignmentModel != null)
                {
                   switch(objAssignmentModel.m_iAssignmentType)
                    {
                        case (int)Constants.AssignmentQuestionType.MCQ:
                            {
                                objAssignmentModel.m_lsMcqQuestion =await  objHomeDTO.GetMcqQuestionDetails(AssignmentId);
                                break;
                            }
                        case (int)Constants.AssignmentQuestionType.SUBJECTIVE:
                            {
                                objAssignmentModel.m_lsSubjectiveQuestion = await objHomeDTO.GetAllQuestionsOfSubjectiveAssignment(AssignmentId);
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
        public async Task<TestModel> GetFullMcqTestDetails(long TestId)
        {
            TestModel objTestModel = null;
            try
            {
                objTestModel = await objHomeDTO.GetTestDetails(TestId);
                if (objTestModel != null)
                {
                    objTestModel.m_lsMcqQuestion = await objHomeDTO.GetMcqTestQuestions(TestId);  
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

        public async Task<bool> InsertNewMcqQuestion(McqQuestion objMcqQuestion)
        {
            return await objHomeDTO.InsertNewMcqTestQuestion(objMcqQuestion);
        }
        public async Task<bool> InsertNewMcqAssignmentQuestion(McqQuestion objMcqQuestion)
        {
            return await objHomeDTO.InsertNewMcqAssignmentQuestion(objMcqQuestion);
        }
        public async Task<bool> InsertAssignment(AssignmentModel objAssignmentmodel)
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
                    if (await objHomeDTO.InsertAssignmentIdToIndex(objAssignmentmodel.m_llAssignemntId, objAssignmentmodel.m_llIndexId))
                    {
                        if (objAssignmentmodel.m_iAssignmentType == (short)Constants.AssignmentQuestionType.MCQ)
                        {
                            foreach (var Questions in objAssignmentmodel.m_lsMcqQuestion)
                            {
                                Questions.m_llAssignmentId = objAssignmentmodel.m_llAssignemntId;
                                result =await objHomeDTO.InsertNewMcqAssignmentQuestion(Questions);
                            }
                        }
                        else if (objAssignmentmodel.m_iAssignmentType == (short)Constants.AssignmentQuestionType.SUBJECTIVE)
                        {
                            foreach (var Questions in objAssignmentmodel.m_lsSubjectiveQuestion)
                            {
                                Questions.m_llAsssignmentId = objAssignmentmodel.m_llAssignemntId;
                                result = await objHomeDTO.InsertSubjectiveAssignmentQuestion(Questions);
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
        public async Task<bool> InsertNewIndependentAssignment(AssignmentModel objAssignmentmodel)
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
                            result =await objHomeDTO.InsertNewMcqAssignmentQuestion(Questions);
                        }
                    }
                    else if (objAssignmentmodel.m_iAssignmentType == (short)Constants.AssignmentQuestionType.SUBJECTIVE)
                    {
                        foreach (var Questions in objAssignmentmodel.m_lsSubjectiveQuestion)
                        {
                            Questions.m_llAsssignmentId = objAssignmentmodel.m_llAssignemntId;
                            result = await objHomeDTO.InsertSubjectiveAssignmentQuestion(Questions);
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
        public async Task<bool> InsertTest(TestModel objTestmodel)
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
                    if (await objHomeDTO.InsertTestIdToIndex(objTestmodel.m_llTestId, objTestmodel.m_llIndexId))
                    {
                        if (objTestmodel.m_iTestType == (short)Constants.TestQuestionType.MCQ)
                        {
                            foreach (var Questions in objTestmodel.m_lsMcqQuestion)
                            {
                                Questions.m_llTestId = objTestmodel.m_llTestId;
                                result = await objHomeDTO.InsertNewMcqTestQuestion(Questions);
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
        public async Task< bool> InsertNewIndependentTest(TestModel objTestmodel)
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
                                result = await objHomeDTO.InsertNewMcqTestQuestion(Questions);
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

        public async Task<List<AssignmentDetailsModel>> GetAssignmentForInstructor(int InstructorId)
        {
            return await objHomeDTO.GetAssignmentForInstructor(InstructorId);
        }
        public async Task<List<TestDetailsModel>> GetInstructorTestDetails(int InstructorId)
        {
            return await objHomeDTO.GetInstructorTestDetails(InstructorId);
        }
        public async Task<bool> InsertActivityForInstructor(int InstructorId, string ActivityMessage)
        {
            return await objHomeDTO.InsertActivityForInstructor(InstructorId, ActivityMessage);
        }
        public async Task<bool> InsertNewCourse(CourseModel objCourseModel)
        {
            bool result = objHomeDTO.InsertNewCourse(objCourseModel);
            if(result)
            {
                long InstructorId=-1;
                objHomeDTO.GetInstructorIdFromUserId(objCourseModel.m_strInstructorUserName,ref InstructorId);
                if (InstructorId != -1)
                {
                    await InsertActivityForInstructor((int)InstructorId, objActivityManager.CreateActivityMessageForinstructor(objCourseModel.m_llCourseId, objCourseModel.m_strCourseName, (int)Constants.ActivityType.COURSE_CREATED));
                }
            }
            return result;
        }
        public async Task<List<ActivityModal>> GetInstructorActivityDetails(int InstructorId)
        {
            List<ActivityModal> lsActivityModal;
            lsActivityModal= await objHomeDTO.GetInstructorActivityDetails(InstructorId);
            if (lsActivityModal != null&& lsActivityModal.Count>0)
            {
                lsActivityModal = lsActivityModal.OrderByDescending((x) => x.m_dtDateTime).ToList();
            }
            return lsActivityModal;
        }
        public async Task<bool> ActivateTest(long TestId)
        {
            return await objHomeDTO.ActivateTest(TestId);
        }
        public async Task<bool> DeleteTest(long TestId)
        {
            return await objHomeDTO.DeleteTest(TestId);
        }
        public async Task<bool> DeleteTestOfCourse(long TestId)
        {
            return await objHomeDTO.DeleteTestOfCourse(TestId);
        }
        public async Task<bool> ActivateAssignment(long AssignmentId)
        {
            return await objHomeDTO.ActivateAssignment(AssignmentId);
        }
        public async Task<bool> DeleteAssignment(long AssignmentId)
        {
            return await objHomeDTO.DeleteAssignment(AssignmentId);
        }
        public async Task< bool> DeleteIndependentAssignment(long AssignmentId)
        {
            return await objHomeDTO.DeleteIndependentAssignment(AssignmentId);
        }
        public async Task<bool> DeleteIndependentTest(long TestId)
        {
            return await objHomeDTO.DeleteIndependentTest(TestId);
        }
        public async Task<bool> InserContatUsRequest(ContactUsApiRequest objContactUsApiRequest)
        {
            return await objHomeDTO.InserContatUsRequest(objContactUsApiRequest);
        }
        public async Task<bool> DeleteIndexTopic(long TopicId)
        {
            return await objHomeDTO.DeleteIndexTopic(TopicId);
        }
        public async Task<bool> UpdateIndexTopic(TopicModel objTopicModel)
        {
            return await objHomeDTO.UpdateIndexTopic(objTopicModel);
        }
        public async Task<bool> DeleteIndex(long IndexId)
        {
            return await objHomeDTO.DeleteCourseIndex(IndexId);
        }
        public async Task<bool> UpdateAssignmentDetails(AssignmentModel objAssignmentDetailsModel)
        {
            return await objHomeDTO.UpdateAssignmentDetails(objAssignmentDetailsModel);
        }
        public async Task<bool> UpdateCourseIndex(IndexModel objIndexModel)
        {
            return await objHomeDTO.UpdateCourseIndex(objIndexModel);
        }
        public async Task<bool> UpdateFullCourseDetails(CourseDetailsModel objCourse)
        {
            return await objHomeDTO.UpdateFullCourseDetails(objCourse);
        }
        public async Task<bool> UpdateTestDetails(TestDetailsModel objTestDetails)
        {
            return await objHomeDTO.UpdateTestDetails(objTestDetails);
        }
        public async Task<bool> AddMcqTestQuestion(McqQuestion objMcqQuestion)
        {
            return await objHomeDTO.AddMcqTestQuestion(objMcqQuestion);
        }
        public async Task<bool> UpdateMcqTestQuestion(McqQuestion objMcqQuestion)
        {
            return await objHomeDTO.UpdateMcqTestQuestion(objMcqQuestion);
        }
        public async Task<bool> InsertNewSeperateAssignmentToCourse(AssignmentModel objAssignmentModel)
        {
            return await objHomeDTO.InsertNewAssignmentToCourse(objAssignmentModel);
        }
        public async Task<bool> DeleteMcqTestQuestion(long QuestionId)
        {
            return await objHomeDTO.DeleteMcqTestQuestion(QuestionId);
        }
        public async Task<bool> InsertNewTestToCourse(TestModel objTestModel)
        {
            return await objHomeDTO.InsertNewTestToCourse(objTestModel);
        }
        public async Task<bool> InsertNewAssignmentToCourse(AssignmentModel objAssignmentModel)
        {
            return await objHomeDTO.InsertNewAssignmentToCourse(objAssignmentModel);
        }
        public async Task<List<BasicAssignmentDetails>> GetAssignmentForCourse(long CourseId)
        {
            return await objHomeDTO.GetAssignmentForCourse(CourseId);
        }
        public async Task<List<BasicTestDetails>> GetTestOfCourse(long CourseId)
        {
            return await objHomeDTO.GetTestOfCourse(CourseId);
        }
        public async Task<bool> InsertSubjectiveAssignmentQuestion(SubjectiveQuestion objSubjectiveQuestion)
        {
            return await objHomeDTO.InsertSubjectiveAssignmentQuestion(objSubjectiveQuestion);
        }
        public async Task<bool> UpdateSubjectiveAssignmentQuestion(SubjectiveQuestion objSubjectiveQuestion)
        {
            return await objHomeDTO.UpdateSubjectiveAssignmentQuestion(objSubjectiveQuestion);
        }
        public async Task<bool> DeleteSubjectiveAssignmentQuestion(long QuestionId)
        {
            return await objHomeDTO.DeleteSubjectiveAssignmentQuestion(QuestionId);
        }
        public async Task<bool> DeleteSubjectiveAssignmentOfCourse(long AssignmentId)
        {
            return await objHomeDTO.DeleteSubjectiveAssignmentOfCourse(AssignmentId);
        }
        public async Task<List<SubjectiveQuestion>> GetAllQuestionsOfSubjectiveAssignment(long AssignmentId)
        {
            return await objHomeDTO.GetAllQuestionsOfSubjectiveAssignment(AssignmentId);
        }
        public async Task<List<AssignmentSubmissionResponseModal>> GetAllSubmissionsOfAnAssignment(long AssignmentId)
        {
            return await objHomeDTO.GetAllSubmissionsOfAnAssignment(AssignmentId);
        }
        public async Task<List<AssignmentSubmissionResponseModal>> GetAllTestSubmissions(long TestId)
        {
            return await objHomeDTO.GetAllTestSubmissions(TestId);
        }
        public async Task<List<CoursesJoinedResponseModal>> GetAllStudentsJoinedToCourse(long CourseId)
        {
            return await objHomeDTO.GetAllStudentsJoinedToCourse(CourseId);
        }
        public async Task<List<CoursesJoinedResponseModal>> GetAllStudentsJoinedToInstructor(int InstructorId)
        {
            return await objHomeDTO.GetAllStudentsJoinedToInstructor(InstructorId);
        }
        public  bool InsertNewCourseV2(InsertCourseV2Request objInsertCourseV2Request)
        {
            return  objHomeDTO.InsertNewCourseV2(objInsertCourseV2Request);
        }
        public async Task<bool> InsertNewAlertForInstructor(int InstructorId, string AlertMessage, int AlertTypeId, long StudentId, long? EffectiveId)
        {
            return await objHomeDTO.InsertNewAlertForInstructor(InstructorId, AlertMessage, AlertTypeId, StudentId, EffectiveId);
        }
        public async Task<List<AlertDetailsModal>> GetAllAlertOfInstructor(int InstructorId)
        {
            return await objHomeDTO.GetAllAlertOfInstructor(InstructorId);
        }
        public bool GetInstructorIdByCourseId(ref int InstructorId, long CourseId)
        {
            return  objHomeDTO.GetInstructorIdByCourseId(ref InstructorId, CourseId);
        }
        public  bool GetInstructorIdByAssignmentId(ref int InstructorId, long AssignmentId)
        {
            return  objHomeDTO.GetInstructorIdByAssignmentId(ref InstructorId, AssignmentId);
        }
        public bool GetInstructorIdByTestId(ref int InstructorId, long TestId)
        {
            return  objHomeDTO.GetInstructorIdByTestId(ref InstructorId, TestId);
        }
        public async Task<List<AssignmentDetailsModel>> SearchForAssignmentOfInstructor(string SearchString, int MaxRowToReturn, int InstructorId)
        {
            return await objHomeDTO.SearchForAssignmentOfInstructor( SearchString,  MaxRowToReturn,  InstructorId);
        }
        public async Task<List<TestDetailsModel>> SearchForTestOfInstructor(string SearchString, int MaxRowToReturn, int InstructorId)
        {
            return await objHomeDTO.SearchForTestOfInstructor(SearchString, MaxRowToReturn, InstructorId);
        }
        public async Task<List<CourseDetailsModel>> SearchForCourseOfInstructor(string SearchString, int MaxRowToReturn, int InstructorId)
        {
            return await objHomeDTO.SearchForCourseOfInstructor(SearchString, MaxRowToReturn, InstructorId);
        }
        public async Task<InstructorSearchResponse> GetInstructorSearchDetails(InstructorSearchRequest objInstructorSearchRequest)
        {
            InstructorSearchResponse objInstructorSearchResponse = null;
            try
            {
                objInstructorSearchResponse = new InstructorSearchResponse();
                objInstructorSearchResponse.m_lsAssignments = await SearchForAssignmentOfInstructor(objInstructorSearchRequest.m_strSerachStraing,4,objInstructorSearchRequest.m_iInstructorId);
                objInstructorSearchResponse.m_lsCourses = await SearchForCourseOfInstructor(objInstructorSearchRequest.m_strSerachStraing, 4, objInstructorSearchRequest.m_iInstructorId);
                objInstructorSearchResponse.m_lsTestDetails = await SearchForTestOfInstructor(objInstructorSearchRequest.m_strSerachStraing, 4, objInstructorSearchRequest.m_iInstructorId);
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