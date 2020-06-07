﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Course
{
    public class CourseModel
    {
        [JsonProperty("instructor_id")]
        public string m_strInstructorUserName { get; set; }
        public long m_llCourseId { get; set; }
        [JsonProperty("name")]
        public string m_strCourseName { get; set; }
        [JsonProperty("description")]
        public string m_strCourseDescription { get; set; }
        [JsonProperty("creation_date")]
        public DateTime m_dtCreationDate { get; set; }
        [JsonProperty("updation_date")]
        public DateTime m_dtCourseUpdationDate { get; set; }
        [JsonProperty("tests")]
        public List<TestModel> m_lsTests { get; set; }
        [JsonProperty("assignments")]
        public List<AssignmentModel> m_lsAssignments { get; set; }
        [JsonProperty("indexes")]
        public List<IndexModel> m_lsIndexes { get; set; }
        public int m_iTotalNoOdIndexes { get; set; }
        public  CourseModel()
        {

        }
        public CourseModel(long Id,string Name,DateTime CreationDate,int NoOfindexes)
        {
            this.m_llCourseId = Id;
            this.m_dtCreationDate = CreationDate;
            this.m_strCourseName = Name;
            this.m_iTotalNoOdIndexes = NoOfindexes;
        }
        public CourseModel(string Name, DateTime CreationDate, DateTime UpdationDate,int NoOfindexes)
        {
            this.m_dtCreationDate = CreationDate;
            this.m_strCourseName = Name;
            this.m_iTotalNoOdIndexes = NoOfindexes;
        }
    }
}