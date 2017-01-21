using StudentProgress.Entities;
using System.Collections.Generic;

namespace StudentProgress.Requests
{
    public class FillJournalSheetRequest
    {
        public long JournalSheetId { set; get; }
        public List<Student> ListOfStudents { set; get; }
        public List<Lesson> ListOfLessons { set; get; } 
        public List<bool[]> ListOfAttendings { set; get; }
        public List<int[]> ListOfMarks { set; get; }
    }
}
