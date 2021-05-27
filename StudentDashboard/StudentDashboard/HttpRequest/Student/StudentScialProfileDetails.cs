using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest.Student
{
    public class StudentScialProfileDetails
    {
        [Display(Name ="personal_website")]
        public string m_strPersonalWebsite;
        [Display(Name = "linkedin_url")]
        public string m_strLinkedInUrl;
        [Display(Name = "instagram_url")]
        public string m_strInstagramUrl;
        [Display(Name = "facebook_url")]
        public string m_strFacebookUrl;
        [Display(Name = "twitter_url")]
        public string m_strTwitterUrl;

    }
}