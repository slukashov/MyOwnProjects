using System;

namespace StudentProgress.Models
{
    public class LessonModel
    {
        public long? LessonId { set; get; }
        public long JournalSheetId { get; set; }
        public DateTime LessonDate { set; get; }
        public int Mark { get; set; }
        public bool Attending { get; set; }
    }
}
