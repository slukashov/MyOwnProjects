using System.Collections.Generic;

namespace StudentProgress.Models
{
    public class StudentModel
    {
        public long StudentId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string GroupId { set; get; }
        public IEnumerable<LessonModel> Lessons { get; set; } = new List<LessonModel>();
    }
}
