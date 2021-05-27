using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Classroom
{
    public class ClassroomShowcaseDetails: ClassroomBase
    {
        [JsonProperty("classsroom_image")]
        public string ClassroomImage;
        [JsonProperty("no_of_students_joined")]
        public int NoOfStudentsJoined;
        [JsonProperty("avg_rating")]
        public double? AvgRating;
    }
}