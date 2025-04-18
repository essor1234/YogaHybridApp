using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YogaHybridApp.Objects
{
    public class ClassInstance
    {
        private string ClassInstanceId { get; set; }
        private string CourseId { get; set; }
        private DateTime Date { get; set; }
        private string TeacherId { get; set; }
        private string Comment {get; set; }

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
