﻿using Newtonsoft.Json;
using StudentDashboard.Models.Classroom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class ClassroomJoinDetailsModal
    {
        public int m_iNoOfStudentsJoined;
        public int m_iNoOfAssignments;
        public int m_iNoOfLiveClass;
        public long m_llClassroomId;
        public int m_iNoOfTests;
        public int m_iNoOfStudyMaterials;
        public int m_iNoOfPracticeMaterials;
        public string m_strClassroomName;
        public string m_strClassroomDescription;
        public string m_strClassroomTime;
        public string m_strClassroomCreationDate;
        public int m_iClassroomCharge;
        public string m_strClassroomCharge;
        public string m_strClassroomBackgroundImage;
        public ClassroomScheduleDetails classroomScheduleDetails;
        
        public ClassroomSyllabusDetailsModal classroomSyllabusDetailsModal;
        public ClassroomJoinDetailsModal(int NoOfStudentsJoined,int NoOfAssignments,int NoOfLiveClassess,
            int NoOfTests,int NoOfStudyMaterials,string ClassroomName,string ClassroomDescription,DateTime ClassroomStartDate,
            int ClassroomChargeInPaise,string ClassroomImage,string ClassroomSyllabus,string Schedule
            )
        {
            this.m_iNoOfAssignments = NoOfAssignments;
            this.m_iNoOfStudentsJoined = NoOfStudentsJoined;
            this.m_iNoOfLiveClass = NoOfLiveClassess;
            this.m_iNoOfStudyMaterials = NoOfStudyMaterials;
            this.m_iNoOfTests = NoOfTests;
            if(m_iNoOfAssignments<5)
            {
                m_iNoOfAssignments = 5;
            }
            if(m_iNoOfTests<5)
            {
                m_iNoOfTests = 5;
            }
            if(m_iNoOfLiveClass<20)
            {
                m_iNoOfLiveClass = 20;
            }
            if(m_iNoOfStudyMaterials<10)
            {
                m_iNoOfStudyMaterials = 10;
            }
            if(NoOfStudentsJoined<1000)
            {
                m_iNoOfStudentsJoined = 1000;
            }
            m_iNoOfPracticeMaterials = m_iNoOfTests + m_iNoOfAssignments;
            this.m_strClassroomName = ClassroomName;
            this.m_strClassroomDescription = ClassroomDescription;
            this.m_strClassroomCreationDate = ClassroomStartDate.AddDays(13).ToString("d MMM yyyy");
            this.m_iClassroomCharge = ClassroomChargeInPaise/100;
            if(this.m_iClassroomCharge==0)
            {
                this.m_strClassroomCharge = "free";
            }
            else
            {
                this.m_strClassroomCharge = this.m_iClassroomCharge.ToString();
            }
            if(ClassroomImage==null)
            {
                this.m_strClassroomBackgroundImage = Constants.CLASSROOM_DEFAULT_IMAGE;
            }
            else
            {
                this.m_strClassroomBackgroundImage = ClassroomImage;
            }
            if(ClassroomSyllabus!=null)
            {
                classroomSyllabusDetailsModal = new ClassroomSyllabusDetailsModal();
                classroomSyllabusDetailsModal.m_lsClassroomWeekWiseSyallabus = JsonConvert.DeserializeObject<List<ClassroomWeekWiseSyallabus>>(ClassroomSyllabus);
            }
            if (Schedule != null)
            {
                setClassroomSchedule(Schedule);
            }
        }
        public ClassroomJoinDetailsModal()
        {

        }
        public ClassroomJoinDetailsModal(int NoOfStudentsJoined, int NoOfAssignments, int NoOfLiveClassess,
            int NoOfTests, int NoOfStudyMaterials, string ClassroomSyllabus, string Schedule)
        {
            this.m_iNoOfAssignments = NoOfAssignments;
            this.m_iNoOfStudentsJoined = NoOfStudentsJoined;
            this.m_iNoOfLiveClass = NoOfLiveClassess;
            this.m_iNoOfStudyMaterials = NoOfStudyMaterials;
            this.m_iNoOfTests = NoOfTests;
            if (m_iNoOfAssignments < 5)
            {
                m_iNoOfAssignments = 5;
            }
            if (m_iNoOfTests < 5)
            {
                m_iNoOfTests = 5;
            }
            if (m_iNoOfLiveClass < 20)
            {
                m_iNoOfLiveClass = 20;
            }
            if (m_iNoOfStudyMaterials < 10)
            {
                m_iNoOfStudyMaterials = 10;
            }
            if (NoOfStudentsJoined < 1000)
            {
                m_iNoOfStudentsJoined = 1000;
            }
            m_iNoOfPracticeMaterials = m_iNoOfTests + m_iNoOfAssignments;
            if (ClassroomSyllabus != null)
            {
                classroomSyllabusDetailsModal = new ClassroomSyllabusDetailsModal();
                classroomSyllabusDetailsModal.m_lsClassroomWeekWiseSyallabus = JsonConvert.DeserializeObject<List<ClassroomWeekWiseSyallabus>>(ClassroomSyllabus);
            }
            if (Schedule != null)
            {
                setClassroomSchedule(Schedule);
            }
        }
        private void setClassroomSchedule(string Schedule)
        {
            classroomScheduleDetails = JsonConvert.DeserializeObject<ClassroomScheduleDetails>(Schedule);
            if(classroomScheduleDetails!=null&& classroomScheduleDetails.m_lsDayWiseScheduleDetails!=null)
            {
                var days = new List<String> { "Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"};
                for(var i=0;i<classroomScheduleDetails.m_lsDayWiseScheduleDetails.Count;i++)
                {
                    classroomScheduleDetails.m_lsDayWiseScheduleDetails[i].m_strDayName = days[i];
                }
            }
        }
    }
}