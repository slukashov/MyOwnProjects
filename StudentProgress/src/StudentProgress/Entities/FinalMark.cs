namespace StudentProgress.Entities
{
    public class FinalMark
    {
        public long Id { set; get; }
        public virtual long? JournalSheetId{ set; get; }
        public JournalSheet JournalSheet { set; get; }
        public virtual long? StudentId { set; get; }
        public Student Student { set; get; }
        public int Rating { set; get; }

        public FinalMark() { }

        public FinalMark(int rating) { Rating = rating; }

        public FinalMark(int rating, long journalSheetId, long studentId)
        {
            Rating = rating;
            JournalSheetId = journalSheetId;
            StudentId = studentId;
        }
    }
}
