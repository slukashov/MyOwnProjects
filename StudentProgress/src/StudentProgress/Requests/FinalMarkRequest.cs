using StudentProgress.Entities;
using System.Collections.Generic;

namespace StudentProgress.Requests
{
    public class FinalMarkRequest
    {
        public long JournalSheetId { set; get; }
        public IList<Student> Students { set; get; }
        public IList<RatingRequest> Ratings { set; get; }
    }
}
