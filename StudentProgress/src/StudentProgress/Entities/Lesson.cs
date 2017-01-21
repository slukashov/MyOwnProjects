using System;
using System.Collections.Generic;

namespace StudentProgress.Entities
{
    public class Lesson
    {
        public long Id { set; get; }
        public DateTime Date { set; get; }
        public virtual long? JournalSheetId {set;get;}
        public JournalSheet JournalSheet { set; get; }
        public virtual ICollection<LessonStudent> ListOfStudents{ set; get; }

        public Lesson()
        {
            ListOfStudents = new List<LessonStudent>();
        }

        public Lesson(DateTime date, JournalSheet journalSheet):this()
        {
            Date = date;
            JournalSheet = journalSheet;
            JournalSheetId = journalSheet.Id;
        }
    }
}
