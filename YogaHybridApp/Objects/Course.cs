using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YogaHybridApp.Objects
{
    public class Course
    {
        public  string CourseId { get; set; }
        public string Time { get; set; }
        public string DayOfWeek { get; set; }
		public int Capacity { get; set; }
		public int Duration { get; set; }
		public int Price { get; set; }
		public string Category { get; set; }
		public string Level { get; set; }
		public string Location { get; set; }
		public string Deadline { get; set; }
		public string Description { get; set; }

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

		public Course() { }


	}
}
