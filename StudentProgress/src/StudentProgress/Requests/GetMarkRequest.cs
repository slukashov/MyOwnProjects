using StudentProgress.Entities;
using System.Collections.Generic;

namespace StudentProgress.Requests
{
    public class GetMarkRequest
    {
        public long JournalSheetId { set; get; }
        public List<Student> Students { set; get; }
        public List<Lesson> Lessons { set; get; }
        public IList<RatingRequest> Ratings { set; get; }
    }
}

