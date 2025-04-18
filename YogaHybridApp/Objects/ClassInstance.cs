using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YogaHybridApp.Objects
{
    public class ClassInstance
    {
        public string ClassInstanceId { get; set; }
        public string CourseId { get; set; }
        public DateTime Date { get; set; }
        public string TeacherId { get; set; }
        public string Comment {get; set; }

        public ClassInstance(string classInstanceId, string courseId, DateTime date, string teacherId, string comment)
        {
            ClassInstanceId = classInstanceId;
            CourseId = courseId;
            Date = date;
            TeacherId = teacherId;
            Comment = comment;
        }
    }
}
