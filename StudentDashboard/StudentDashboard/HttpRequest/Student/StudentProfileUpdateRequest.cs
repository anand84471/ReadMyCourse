using System.ComponentModel.DataAnnotations;

namespace StudentDashboard.HttpRequest.Student
{
    public class StudentProfileUpdateRequest:StudentScialProfileDetails
    {
        [Display(Name = "student_id")]
        public long m_llStudentId;
        [Display(Name = "first_name")]
        public string m_strFirstName;
        [Display(Name = "last_name")]
        public string m_strLastName;
    }
}