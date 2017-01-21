using System.Collections.Generic;

namespace StudentProgress.Entities
{
    public class Group
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public ICollection<Student> ListOfStudents { set; get; }
        public virtual long? CapitainId { set; get; }
        public ICollection<JournalSheet> ListOfJournalSheets { set; get; }
        public virtual long? FacultyId { set; get; }

        public Group()
        {
            ListOfStudents = new List<Student>();
        }

        public Group(string name)
        {
            Name = name;
            ListOfStudents = new List<Student>();
        }
    }
}
