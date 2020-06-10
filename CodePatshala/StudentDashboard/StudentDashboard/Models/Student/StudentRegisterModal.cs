﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentRegisterModal
    {
        public string m_strFirstName { get; set; }
        public string m_strLastName { get; set; }
        public string m_strAddressLine1 { get; set; }
        public string m_strAddressLine2 { get; set; }
        public string m_strPhoneNo { get; set; }
        public string m_strUserId { get; set; }
        public string m_strEmail { get; set; }
        public string m_strHashedPassword { get; set; }
        public string m_strPassword { get; set; }
        public bool m_bIsRemeberMe { get; set; }
        public long m_llStudentId { get; set; }
        public string m_strState { get; set; }
        public string m_strCity { get; set; }
        public string m_strGender { get; set; }
        public string m_strPinCode { get; set; }
        public string m_strDateOfJoining { get; set; }
        public string m_strLastUpdated { get; set; }
        public string m_strFullAddress { get; set; }
        public int m_iCityId { get; set; }
        public int m_iStateId { get; set; }
        public string m_strToken { get; set; }
        public StudentRegisterModal()
        {

        }

        public StudentRegisterModal(string FirstName, string LastName, string InstrcutorEmail, string InstructorPhoneNo, string AddressLine1,
            string AddreessLine2, string CityName, string StateName, string PinCode, string Gender, string LastUpdated, string DateOfJoining)
        {
            this.m_strFirstName = FirstName;
            this.m_strLastName = LastName;
            this.m_strEmail = InstrcutorEmail;
            this.m_strPhoneNo = InstructorPhoneNo;
            this.m_strAddressLine1 = AddressLine1;
            this.m_strAddressLine2 = AddreessLine2;
            this.m_strState = StateName;
            this.m_strCity = CityName;
            this.m_strPinCode = PinCode;
            this.m_strGender = Gender;
            this.m_strDateOfJoining = DateOfJoining;
            this.m_strLastUpdated = LastUpdated;

            if (this.m_strState == null)
            {
                this.m_strFullAddress = "Not set";
            }
            else
            {
                this.m_strFullAddress = this.m_strAddressLine1 + ", " + this.m_strAddressLine2 + ", " + this.m_strCity + ", " + this.m_strState + ", pincode- " + this.m_strPinCode;
            }
        }

    }
}