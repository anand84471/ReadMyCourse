using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Category
{
    public class CategoryModel
    {
        [JsonProperty("category_name")]
        public string CategoryName { get; set; }
        [JsonProperty("category_id")]
        public int CategoryId { get; set; }
        [JsonProperty("no_of_courses")]
        public int NoOfCourses { get; set; }
        [JsonProperty("category_image_url")]
        public string CategoryImageUrl;
    }
}