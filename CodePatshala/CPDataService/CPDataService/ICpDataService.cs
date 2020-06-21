using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CPDataService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICpDataService
    {
        [OperationContract]
        string GetData(int value);
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
        [OperationContract]
        bool RegisterNewUser(string FirstName, string LastName, string PhoneNo, string Email, string Password);
        [OperationContract]
        DataSet ValidateLoginDetails(string Email, string Password);
        [OperationContract]
        bool RegisterNewInstructor(string FirstName, string LastName, string PhoneNo, string Email, string Password,
            string PhoneNoVerificationGuid, string EmailIdVerificationGuid);
        [OperationContract]
        DataSet ValidateInstructorLoginDetails(string Email, string Password);
        [OperationContract]
        DataSet GetAllCountryDetails();
        [OperationContract]
        DataSet GetAllCityDetailsOfState(int StateId);
        [OperationContract]
        DataSet GetAllStateDetailsOfCountry(int CountryId);
        [OperationContract]
        DataSet CheckIsSchoolUserIdAlreadyExists(string SchoolUserIdToCheck);
        [OperationContract]
        bool InsertNewSchoolDetails(string SchoolName, string AddressLine1, string AddressLine2, int CityId, int PinCode, string SchoolUserId,
                                          string Password, string PhoneNo, string Email);
        [OperationContract]
        DataSet ValidateSchoolLoginDetails(string Email, string Password);
        [OperationContract]
        bool InsertNewCourse(string CourseName, string CourseDescription, string InstructorId, ref long CourseId);
        [OperationContract]
        bool InertNewCourseIndex(string IndexName, string IndexDescription, long CourseId, ref long IndexId);
        [OperationContract]
        bool InsertNewTopic(string TopicName, string TopicDescription, string FileUploadPath, short FileUploadTypeId, long IndexId);
        [OperationContract]
        bool InsertNewTest(string TestName, string TestDescription, string FilePath, short FileTypeId, short TestType, ref long TestId);
        [OperationContract]
        bool InsertNewAssignment(string AssignmentName, string FilePath, short FileTypeId, short AssignmentType, string AssignmentDescription, ref long AssignmentId);
        [OperationContract]
        bool InsertNewMcqAssignment(long AssignmentId, string QuestionStatement, string Option1, string Option2, string Option3, string Option4, short CorrectOption);
        [OperationContract]
        bool InsertNewMcqTestQuestion(long TestId, string QuestionStatement, string Option1, string Option2, string Option3, string Option4, short CorrectOption,
                                int TimeForQuestionInSeconds, int Marks);
        [OperationContract]
        bool InsertTestIdToIndex(long TestId, long IndexId);
        [OperationContract]
        bool InsertAssignmentIdToIndex(long AssignmentId, long IndexId);
        [OperationContract]
        DataSet GetAllCourse(long InstructorId);
        [OperationContract]
        bool GetInstructorIdFromUserId(string InstructorId, ref long Id);
        [OperationContract]
        DataSet GetIndexDetailsOfCourse(long CourseId);
        [OperationContract]
        DataSet GetCourseDetails(long CourseId);
        [OperationContract]
        bool InsertNewIndependentAssignment(int InstructorId, string AssignmentName, string AssignmentDescription, string FilePath, short FileTypeId, short AssignmentType, ref long AssignmentId);
        [OperationContract]
        bool InsertActivityForInstructor(int InstructorId, string ActivityMessgae);
        [OperationContract]
        bool UpdateTestDetails(long TestId, string TestName, string TestDescription, string FilePath, byte FileType);
        [OperationContract]
        bool UpdateAssignmentDetails(long AssignmentId, string AssignmentName, string AssignmentDescription, string FilePath, byte FileType);
        [OperationContract]
        bool UpdateIndexTopicDetails(long TopicId, string TopicName, string TopicDescription, string FilePath, byte FileType);
        [OperationContract]
        bool UpdateCourseIndexDetails(long IndexId, string IndexName, string IndexDescription);
        [OperationContract]
        bool UpdateCourseDetails(long CourseId, string CourseDescription);
        [OperationContract]
        bool ActivateCourse(long CourseId);
        [OperationContract]
        bool DeleteCourse(long CourseId);
        [OperationContract]
        DataSet GetAssignmentDetails(long AssignmentId);
        [OperationContract]
        DataSet GetMcqAssignmentDetails(long AssignmentId);
        [OperationContract]
        DataSet GetInstructorDetails(int Id);
        [OperationContract]
        bool UpdateInstructorDetails(string FirstName, string LastName, string PhoneNo, string Gender, string AddressLine1, string AddressLine2,
                                          int CityId, int StateId, string PinCode, int InstructorId);
        [OperationContract]
        bool UpdateInstructorPassword(string Password, int InstructorId);
        [OperationContract]
        DataSet GetInstructorPostLoginDetails(int Id);
        [OperationContract]
        bool InsertNewIndependentTest(int InstructorId, string TestName, string TestDescription, string FilePath, short FileTypeId, short TestType, ref long TestId);
        [OperationContract]
        DataSet GetInstructorTestDetails(int Id);
        [OperationContract]
        DataSet GetInstructorAssignmentDetails(int Id);
        [OperationContract]
        DataSet GetInstructorActivityDetails(int Id);
        [OperationContract]
        bool ActivateTest(long TestId, string ShareCode, string TinyUrl);
        [OperationContract]
        bool DeleteAssignmentOfCourse(long AssignmentId);
        [OperationContract]
        bool ActivateAssignment(long AssignmentId, string ShareCode, string TinyUrl);
        [OperationContract]
        bool DeleteTest(long TestId);
        [OperationContract]
        bool InsertContactFormDetails(string Name, string Email, string PhoneNo, string Subject, string Message);
        [OperationContract]
        DataSet GetIndexTopicDetails(long IndexId);
        [OperationContract]
        DataSet GetIndexDetails(long IndexId);
        [OperationContract]
        bool DeleteMcqQuestionOfAssignment(long QuestionId);
        [OperationContract]
        bool UpdateMcqQuestionOfAssignment(long QuestionId, string QuestionStatement, string Option1, string Option2, string Option3, string Option4, byte CorrectOption);
        [OperationContract]
        bool DeleteIndexTopic(long TopicId);
        [OperationContract]
        bool UpdateIndexTopic(long TopicId, string TopicName, string TopicDescription, string FilePathMapToServer, byte? FileTypeId);
        [OperationContract]
        bool UpdateFullCourseDetails(long CourseId, string CourseName, string CourseDescription);
        [OperationContract]
        bool DeleteIndex(long IndexId);
        [OperationContract]
        DataSet GetMcqTestDetails(long TestId);
        [OperationContract]
        DataSet GetMcqtestQuestionDetails(long TestId);
        [OperationContract]
        bool UpdateMcqTestDetails(long TestId, string TestName, string TestDescroption);
        [OperationContract]
        bool UpdateMcqQuestionDetails(long Questionid, string QuestionStatement, string Option1, string Option2, string Option3, string Option4,
                 byte CorrectOption, int iTimeForQuestion, int iMarksForQuestion);
        [OperationContract]
        bool DeleteMcqTest(long TestId);
        [OperationContract]
        bool InsertNewAssignmentToCourse(string AssignmentName, string AssignmentDescription, byte AssignmentTypeId, string FilePath, byte? FileTypeId,
                  long CourseId);
        [OperationContract]
        bool InsertNewTestToCourse(string TestName, string TestDescription, byte TestTypeId, string FilePath, byte? FileTypeId,
                    long CourseId);
        [OperationContract]
        bool DeleteMcqTestQuestion(long QuestionId);
        [OperationContract]
        DataSet GetAllAssignmentsForCourse(long CourseId);
        [OperationContract]
        bool DeleteTestOfCourse(long TestId);
        [OperationContract]
        bool DeleteInpependentAssignment(long AssignmentId);
        [OperationContract]
        bool DeleteIndepenetTest(long TestId);
        [OperationContract]
        bool InsertSubjectiveAssignmentQuestion(long AssignmentId, string QuestionStatement, string Hint);
        [OperationContract]
        bool DeleteSubjectiveAssignmentQuestion(long AssignmentId);
        [OperationContract]
        bool DeleteSubjectiveAssignmentOfCourse(long AssignmentId);
        [OperationContract]
        bool UpdateSubjectiveAssignmentQuestion(long QuestionId, string QuestionStatement, string Hint);
        [OperationContract]
        DataSet GetAllQuestionsOfSubjectiveAssignment(long AssignmentId);
        [OperationContract]
        bool RegisterNewStudent(string FirstName, string LastName, string UserId, string HashedPassword, string PhoneNo,
            string PhoneNoVerificationGuid, string EmailIdVerificationGuid);
        [OperationContract]
        bool ValidateStudentLogin(string UserId, string HashedPassword, ref long StudentId);
        [OperationContract]
        bool InsertActivityForStudent(string ActivityMessage, long StudentId);
        [OperationContract]
        bool JoinStudentToCourse(long CourseId, long StudentId);
        [OperationContract]
        DataSet SearchForCourse(string SerachString, int MaxRowToReturn, int NoOfRowsFetch, int SortType);
        [OperationContract]
        DataSet GetStudentDetails(long StudentId);
        [OperationContract]
        bool GetStudentIdFromUserId(string UserId, ref long StudentId);
        [OperationContract]
        bool UpdateStudentDetails(string FirstName, string LastName, string AddressLine1, string AddressLine2, string PinCode,
                                          int StateId, int CityId, string Gender);
        [OperationContract]
        bool UpdateStudentPassword(long StudentId, string OldHashedPassword, string NewHashedPassword);
        [OperationContract]
        DataSet SearchForTest(string SerachString, int MaxRowToReturn);
        [OperationContract]
        DataSet SearchForAssignment(string SerachString, int MaxRowToReturn);
        [OperationContract]
        DataSet SearchForInstructor(string SerachString, int MaxRowToReturn);
        [OperationContract]
        DataSet GetJoinedCoursesForStudent(long StudentId, string SearchString, int MaxRowCountToReturn);
        [OperationContract]
        bool JoinStudentToInstructor(long StudentId, int InstructorId);
        [OperationContract]
        DataSet GetAllJoinedInstructorForStudent(long StudentId, string SearchString, int MaxRowCount);
        [OperationContract]
        bool InsertAssignmentResponse(long StudentId, long AssignmentId, DateTime AssignmentStartTime, DateTime AssignmentFinishTime, string Response,
                     int PercentageScore, int TotalNoOfQuestions, ref long SubmissionId);
        [OperationContract]
        bool InsertAssignmentFeedback(long SubmissionId, string FeedBack, int Rating);
        [OperationContract]
        DataSet GetAllAssignmentSubmissionsForStudent(long StudentId);
        [OperationContract]
        DataSet GetAssignmentResponse(long SubmissionId, long StudentId);
        [OperationContract]
        bool InsertTestResponse(long StudentId, long TestId, DateTime TestStartTime, DateTime TestFinishTime, string Response,
                    int PercentageScore, int TotalNoOfQuestions, ref long SubmissionId);
        [OperationContract]
        DataSet GetTestResponse(long SubmissionId, long StudentId);
        [OperationContract]
        DataSet GetAllTestSubmissionsForStudent(long StudentId);
        [OperationContract]
        DataSet GetStudentHomeDetails(long StudentId);
        [OperationContract]
        DataSet GetTestsOfCourse(long CourseId);
        [OperationContract]
        DataSet GetAllTestSubmissions(long StudentId);
        [OperationContract]
        DataSet GetAllAssignmentsSubmissions(long AssignmentId);
        [OperationContract]
        DataSet GetStudentJoinedToCourse(long CourseId);
        [OperationContract]
        DataSet GetAllStudentsJoinedToInstructor(int InstructorId);
        [OperationContract]
        bool InsertNewCourseV2(string CourseName, string CourseDescription, int InstructorId, string AboutCourse, string CourseImagePath, ref long CourseId);
        [OperationContract]
        bool InsertNewIndexToV2Course(long CourseId, string IndexName, string IndexContetHtml, ref long IndexId);
        [OperationContract]
        bool InsertNewTopicToV2Course(long IndexId, string TopicName, string TopicHtml);
        [OperationContract]
        bool InsertNewAlertForInstructor(int InstructorId, string AlertMessage, int AlertTypeId, long? EffectiveContentid, long StudentId);
        [OperationContract]
        DataSet GetAllAlertForInstructor(int InstructorId);
        [OperationContract]
        bool MarkAlertReadForInstructor(long AlertId);
        [OperationContract]
        bool GetInstructorIdByAssignmentId(long AssignmentId, ref int InstructorId);
        [OperationContract]
        bool GetInstructorIdByTestId(long TestId, ref int InstructorId);
        [OperationContract]
        bool GetInstructorIdByCourseId(long CourseId, ref int InstructorId);
        [OperationContract]
        DataSet SearchForCourseOfInstructor(string SerachString, int MaxRowToReturn, int InstructorId);
        [OperationContract]
        DataSet SearchForAssignmentOfInstructor(string SerachString, int MaxRowToReturn, int InstructorId);
        [OperationContract]
        DataSet SearchForTestOfInstructor(string SerachString, int MaxRowToReturn, int InstructorId);
        [OperationContract]
        DataSet SearchForCourseForStudent(string SerachString, int MaxRowToReturn, int NoOfRowsFetch, int SortType, long StudentId);
        [OperationContract]
        DataSet CheckStudentHasJoinedTheCourse(long StudentId, long CourseId);
        [OperationContract]
        DataSet CheckStudentHasSubmittedTheAssignment(long StudentId, long AssignmentId);
        [OperationContract]
        DataSet CheckStudentHasSubmittedTheTest(long StudentId, long TestId);
        [OperationContract]
        DataSet CheckAssignmentResponseIdExistsForStudent(long StudentId, long SubmissionId);
        [OperationContract]
        DataSet CheckTestResponseIdExistsForStudent(long StudentId, long SubmissionId);
        [OperationContract]
        DataSet GetInstructorProfileDetails(int InstructorId);
        [OperationContract]
        DataSet CheckTestAccess(long TestId, string AccessCode);
        [OperationContract]
        DataSet CheckAssignmentAccess(long AssignmentId, string AccessCode);
        [OperationContract]
        DataSet GetIndependentAssignmentDetails(long AssignmentId);
        [OperationContract]
        DataSet GetIndependentTestDetails(long TestId);
        [OperationContract]
        DataSet GetTestDetailsWithAccessCode(long TestId, string AccessCode);
        [OperationContract]
        DataSet GetAssignmentDetailsWithAC(long AssignmentId, string AccessCode);
        [OperationContract]
        DataSet GetWebsiteAboutDetails();
        [OperationContract]
        bool UpdateNotificationStatus(bool Status, long NotificationId);
        [OperationContract]
        DataSet GetAllNotificationToPrecess(int MaxRetryCount);
        [OperationContract]
        bool InsertSmsNotification(int NotificationTypeId, string SmsBody, string ReceiverPhoneNo);
        [OperationContract]
        bool InsertStudentPasswordRecoveryRequest(string UserId, string Token, string OTP);
        [OperationContract]
        DataSet ValidateStudentPasswordRecoveryRequest(string UserId, string Token, string OTP);
        [OperationContract]
        bool ChanegPasswordAfterAuthentication(string UserId, string Token, string HashedPassword);
        [OperationContract]
        bool MarkOtpVarified(string UserId, string Token);


    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
