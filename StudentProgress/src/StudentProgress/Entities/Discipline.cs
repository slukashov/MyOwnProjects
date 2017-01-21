using System.Collections.Generic;

namespace StudentProgress.Entities
{
    public class Discipline
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public virtual ICollection<JournalSheet> ListOfJournalSheets{ set; get; } = new List<JournalSheet>();

        public Discipline() { }

        public Discipline(string disciplineName)
        {
            Name = disciplineName;
        }
    }
}
