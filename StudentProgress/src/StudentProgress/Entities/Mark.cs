using NuGet.Protocol.Core.v3;

namespace StudentProgress.Entities
{
    public class Mark
    {
        public long Id { set; get; }
        public virtual long? JournalSheetId { set; get; }
        public virtual long? StudentId { set; get; }
        public int Rating { set; get; }
        public virtual long? LessonId { set; get; }
        public virtual Lesson Lesson { set; get; }

        public Mark() { }

        public Mark (long id, int rating, long journalSheetId, long lessonId, long studentId)
        {
            Id = id;
            Rating = rating;
            JournalSheetId = journalSheetId;
            LessonId = lessonId;
            StudentId = studentId;
        }
    }
}
