using Newtonsoft.Json;
using StudentDashboard.Models.Classroom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
	public class InstructorClassroomsEarningDetails:ClassroomBase
	{
		[JsonProperty("total_earnings")]
		public int TotalEarning;
		[JsonProperty("students_joined")]
		public int TotalSells;
	}
}