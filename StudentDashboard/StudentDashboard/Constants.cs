using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard
{
    public static class Constants
    {
      
        public static readonly string API_RESPONSE_MESSAGE_SUCCESS = "success";
        public static readonly string API_RESPONSE_MESSAGE_FAIL = "fail";
        public static readonly int COUNTRY_CODE_FOR_INDIA = 101;
        public static readonly int API_RESPONSE_CODE_SUCCESS = 1;
        public static readonly int API_RESPONSE_CODE_FAIL = -1;
        public static readonly int MAX_ITEMS_TO_BE_RETURNED = 10;
        public static readonly int SMS_VERIFICATION_LINK_TYPE_ID_FOR_INSTRUCTOR = 1;
        public static readonly int SMS_VERIFICATION_LINK_TYPE_ID_FOR_STUDENT = 2;
        public const int COURSE_STATUS_ACTIVE=2;
        public const int COURSE_STATUs_DELETED = 3;
        public const int COURSE_STATUs_CREATED = 1;
        public static readonly string ASSIGNMENT_TYPE_MCQ = "mcq";
        public static readonly string ASSIGNMENT_TYPE_SUBJECTIVE = "sub";
        public static readonly string TEST_TYPE_MCQ = "mcq";
        public static readonly string TEST_TYPE_SUBJECTIVE = "sub";
        public static readonly string QUESTION_TYPE_SUBJECTIVE = "Subjective";
        public static readonly string NEW_ASSIGNMENT_CREATED = "A new assignment added ";
        public static readonly string NEW_COURSE_CREATED = "A new course created ";
        public static readonly string COURSE_DELETED = "A course deleted ";
        public static readonly string NEW_TEST_CREATED = "A new test created ";
        public static readonly string ASSIGNMENT_UPDATED = "An assignment updated ";
        public static readonly string TEST_UPDATED = "A  test created ";
        public static readonly string COURSE_UPDATED = "A course upadted ";
        public static readonly string ALERT_INSTRUCTOR_NEW_JOIN = "new student joined";
        public static readonly string ALERT_INSTRUCTOR_NEW_COURSE_JOIN = "new course join";
        public static readonly string ALERT_INSTRUCTOR_NEW_ASSIGNMENT_SUBMISSION = "new assignment submission";
        public static readonly string ALERT_INSTRUCTOR_NEW_TEST_SUBMISSION = "new test submission";
        public static readonly string MESSEGE_SENDER_CLASSROOM = "admin";
        public static readonly string CONTENT_STATUS_ACTIVE = "active";
        public static readonly string CONTENT_STATUS_INACTIVE = "in active";
        public static readonly string MEETING_STATUS_ACTIVE = "active";
        public static readonly string MEETING_STATUS_CLOSED = "closed";
        public static readonly string CLASSROOM_MEETING_NOT_CLOSED_MESSAGE = "NA";
        public static readonly string ACCESS_CODE_FOR_INTERNAL_ACCESS= "q96zSIeRwke2qflTV0kWRA";

        #region Tiny url service
        public static string BASE_URL_PATH_FOR_ASSIGNMENT = "https://readmycourse.com/Assignment/Details?id=";
        public static string BASE_URL_PATH_FOR_TEST = "https://readmycourse.com/Test/Details?id=";
        public static string BASE_URL_PATH_FOR_COURSE = "https://readmycourse.com/Course/Details?id=";
        public static string BASE_URL_PATH_FOR_CLASSROOM = "https://readmycourse.com/Classroom/Details?id=";
        public static string BASE_URL_PATH_FOR_AUTHORIZATION = "https://readmycourse.com/AuthService/?";
        #endregion

        #region SMS notification type
        public static int SMS_NOTIFICATION_TYPE_INSTRUCTOR_FORGOT_PASSWORD = 1;
        public static int SMS_NOTIFICATION_TYPE_STUDENT_FORGOT_PASSWORD = 2;
        public static int SMS_NOTIFICATION_TYPE_INSTRUCTOR_PHONE_NO_VERIFICATION = 3;
        public static int SMS_NOTIFICATION_TYPE_STUDENT_PHONE_NO_VERIFICATION = 4;
        #endregion
        
      
        #region fluent validation constants for Studnet Registration
        public static readonly int STUDENT_FIRST_NAME_MAX_LENGTH = 250;
        public static readonly int STUDENT_LAST_NAME_MAX_LENGTH = 250;
        public static readonly int STUDENT_EMAIL_MAX_LENGTH = 250;
        public static readonly int STUDENT_PASSWORD_MAX_LENGTH = 250;
        public static readonly int STUDENT_PHONE_NO_MAX_LENGTH = 20;
        public static readonly int STUDENT_PASSWORD_MINIMIUM_LENGTH = 8;
        #endregion
        #region fluent validation constants for Instructor Registration
        public static readonly int INSTRUCTOR_FIRST_NAME_MAX_LENGTH = 250;
        public static readonly int INSTRUCTOR_LAST_NAME_MAX_LENGTH = 250;
        public static readonly int INSTRUCTOR_EMAIL_MAX_LENGTH = 250;
        public static readonly int INSTRUCTOR_PASSWORD_MAX_LENGTH = 250;
        public static readonly int INSTRUCTOR_PHONE_NO_MAX_LENGTH = 20;
        public static readonly int INSTRUCTOR_PASSWORD_MINIMIUM_LENGTH = 8;
        #endregion
        
        public enum AssignmentQuestionType
        {
            MCQ=1,
            SUBJECTIVE=2
        }
        public enum TestQuestionType
        {
            MCQ=1,
            SUBJECTIVE=2
        }
        public enum ActivityType
        {
            COURSE_CREATED = 1,
            TEST_CREATED = 2,
            ASSIGNMENT_CREATED = 3,
            COURSE_ACTIVATED = 4,
            COURSE_DELETED = 5,
            ASSIGNMENT_DELETED = 6,
            TEST_DELETED = 7,
            COURSE_UPDATED = 8,
            ASSIGNMENT_UPDATED = 9,
            TEST_UPDATED = 10
        }
        public enum InstructorAlertType
        {
            STUDNET_JOINED = 1,
            COURSE_JOINED = 2,
            ASSIGNMENT_SUBMISSION = 3,
            TEST_SUBMISSION = 4
        }
        public enum AssignentHostType
        {
            CLASSROOM=1
        }
        public enum AWSFileType
        {
            IMAGE=1,
            VIDEO=2
        }
        public enum AWSFolderType
        {
            INSTRUCTOR=1,
            STUDENT=2,
            ADMIN=3
        }
       public enum FileUploadTypeId
        {
            IMAGE=1,
            PDF=2,
            VIDEO=3,
            CUSTOM=4
        }

    }
}