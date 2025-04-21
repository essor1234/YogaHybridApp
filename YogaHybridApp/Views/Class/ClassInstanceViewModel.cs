using System;
using YogaHybridApp.Objects;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YogaHybridApp.Views.Class
{
    public class ClassInstanceViewModel
    {
        public string ClassInstanceId { get; set; }
        public string CourseId { get; set; }
        public DateTime Date { get; set; }
        public string TeacherName { get; set; }
        public string Comment { get; set; }
        public Teacher Teacher { get; set; } // Added to store full teacher details
        public Objects.Course Course { get; set; }
    }
}
