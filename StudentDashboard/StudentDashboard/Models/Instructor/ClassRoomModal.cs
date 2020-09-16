using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class ClassRoomModal
    {
        [JsonProperty("classroom_id")]
        public long m_llClassRoomId;
        [JsonProperty("classroom_name")]
        public string m_strClassRoomName;
        [JsonProperty("classroom_description")]
        public string m_strClassRoomDescription;
        [JsonProperty("creation_date")]
        public string m_strCreationDate;
        [JsonProperty("updation_date")]
        public string m_strUpdationDate;
        [JsonProperty("background_url")]
        public string m_strBackGroundImageUrl;
        [JsonProperty("classroom_status")]
        public string m_strClassroomStatus;
        [JsonProperty("share_code")]
        public string m_strShareCode;
        [JsonProperty("share_url")]
        public string m_strShareUrl;
        [JsonProperty("no_of_students_joined")]
        public string m_strNoOfStudentsJoined;
        public int m_iInstrutcorId;
        [JsonProperty("no_of_posts")]
        public int m_iNoOfPosts;
        [JsonProperty("is_meeting_active")]
        public bool? m_bIsMeetingActive;
        [JsonProperty("no_of_test")]
        public int m_iNoOfTests;
        [JsonProperty("no_of_assignments")]
        public int m_iNoOfAssignments;
        [JsonProperty("no_of_student_joined")]
        public int m_iNoOfStudentsJoined;
        [JsonProperty("no_of_meetings")]
        public int m_iNoOfMeetings;
        [JsonProperty("classroom_meeting_name")]
        public string m_strClassroomMeetingName;
        [JsonProperty("classroom_joining_fee")]
        public string m_strClassrooomJoiningFee;

        public ClassRoomModal()
        {

        }
        public ClassRoomModal(long ClassRoomId,string ClassRoomName,string CreationDate, string ClassroomStatus)
        {
            this.m_llClassRoomId = ClassRoomId;
            this.m_strClassRoomName = ClassRoomName;
            this.m_strCreationDate = CreationDate; ;
            this.m_strClassroomStatus = ClassroomStatus;
        }
        public ClassRoomModal(long ClassRoomId, string ClassRoomName, string CreationDate, string ClassroomStatus,bool? IsMeetingActive)
        {
            this.m_llClassRoomId = ClassRoomId;
            this.m_strClassRoomName = ClassRoomName;
            this.m_strCreationDate = CreationDate; ;
            this.m_strClassroomStatus = ClassroomStatus;
            this.m_bIsMeetingActive = IsMeetingActive;
        }
        public ClassRoomModal(long ClassRoomId, string ClassRoomName, string ClassroomDescription, string CreationDate, string ClassroomStatus,
            int NoOfPosts,string ShareUrl,string AccessCode,int NoOfAssignments,int NoOfTests,int NoOfStudentsJoined,int NoOfMeetings,
            string BackgroundUrl,string MeetingName)
        {
            this.m_llClassRoomId = ClassRoomId;
            this.m_strClassRoomName = ClassRoomName;
            this.m_strCreationDate = CreationDate;
            this.m_strClassRoomDescription = ClassroomDescription;
            this.m_strClassroomStatus = ClassroomStatus;
            this.m_iNoOfPosts = NoOfPosts;
            this.m_strShareCode = AccessCode;
            this.m_strShareUrl = ShareUrl;
            this.m_iNoOfAssignments = NoOfAssignments;
            this.m_iNoOfTests = NoOfTests;
            this.m_iNoOfStudentsJoined = NoOfStudentsJoined;
            this.m_iNoOfMeetings = NoOfMeetings;
            this.m_strBackGroundImageUrl = BackgroundUrl;
            this.m_strClassroomMeetingName = MeetingName;
        }
        public ClassRoomModal(long ClassRoomId, string ClassRoomName, string classroomDescription,
             string ClassroomCreationDate,string NoOfStudentsJoined,bool? IsMeetingActive,string BackGroundImageUrl,string ClassroomMeetingName,int ClassroomJoiningFeeInPaise)
        {
            this.m_llClassRoomId = ClassRoomId;
            this.m_strClassRoomName = ClassRoomName;
            this.m_strClassRoomDescription = classroomDescription; ;
            this.m_strCreationDate = ClassroomCreationDate;
            this.m_strNoOfStudentsJoined = NoOfStudentsJoined;
            this.m_bIsMeetingActive = IsMeetingActive;
            this.m_strBackGroundImageUrl = BackGroundImageUrl;
            this.m_strClassroomMeetingName = ClassroomMeetingName;
            if (ClassroomJoiningFeeInPaise == 0)
            {
                this.m_strClassrooomJoiningFee = "free";
            }
            else
            {
                this.m_strClassrooomJoiningFee = "" + (ClassroomJoiningFeeInPaise / 100).ToString();
            }
        }
    }
}