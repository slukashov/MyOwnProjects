using System.Collections.Generic;

namespace StudentProgress.Entities
{
    public class JournalSheet
    {
        public long Id { set; get; }
        public string Semester { set; get; }
        public Group Group { set; get; }
        public virtual long? GroupId { set; get; }
        public Discipline Discipline { set; get; } = new Discipline();
        public virtual long? DisciplineId { set; get; }
        public Professor Professor { set; get; }
        public virtual long? ProfessorId { set; get; }
        public virtual ICollection<Lesson> ListOfLessons { set; get; }
        public virtual ICollection<Mark> ListOfMarks { set; get; }
        public virtual ICollection<FinalMark> ListOfFinalMarks { set; get; }

        public JournalSheet(Group group,
            string disciplineName,
            Professor professor,
            string semester)
        {
            Group = group;
            Discipline.Name = disciplineName;
            Professor = professor;
            Semester = semester;
        }

        public JournalSheet(Group group,
            Discipline discipline,
            Professor professor,
            string semester)
        {
            Group = group;
            Discipline = discipline;
            Professor = professor;
            Semester = semester;
        }

        public JournalSheet() { }
    }
}
