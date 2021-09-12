using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace StudentDashboard.Utilities
{
    public class SmsManger
    {
        private string _authToken = "";
        private string _accountSid = "";
        private string _phoneNoOfSender;
        public SmsManger()
        {
            ParseAuthSetting();
            TwilioClient.Init(_accountSid, _authToken);
        }
        private void ParseAuthSetting()
        {
            if(ConfigurationManager.AppSettings["TwilioAccountSid"]!=null)
            {
                _accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"].ToString();
            }
            if (ConfigurationManager.AppSettings["TwilioAuthToken"] != null)
            {
                _authToken = ConfigurationManager.AppSettings["TwilioAuthToken"].ToString();
            }
            if (ConfigurationManager.AppSettings["TwilioPhoneNumber"] != null)
            {
                _phoneNoOfSender = ConfigurationManager.AppSettings["TwilioPhoneNumber"].ToString();
            }
        }
        public void SendEmail(string PhoneNo,string Message )
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                | SecurityProtocolType.Tls11
                                                | SecurityProtocolType.Tls12
                                                | SecurityProtocolType.Ssl3;
            try
            {
                var message = MessageResource.Create(
                body: Message,
                from: new Twilio.Types.PhoneNumber(_phoneNoOfSender),
                to: new Twilio.Types.PhoneNumber(PhoneNo));
                var result = message.Sid;
            }
            catch(Exception Ex)
            {

            }
        }

    }
}