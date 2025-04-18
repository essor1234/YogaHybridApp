using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YogaHybridApp.Objects
{
    public class Course
    {
        private  string CourseId { get; set; }
        private string Time { get; set; }
        private string DayOfWeek { get; set; }
		private int Capacity { get; set; }
		private int Duration { get; set; }
		private int Price { get; set; }
		private string Category { get; set; }
		private string Level { get; set; }
		private string Location { get; set; }
		private string Deadline { get; set; }
		private string Description { get; set; }

		public Course(string courseId, string time, string dayOfWeek,
				int capacity, int duration, int price, string category, string level,
				string location, string deadline, string description) { 
			CourseId = courseId;
			Time = time;
			DayOfWeek = dayOfWeek;
			Capacity = capacity;
			Duration = duration;
			Price = price;
			Category = category;
			Level = level;
			Location = location;
			Deadline = deadline;
			Description = description;
		
		}


	}
}
