using Newtonsoft.Json;
using StudentDashboard.Models.Base;
using StudentDashboard.Models.Classroom;
using StudentDashboard.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class InstructorPublicProfileDetails:InstructorBase
    {
        [JsonProperty("bio")]
        public string Bio;
        [JsonProperty("no_of_followers")]
        public int NoOfFollowers;
        [JsonProperty("no_of_courses")]
        public int NoOfCourses;
        [JsonProperty("no_of_students_joined")]
        public int NoOfStudentsJoined;
        [JsonProperty("classrooms")]
        public List<ClassroomShowcaseDetails> classrooms;
        [JsonProperty("avg_rating")]
        public double? AvgRating;
        [JsonProperty("reviews")]
        public List<ReviewModel> reviews;
    }
}