using System;
using System.Collections.Generic;
using StudentProgress.Models;

namespace StudentProgress.Requests
{
    public class LessonRequest
    {
        public long JournalSheetId { set; get; }
        public DateTime Date { set; get; }
        public IList<StudentModel> StudentModels { set; get; }
    }
}
