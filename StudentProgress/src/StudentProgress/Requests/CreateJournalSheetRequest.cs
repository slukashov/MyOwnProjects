namespace StudentProgress.Requests
{
    public class CreateJournalSheetRequest
    {
        public long GroupId { set; get; }
        public long DisciplineId { set; get; }
        public long ProfessorId { set; get; }
        public string Semester { set; get; }
    }
}
