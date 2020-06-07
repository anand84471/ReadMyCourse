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

    }
}