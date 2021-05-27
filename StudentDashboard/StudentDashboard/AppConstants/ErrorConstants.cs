using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.AppConstants
{
    public static class ErrorConstants
    {
        public static readonly int INTERNAL_SERVER_ERROR = -100;
        public static readonly int GOOGLE_LOGIN_FAILED = -101;
        public static readonly int FB_LOGIN_FAILED = -102;
        public static readonly int IVALID_CREDIENTIALS = -103;
        public static readonly int USER_ID_DOES_NOT_EXISTS = -104;
        public static readonly int WRONG_OTP = -105;
        public static readonly string GOOGLE_USER_NOT_EXISTS_MSG = "user is not registered";
        public static readonly string FB_USER_NOT_EXISTS_MSG = "User is not registered";
        public static readonly string INVALID_LOGIN_CREDENTIALS_MSG = "Invalid credentials";
        public static readonly string USER_ID_DOES_NOT_EXISTS_MSG = "User Id does not exists";
        public static readonly string WRONG_OTP_MESSAGE = "Wrong otp";
    }
}