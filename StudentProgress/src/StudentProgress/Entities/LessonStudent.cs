namespace StudentProgress.Entities
{
    public class LessonStudent
    {
        public long? StudentId { set; get; }
        public virtual Student Student { set; get; }
        public long? LessonId { set; get; }
        public virtual Lesson Lesson { set; get; }
        public bool Attending { set; get; }

        public LessonStudent() { }

        public LessonStudent(long studentId, long lessonId, bool attending)
        {
            StudentId = studentId;
            LessonId = lessonId;
            Attending = attending;
        }
    }
}
